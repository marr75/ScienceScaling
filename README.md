# ScienceScaling

BepInEx plugin for the Unity game **Solar Expanse** that rebalances the research-rate formula.

Vanilla `ResearchManager.GetResearchPointPerHour` adds each lab's bonus additively to the base
research rate. ScienceScaling replaces that with a multiplicative lab term (`1 + k * L`) via a
Harmony prefix, and exposes a global rate multiplier so overall research tempo can be tuned
independently of lab strength. Config (`BepInEx/config/marr75.solarexpanse.sciencescaling.cfg`):

- `Balance.Enabled` — gate the prefix; `false` runs the vanilla additive formula unchanged.
- `Balance.BaseRateMultiplier` — global research-tempo scalar (1.0 = vanilla).
- `Balance.LabContributionPerPoint` — `k`, the multiplicative contribution per point of lab bonus.

## Build

Requires .NET SDK (net48 target) and a Solar Expanse install.

```powershell
$env:SOLAR_EXPANSE_DIR = 'C:\path\to\Solar Expanse'
dotnet build
```

`SOLAR_EXPANSE_DIR` (or `-p:GameDir=...` on the CLI) locates the game's managed DLLs and the
BepInEx plugins folder. A post-build target copies the built DLL to
`$(GameDir)\BepInEx\plugins\ScienceScaling\`.

## License

MIT (see LICENSE).
