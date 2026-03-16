using Vintagestory.API.Common;
using Yapper.Ports;

namespace Yapper.Commands;

public class ChatterCommand : ISfxCommand
{
    public string Name => "chatter";
    public string Description => "Chatter like a raccoon!";
    public string FlavorText => "chattered!";
    public float Range => 48f;
    public float Volume => 1f;
    public string[] Variants => [];
    public string DefaultVariant => "";

    private static readonly SfxSound[] _sounds =
    [
        new(new AssetLocation("sounds/creature/raccoon/idle"), 1600),
        new(new AssetLocation("sounds/creature/raccoon/aggro"), 2000),
    ];

    public SfxSound[] GetSounds(string? variant) => _sounds;
}
