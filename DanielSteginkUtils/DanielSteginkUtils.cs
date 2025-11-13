using BepInEx;
using DanielSteginkUtils.Settings;
using HarmonyLib;

namespace DanielSteginkUtils
{
    [BepInPlugin(PluginSettings.GUID, PluginSettings.NAME, PluginSettings.VERSION)]
    public class DanielSteginkUtils : BaseUnityPlugin
    {
        internal static DanielSteginkUtils Instance;

        public void Awake()
        {
            Logger.LogInfo("Initializing");
            Instance = this;

            Harmony harmony = new Harmony(PluginSettings.GUID);
            harmony.PatchAll();

            Logger.LogInfo("Initialized");
        }
    }
}