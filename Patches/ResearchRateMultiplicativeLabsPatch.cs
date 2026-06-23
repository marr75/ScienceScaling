using HarmonyLib;
using Manager;
using ScriptableObjectScripts;
using UnityEngine;

namespace ScienceScaling.Patches;

// Recomputes GetResearchPointPerHour with the lab term as a multiplier (1 + k*L) instead of the
// vanilla additive (+ L), and scales the base rate by BaseRateMultiplier. Gated by Plugin.Enabled:
// when off the prefix returns true and the stock additive formula runs unchanged.
[HarmonyPatch(typeof(ResearchManager), nameof(ResearchManager.GetResearchPointPerHour))]
static class ResearchRateMultiplicativeLabsPatch {
    [HarmonyPrefix]
    static bool Prefix(ResearchManager __instance, ResearchDefinition rd, ref float __result) {
        if (!Plugin.Enabled.Value) {
            return true;
        }

        var s = Mathf.Clamp(__instance.company.BonusController.GetBonus(EBonus.ResearchProduction), 0.1f, 1f);
        var num = (float)__instance.researchPoint1PerHour * Plugin.BaseRateMultiplier.Value / s;
        if ((double)__instance.bonusFromObservatory > 0.0) {
            num *= 2f;
        }
        var k = Plugin.LabContributionPerPoint.Value;
        var result = num * (1f + k * __instance.company.BonusController.GetBonusFromLab(rd));
        if (DevConsoleCommands.speedResearchMultiplayer.HasValue) {
            result *= (float)DevConsoleCommands.speedResearchMultiplayer.Value;
        }
        __result = result;
        return false;
    }
}
