using Vintagestory.API.Common;
using Yapper.Ports;

namespace Yapper.Commands;

public class ChirpCommand : ISfxCommand
{
    public string Name => "chirp";
    public string Description => "Chirp like an insect!";
    public string FlavorText => "chirped!";
    public float Range => 48f;
    public float Volume => 1f;
    public string[] Variants => [];
    public string DefaultVariant => "";

    private static readonly SfxSound[] _sounds =
    [
        new(new AssetLocation("sounds/creature/grasshopper"), 1800),
    ];

    public SfxSound[] GetSounds(string? variant) => _sounds;
}
