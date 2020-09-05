using BepInEx;
using R2API.Utils;
using RoR2;

namespace TotalItemCount
{
    [BepInDependency("com.bepis.r2api")]
    //Change these
    [BepInPlugin("com.theOneTrueGod.TotalItemCount", "Gives total item count", "1.0.0")]
    public class TotalItemCount : BaseUnityPlugin
    {
        private int GetItemCount(CharacterMaster master)
        {
            int itemCount = 0;
            for (ItemTier i = 0; i <= ItemTier.Boss; i++)
            {
                itemCount += master.inventory.GetTotalItemCountOfTier(i);
            }
            return itemCount;
        }

        private void SetItemCountDisplay(
            On.RoR2.UI.ScoreboardStrip.orig_SetMaster orig,
            RoR2.UI.ScoreboardStrip self,
            CharacterMaster master
        )
        {
            orig(self, master);

            self.nameLabel.text = Util.GetBestMasterName(master) + " | " + GetItemCount(master);
            self.moneyText.enableWordWrapping = true;
        }

        private void SetItemCountInMoney(
            On.RoR2.UI.ScoreboardStrip.orig_Update orig,
            RoR2.UI.ScoreboardStrip self
        )
        {
            orig(self);

            var master = self.GetFieldValue<CharacterMaster>("master");
            self.moneyText.text = string.Format("{0} Items   ${1}", GetItemCount(master), master.money);
        }

        public void Awake()
        {
            On.RoR2.UI.ScoreboardStrip.SetMaster += SetItemCountDisplay;

            On.RoR2.UI.ScoreboardStrip.Update += SetItemCountInMoney;
        }

        public void Destroy()
        {
            On.RoR2.UI.ScoreboardStrip.SetMaster -= SetItemCountDisplay;
        }
    }
}