using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using LCFreeReroutePurchase.Patches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCFreeReroutePurchase
{

    [BepInPlugin(modGUID, modName, modVersion)]
    public class TutorialModBase : BaseUnityPlugin
    {
        private const string modGUID = "Derp.TutorialMod";
        private const string modName = "Tutorial Mod";
        private const string modVersion = "1.0.1.0";

        private readonly Harmony harmony = new Harmony(modGUID);

        private static TutorialModBase Instance;

        internal ManualLogSource mls;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            mls = BepInEx.Logging.Logger.CreateLogSource(modGUID);

            mls.LogInfo("Free reroute purchase has awaken :D");

            harmony.PatchAll(typeof(StartOfRoundPatch));
            harmony.PatchAll(typeof(TerminalPatch));
            harmony.PatchAll(typeof(TutorialModBase));
        }
    }
}
