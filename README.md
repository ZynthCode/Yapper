# Yapper

A [Vintage Story](https://www.vintagestory.at/) mod that adds 16 animal sound effect commands, letting players howl, growl, bark, roar, and more using in-game creature sounds.

## Features

- **16 animal sound commands** from wolves, bears, foxes, elk, chickens, and more
- **Variant system** for commands with multiple animal sources (toggle between wolf/fox howls, etc.)
- **Proximity-based flavor text** broadcasts to nearby players within range (approx 40-50 block distance)
- **Per-player variant persistence** saved to config file across sessions
- **Cooldown system** based on sound duration to prevent spam

## Requirements

- Vintage Story 1.21.0+

## Installation

1. Download Yapper from the [releases page](https://github.com/zynthcode/yapper/releases)
2. Place the Yapper ZIP in your Vintage Story `Mods` folder
3. Restart the server

## SFX Commands

| Command | Description | Variants |
|---------|-------------|----------|
| `/awoo` | Howl like a wolf! | **wolf** (default), fox |
| `/growl` | Growl like a beast! | **wolf** (default), bear, fox, hyena |
| `/roar` | Roar like a bear! |  |
| `/bark` | Bark like a wolf! | **adult** (default), pup |
| `/bellow` | Bellow like a mighty beast! | **all** (default), elk, redstag |
| `/bugle` | Bugle like an elk! |  |
| `/baa` | Baa like a sheep! |  |
| `/bleat` | Bleat like a baby animal! |  |
| `/cluck` | Cluck like a chicken! |  |
| `/crow` | Crow like a rooster! |  |
| `/oink` | Oink like a pig! |  |
| `/laugh` | Laugh like a hyena! |  |
| `/chirp` | Chirp like an insect! |  |
| `/peep` | Peep like a little bird! |  |
| `/chatter` | Chatter like a raccoon! |  |
| `/sniff` | Sniff around! |  |

## Utility Commands

| Command | Description |
|---------|-------------|
| `/yapper help` | Show available commands and usage |
| `/yapper version` | Show mod version |
| `/yapper config` | Show your current variant settings |
| `/yapper play [sound] [path]` | Play an arbitrary game sound (admin only) |

## Variant System

Four commands support multiple animal variants: `/awoo`, `/growl`, `/bark`, and `/bellow`. Each player can toggle between variants using the toggle subcommand.

For example, to switch your `/awoo` from wolf to fox:

```
/awoo toggle
```

Each toggle cycles to the next variant in the list. Your selection is saved per-player and persists across sessions.

| Command | Variants | Default |
|---------|----------|---------|
| `/awoo` | wolf, fox | wolf |
| `/growl` | wolf, bear, fox, hyena | wolf |
| `/bark` | adult, pup | adult |
| `/bellow` | all, elk, redstag | all |

## Configuration

Player variant preferences are stored in `YapperConfig.json` in the mod's config directory. The file maps each player's chosen variant per command and is updated automatically when players use the toggle command.

---

## Development Setup

### Prerequisites

- **.NET 8.0 SDK** - [Download](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- **Vintage Story** installed (for API reference and testing)
- **`VINTAGE_STORY` environment variable** pointing to your Vintage Story installation

### Building

```powershell
dotnet build
```

### Installing During Development

Use the included install script to build, zip, and deploy to your Mods folder in one step:

```powershell
.\install.ps1
```

Restart Vintage Story after installing to load the new build.

### Testing

Use `/yapper version` to verify the mod loaded. Try the various SFX commands and use `/awoo toggle` to test variant cycling.

## MIT License

See [LICENSE](LICENSE) for details.
