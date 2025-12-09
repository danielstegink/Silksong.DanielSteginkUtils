using BepInEx;
using HarmonyLib;

namespace DanielSteginkUtils;

/// <summary>
/// Root plugin
/// </summary>
[BepInAutoPlugin(id: "io.github.danielstegink.danielsteginkutils")]
public partial class DanielSteginkUtils : BaseUnityPlugin
{
    /// <summary>
    /// Static instance to make calling Log easier
    /// </summary>
    internal static DanielSteginkUtils Instance { get; private set; }

    private void Awake()
    {
        // Put your initialization logic here
        Instance = this;

        Harmony harmony = new Harmony(Id);
        harmony.PatchAll();

        Logger.LogInfo($"Plugin {Name} ({Id}) has loaded!");
    }

    /// <summary>
    /// Shared log method for the entire assembly
    /// </summary>
    /// <param name="message"></param>
    internal void Log(string message)
    {
        Logger.LogInfo(message);
    }
}