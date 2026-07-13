using HarmonyLib;
using Manager;
using ScienceScaling.Core;
using ScriptableObjectScripts;
using UnityEngine;

namespace ScienceScaling.Patches;

// Recomputes GetResearchPointPerHour with the lab term as a multiplier (1 + k*L) instead of the
// vanilla additive (+ L), and scales the base rate by ResearchSpeedMultiplier. Gated by Enabled:
// when off the prefix returns true and the stock additive formula runs unchanged.
[HarmonyPatch(typeof(ResearchManager), nameof(ResearchManager.GetResearchPointPerHour))]
static class ResearchRateMultiplicativeLabsPatch {
    [HarmonyPrefix]
    static bool Prefix(ResearchManager __instance, ResearchDefinition rd, ref float __result) {
        if (!Services.Config.Enabled.Value) { return true; }

        var s = Mathf.Clamp(__instance.company.BonusController.GetBonus(EBonus.ResearchProduction), 0.1f, 1f);
        var num = __instance.researchPoint1PerHour * Services.Config.ResearchSpeedMultiplier.Value / s;
        if (__instance.bonusFromObservatory > 0.0) { num *= 2f; }
        var k = Services.Config.LabBonusStrength.Value;
        var result = num * (1f + k * __instance.company.BonusController.GetBonusFromLab(rd));
        if (DevConsoleCommands.speedResearchMultiplayer.HasValue) {
            result *= DevConsoleCommands.speedResearchMultiplayer.Value;
        }
        __result = result;
        return false;
    }
}
