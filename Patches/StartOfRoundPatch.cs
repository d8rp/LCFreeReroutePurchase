using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCFreeReroutePurchase.Patches
{
    [HarmonyPatch(typeof(StartOfRound))]
    internal class StartOfRoundPatch
    {
        [HarmonyPatch("EndOfGame")]
        [HarmonyPostfix]
        static void GetCurrentDay(ref StartOfRound __instance, EndOfGameStats gameStats)
        {
            // Change the current days spent every end of a round
            SharedData.currentDaysSpent = gameStats.daysSpent;
        }
        [HarmonyPatch("SetTimeAndPlanetToSavedSettings")]
        [HarmonyPostfix]
        static void LoadCurrentDay(ref StartOfRound __instance, EndOfGameStats gameStats)
        {
            // Change the current days spent every time a new save is loaded
            SharedData.currentDaysSpent = gameStats.daysSpent;
        }
    }
}
