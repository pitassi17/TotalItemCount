using BepInEx;
using RoR2;
using UnityEngine;

namespace totalItemCount
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

            Chat.AddMessage("Loaded Test!");
            //Inventory.GetItemCount(ItemIndex);

            On.RoR2.CharacterBody.OnInventoryChanged += (orig, self) =>
            {
                //Inventory inv = CharacterMaster.Inventory;
                for (int i = 0; i < CharacterMaster.readOnlyInstancesList.Count; i++)
                {
                    CharacterMaster player = CharacterMaster.readOnlyInstancesList[i];
                    if (player.teamIndex == TeamIndex.Player && player.minionOwnership.group == null)
                    {
                        itemCount = player.inventory.itemAcquisitionOrder.Count;
                    }
                }

                orig(self);
            };
        }
    }
}