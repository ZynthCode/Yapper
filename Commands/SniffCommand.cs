using Vintagestory.API.Common;
using Yapper.Ports;

namespace Yapper.Commands;

public class SniffCommand : ISfxCommand
{
    public string Name => "sniff";
    public string Description => "Sniff around!";
    public string FlavorText => "sniffed!";
    public float Range => 48f;
    public float Volume => 1.5f;
    public string[] Variants => [];
    public string DefaultVariant => "";

    private static readonly SfxSound[] _sounds =
    [
        new(new AssetLocation("sounds/creature/hooved/generic/sniff"), 500),
    ];

    public SfxSound[] GetSounds(string? variant) => _sounds;
}
