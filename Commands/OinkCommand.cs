using Vintagestory.API.Common;
using Yapper.Ports;

namespace Yapper.Commands;

public class OinkCommand : ISfxCommand
{
    public string Name => "oink";
    public string Description => "Oink like a pig!";
    public string FlavorText => "oinked!";
    public float Range => 48f;
    public float Volume => 1.5f;
    public string[] Variants => [];
    public string DefaultVariant => "";

    private static readonly SfxSound[] _sounds =
    [
        new(new AssetLocation("sounds/creature/pig/idle"), 1600),
    ];

    public SfxSound[] GetSounds(string? variant) => _sounds;
}
