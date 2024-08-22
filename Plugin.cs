using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using LCChangeReroutePurchase.Patches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Netcode;

namespace LCChangeReroutePurchase
{

    [BepInPlugin(modGUID, modName, modVersion)]
    public class TutorialModBase : BaseUnityPlugin
    {
        private const string modGUID = "LCChangeReroutePurchase";
        private const string modName = "Tutorial Mod";
        private const string modVersion = "1.0.2.0";

        private readonly Harmony harmony = new Harmony(modGUID);

        public static TutorialModBase Instance { get; private set; }

        internal static ManualLogSource mls;

        // Configs
        public ConfigEntry<int> configRerouteCostBefore;
        public ConfigEntry<int> configRerouteCostAfter;
        public ConfigEntry<int> configFreeRerouteUntil;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            mls = BepInEx.Logging.Logger.CreateLogSource(modGUID);

            mls.LogInfo("Free reroute purchase has awakened :D");

            configRerouteCostBefore = Config.Bind("General",      // The section under which the option is shown
                                            "DefaultCostBefore",  // The key of the configuration option in the configuration file
                                            0,                      // The default value
                                            "The cost to reroute to ALL moons before the date in [FreeRerouteUntilInDays]."); // Description of the option to show in the config file

            configRerouteCostAfter = Config.Bind("General",      // The section under which the option is shown
                                            "DefaultCostAfter",  // The key of the configuration option in the configuration file
                                            0,                // The default value
                                            "The cost to reroute to ALL moons after the date in [RerouteUntilInDays]."); // Description of the option to show in the config file

            configFreeRerouteUntil = Config.Bind("General",                 // The section under which the option is shown
                                                 "RerouteUntilInDays", // The key of the configuration option in the configuration file
                                                 3,                         // The default value
                                                 "The number of days until reroute pirce change."); // Description of the option to show in the config file

            harmony.PatchAll(typeof(StartOfRoundPatch));
            harmony.PatchAll(typeof(TerminalPatch));
            harmony.PatchAll(typeof(TutorialModBase));
        }
        private bool IsHost()
        {
            return ((NetworkBehaviour)RoundManager.Instance).IsHost;
        }
    }
}
