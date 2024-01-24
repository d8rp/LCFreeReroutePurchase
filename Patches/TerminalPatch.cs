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
        static void OnLoadNewNodeIfAffordable(ref Terminal __instance, TerminalNode node)
        {
            if (node.buyRerouteToMoon != -1)
            {
                if (SharedData.currentDaysSpent < TutorialModBase.Instance.configFreeRerouteUntil.Value)
                    node.itemCost = TutorialModBase.Instance.configRerouteCostBefore.Value;
                else
                    node.itemCost = TutorialModBase.Instance.configRerouteCostAfter.Value;
            }
        }
        [HarmonyPatch("LoadNewNode")]
        [HarmonyPrefix]
        static void OnLoadNewNode(ref Terminal __instance, TerminalNode node, ref int ___totalCostOfItems)
        {
            if (node.buyRerouteToMoon != -1)
            {
                if (SharedData.currentDaysSpent < TutorialModBase.Instance.configFreeRerouteUntil.Value)
                    ___totalCostOfItems = TutorialModBase.Instance.configRerouteCostBefore.Value;
                else
                    ___totalCostOfItems = TutorialModBase.Instance.configRerouteCostAfter.Value;
            }
        }
    }
}
