using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using ScienceScaling.Config;
using ScienceScaling.Core;

namespace ScienceScaling;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
public class Plugin : BaseUnityPlugin {
    internal static ManualLogSource Log = null!;

    void Awake() {
        Log = Logger;
        Services.Init(new Configuration(Config)); // must precede patching: patch reads Services.Config

        Log.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} loaded.");
        new Harmony(MyPluginInfo.PLUGIN_GUID).PatchAll();
    }
}
