using Vintagestory.API.Common;
using Yapper.Ports;

namespace Yapper.Commands;

public class RoarCommand : ISfxCommand
{
    public string Name => "roar";
    public string Description => "Roar like a bear!";
    public string FlavorText => "roared!";
    public float Range => 48f;
    public float Volume => 1f;
    public string[] Variants => [];
    public string DefaultVariant => "";

    private static readonly SfxSound[] _sounds =
    [
        new(new AssetLocation("sounds/creature/bear/attack"), 1300),
    ];

    public SfxSound[] GetSounds(string? variant) => _sounds;
}
