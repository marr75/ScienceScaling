using BepInEx.Configuration;

namespace ScienceScaling.Config;

sealed class Configuration {
    public readonly ConfigEntry<bool> Enabled;
    public readonly ConfigEntry<float> LabBonusStrength;
    public readonly ConfigEntry<float> ResearchSpeedMultiplier;

    public Configuration(ConfigFile c) {
        const string enabledDescription =
            "Turns this mod's research math on or off. Off = research works exactly like unmodified "
            + "Solar Expanse.";
        Enabled = c.Bind(
            "Balance",
            "Enabled",
            true,
            enabledDescription
        );
        const string researchSpeedMultiplierDescription =
            "An overall speed dial for research. 1.0 = vanilla pace. 2.0 = every research project "
            + "finishes twice as fast. This scales all research evenly; it doesn't change how much "
            + "labs matter relative to each other, only how fast the whole system runs.";
        ResearchSpeedMultiplier = c.Bind(
            "Balance",
            "ResearchSpeedMultiplier",
            1.0f,
            new ConfigDescription(
                researchSpeedMultiplierDescription,
                new AcceptableValueRange<float>(0.1f, 20f)
            )
        );
        const string labBonusStrengthDescription =
            "How much each lab's bonus is worth. Every lab has a built-in \"lab bonus\" rating under "
            + "the hood (vanilla's research laboratory is rated at 3 points); this setting is the "
            + "slice of your base research rate that each of those points adds. At the default 0.02, "
            + "each point is worth +2%, so one vanilla lab (3 points) gives +6%. The bonus is a "
            + "straight sum over your labs, so a second lab brings the total to +12% - and since only "
            + "one lab fits per body, that second one has to go on another world. Data mods can "
            + "change the rating: Teddit's Research Labs Rework, for instance, uses 6. Turn this up to "
            + "make lab investment pay off faster; turn it down (or to 0) to make labs matter less.";
        LabBonusStrength = c.Bind(
            "Balance",
            "LabBonusStrength",
            0.02f,
            new ConfigDescription(
                labBonusStrengthDescription,
                new AcceptableValueRange<float>(0f, 1f)
            )
        );
    }
}
