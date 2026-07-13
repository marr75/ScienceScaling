# Science Scaling

> Labs stop being a flat bonus and start being a multiplier — the more/better labs you build, the faster every point of research compounds.

<!-- SCREENSHOT: hero shot — research panel showing a high research rate with several labs built. File: docs/images/sciencescaling-hero.png -->

## What it does

- Turns each lab's contribution into a percentage boost on top of your current research rate, instead of vanilla's flat point-per-hour add-on. Building a second or third lab is worth noticeably more than the first.
- Gives you one dial (`BaseRateMultiplier`) to speed up or slow down all research across the board, without touching how labs stack against each other.
- Ships with a full on/off switch — flip it off and research behaves exactly like unmodified Solar Expanse.

## Before / after

Vanilla: each lab adds a fixed number of research points per hour, so as your base research rate grows from tech and observatories, each additional lab matters *less* in relative terms. Science Scaling: each lab's bonus is applied as a multiplier on your current rate, so labs stay meaningful (and start compounding) no matter how far your research program has scaled up.

## Configuration

Settings live in `BepInEx/config/marr75.solarexpanse.sciencescaling.cfg` and are editable in-game if you have Configuration Manager installed.

- **`Enabled`** — master on/off switch. Off = vanilla research math, unchanged.
- **`BaseRateMultiplier`** — an overall speed dial for research. 1.0 is vanilla pace; 2.0 doubles every research rate; 0.5 halves it. It scales the whole system evenly, so it doesn't change how much labs matter relative to each other.
- **`LabContributionPerPoint`** — how much punch each lab's bonus packs. Every lab has a built-in "lab bonus" rating (a Mk1 lab is worth 6 points, for example); this setting is the percentage of your base rate that each of those points adds. Higher values make investing in labs pay off faster.

<!-- SCREENSHOT: config panel showing the three Science Scaling settings. File: docs/images/sciencescaling-config.png -->

## Requirements

- Solar Expanse + BepInEx 5 (Mono/x64).

## Install

1. Install BepInEx 5.
2. Drop the `ScienceScaling` folder into `BepInEx/plugins/`.

## Building (developers)

`dotnet build` deploys the DLL to the game's plugins folder via the post-build target. See `AGENTS.md`.
