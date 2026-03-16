using Vintagestory.API.Common;
using Yapper.Ports;

namespace Yapper.Commands;

public class BugleCommand : ISfxCommand
{
    public string Name => "bugle";
    public string Description => "Bugle like an elk!";
    public string FlavorText => "bugled!";
    public float Range => 48f;
    public float Volume => 1f;
    public string[] Variants => [];
    public string DefaultVariant => "";

    private static readonly SfxSound[] _sounds =
    [
        new(new AssetLocation("sounds/creature/hooved/large/elk/bugle1"), 3000),
        new(new AssetLocation("sounds/creature/hooved/large/elk/bugle2"), 2200),
    ];

    public SfxSound[] GetSounds(string? variant) => _sounds;
}
