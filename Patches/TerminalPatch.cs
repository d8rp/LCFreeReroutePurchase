using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCFreeReroutePurchase.Patches
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
                if (SharedData.currentDaysSpent < 3)
                    node.itemCost = 0;
                else
                    node.itemCost = 500;
            }
        }
        [HarmonyPatch("LoadNewNode")]
        [HarmonyPrefix]
        static void OnLoadNewNode(ref Terminal __instance, TerminalNode node, ref int ___totalCostOfItems)
        {
            if (node.buyRerouteToMoon != -1)
            {
                if (SharedData.currentDaysSpent < 3)
                    ___totalCostOfItems = 0;
                else
                    ___totalCostOfItems = 500;
            }
        }
    }
}
