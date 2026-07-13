using BepInEx.Configuration;

namespace ScienceScaling.Config;

sealed class Configuration {
    public readonly ConfigEntry<bool> Enabled;
    public readonly ConfigEntry<float> ResearchSpeedMultiplier;
    public readonly ConfigEntry<float> LabBonusStrength;

    public Configuration(ConfigFile c) {
        const string enabledDescription = "Turns this mod's research math on or off. Off = research works exactly like unmodified "
            + "Solar Expanse.";
        Enabled = c.Bind(
            "Balance",
            "Enabled",
            true,
            enabledDescription
        );
        const string researchSpeedMultiplierDescription = "An overall speed dial for research. 1.0 = vanilla pace. 2.0 = every research project "
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
        const string labBonusStrengthDescription = "How much each lab's bonus is worth. Every lab has a built-in \"lab bonus\" rating under "
            + "the hood (a Mk1 lab is rated at 6 points); this setting is the slice of your base "
            + "research rate that each of those points adds. At the default 0.02, each point is worth "
            + "+2%, so one Mk1 lab (6 points) alone gives +12%, and a second identical lab compounds "
            + "on top of that for +24% combined. Turn this up to make lab investment pay off faster; "
            + "turn it down (or to 0) to make labs matter less.";
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
