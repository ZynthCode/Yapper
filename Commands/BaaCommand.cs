using Vintagestory.API.Common;
using Yapper.Ports;

namespace Yapper.Commands;

public class BaaCommand : ISfxCommand
{
    public string Name => "baa";
    public string Description => "Baa like a sheep!";
    public string FlavorText => "baa'd!";
    public float Range => 48f;
    public float Volume => 1f;
    public string[] Variants => [];
    public string DefaultVariant => "";

    private static readonly SfxSound[] _sounds =
    [
        new(new AssetLocation("sounds/creature/goat/bleat1"), 1100),
        new(new AssetLocation("sounds/creature/goat/bleat2"), 1100),
        new(new AssetLocation("sounds/creature/goat/bleat3"), 1100),
        new(new AssetLocation("sounds/creature/goat/bleat4"), 1100),
        new(new AssetLocation("sounds/creature/goat/bleatweird"), 1100),
    ];

    public SfxSound[] GetSounds(string? variant) => _sounds;
}
