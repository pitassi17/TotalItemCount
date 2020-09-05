using BepInEx;
using RoR2;
using UnityEngine;

namespace TotalItemCount
{
    [BepInDependency("com.bepis.r2api")]
    //Change these
    [BepInPlugin("com.theOneTrueGod.TotalItemCount", "Gives total item count", "1.0.0")]
    public class TotalItemCount : BaseUnityPlugin
    {
        private int itemCount = 0;
        private Transform HUDRoot = null;
        public void Awake()
        {
            On.RoR2.UI.ScoreboardStrip.SetMaster += addTotalItemCount;
        }

        private void addTotalItemCount(On.RoR2.UI.ScoreboardStrip.orig_SetMaster orig, RoR2.UI.ScoreboardStrip self, CharacterMaster master)
        {
            orig(self, master);

            Chat.AddMessage("hello the scoreboard is up");
            int itemCount = 0;
            for (ItemTier i = 0; i <= ItemTier.Boss; i++)
            {
                itemCount += master.inventory.GetTotalItemCountOfTier(i);
            }

            self.nameLabel.text = Util.GetBestMasterName(master) + " | " + itemCount;
        }
    }
}