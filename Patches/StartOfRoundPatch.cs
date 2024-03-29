﻿using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCChangeReroutePurchase.Patches
{
    [HarmonyPatch(typeof(StartOfRound))]
    internal class StartOfRoundPatch
    {
        [HarmonyPatch("EndOfGame")]
        [HarmonyPostfix]
        static void GetCurrentDay(ref StartOfRound __instance, ref EndOfGameStats ___gameStats)
        {
            // Change the current days spent every end of a round
            SharedData.currentDaysSpent = ___gameStats.daysSpent;
        }
        [HarmonyPatch("SetTimeAndPlanetToSavedSettings")]
        [HarmonyPostfix]
        static void LoadCurrentDay(ref StartOfRound __instance, ref EndOfGameStats ___gameStats)
        {
            // Change the current days spent every time a new save is loaded
            SharedData.currentDaysSpent = ___gameStats.daysSpent;
        }
    }
}
