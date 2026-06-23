using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;

namespace ScienceScaling;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
public class Plugin : BaseUnityPlugin {
    internal static ManualLogSource Log = null!;

    // Gates the prefix. false => prefix returns true, vanilla additive formula runs.
    internal static ConfigEntry<bool> Enabled = null!;

    // Global research-tempo scalar applied to the live researchPoint1PerHour.
    internal static ConfigEntry<float> BaseRateMultiplier = null!;

    // k — each point of summed GetBonusFromLab adds this fraction of the base rate.
    internal static ConfigEntry<float> LabContributionPerPoint = null!;

    void Awake() {
        Log = Logger;

        Enabled = Config.Bind(
            "Balance",
            "Enabled",
            true,
            "Gates the research-rate prefix. false = prefix returns true and the vanilla additive "
            + "formula runs (byte-identical parity)."
        );

        BaseRateMultiplier = Config.Bind(
            "Balance",
            "BaseRateMultiplier",
            1.0f,
            "Global research-tempo scalar applied to the live researchPoint1PerHour. 1.0 = vanilla "
            + "base rate; 2.0 = twice as fast overall. Scales every research's per-hour rate linearly "
            + "without distorting the lab/speed-tech balance shape."
        );

        LabContributionPerPoint = Config.Bind(
            "Balance",
            "LabContributionPerPoint",
            0.02f,
            "k — each point of summed GetBonusFromLab adds this fraction of the base rate "
            + "(Mk1=6 => +12% per lab). Exposed so a future standalone (non-Teddit) release has a "
            + "player-facing lab-strength knob. A future optional cap on the max lab multiplier "
            + "(1 + k*L clamped) is not yet implemented."
        );

        Log.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} loaded.");
        new Harmony(MyPluginInfo.PLUGIN_GUID).PatchAll();
    }
}
