using Vintagestory.API.Common;
using Yapper.Ports;

namespace Yapper.Commands;

public class BleatCommand : ISfxCommand
{
    public string Name => "bleat";
    public string Description => "Bleat like a baby animal!";
    public string FlavorText => "bleated!";
    public float Range => 48f;
    public float Volume => 1f;
    public string[] Variants => [];
    public string DefaultVariant => "";

    private static readonly SfxSound[] _sounds =
    [
        new(new AssetLocation("sounds/creature/goat/goatkidbleat1"), 1100),
        new(new AssetLocation("sounds/creature/goat/goatkidbleat2"), 1100),
        new(new AssetLocation("sounds/creature/goat/goatkidbleat3"), 1100),
        new(new AssetLocation("sounds/creature/hooved/small/fawn/fawn1"), 1100),
        new(new AssetLocation("sounds/creature/hooved/small/fawn/fawn2"), 1100),
    ];

    public SfxSound[] GetSounds(string? variant) => _sounds;
}
