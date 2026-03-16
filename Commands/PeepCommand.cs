using Vintagestory.API.Common;
using Yapper.Ports;

namespace Yapper.Commands;

public class PeepCommand : ISfxCommand
{
    public string Name => "peep";
    public string Description => "Peep like a little bird!";
    public string FlavorText => "peeped!";
    public float Range => 48f;
    public float Volume => 1f;
    public string[] Variants => [];
    public string DefaultVariant => "";

    private static readonly SfxSound[] _sounds =
    [
        new(new AssetLocation("sounds/creature/coqui"), 800),
    ];

    public SfxSound[] GetSounds(string? variant) => _sounds;
}
