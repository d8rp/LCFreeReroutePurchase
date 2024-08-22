using BepInEx.Logging;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCChangeReroutePurchase.Patches
{
    [HarmonyPatch(typeof(Terminal))]
    internal class TerminalPatch
    {
        [HarmonyPatch("LoadNewNodeIfAffordable")]
        [HarmonyPrefix]
        static bool OnLoadNewNodeIfAffordable(ref Terminal __instance, TerminalNode node)
        {
            if (node.buyRerouteToMoon != -1 || node.buyVehicleIndex != -1)
            {
                if (SharedData.currentDaysSpent < TutorialModBase.Instance.configFreeRerouteUntil.Value)
                    node.itemCost = TutorialModBase.Instance.configRerouteCostBefore.Value;
                else
                    node.itemCost = TutorialModBase.Instance.configRerouteCostAfter.Value;
            }
            return true;
        }
        [HarmonyPatch("LoadNewNode")]
        [HarmonyPrefix]
        static bool OnLoadNewNode(ref Terminal __instance, TerminalNode node, ref int ___totalCostOfItems)
        {
            if (node.buyRerouteToMoon != -1) // || node.buyVehicleIndex != -1
            {
                if (SharedData.currentDaysSpent < TutorialModBase.Instance.configFreeRerouteUntil.Value)
                    ___totalCostOfItems = TutorialModBase.Instance.configRerouteCostBefore.Value;
                else
                    ___totalCostOfItems = TutorialModBase.Instance.configRerouteCostAfter.Value;
            }
            return true;
        }
        [HarmonyPatch("TextPostProcess")]
        [HarmonyPrefix]
        static bool OnTextPostProcess(ref Terminal __instance, ref string modifiedDisplayText, TerminalNode node, ref BuyableVehicle[] ___buyableVehicles)
        {
            if (modifiedDisplayText.Contains("[buyableVehiclesList]"))
            {
                if (___buyableVehicles == null || ___buyableVehicles.Length == 0)
                {
                    modifiedDisplayText = modifiedDisplayText.Replace("[buyableVehiclesList]", "[No items in stock!]");
                }
                else
                {
                    StringBuilder stringBuilder3 = new StringBuilder();
                    for (int l = 0; l < ___buyableVehicles.Length; l++)
                    {
                        stringBuilder3.Append("\n* " + ___buyableVehicles[l].vehicleDisplayName + "  //  Price: $0");
                    }
                    modifiedDisplayText = modifiedDisplayText.Replace("[buyableVehiclesList]", stringBuilder3.ToString());
                }
            }
            return true;
        }
    }
}
