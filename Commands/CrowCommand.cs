using Vintagestory.API.Common;
using Yapper.Ports;

namespace Yapper.Commands;

public class CrowCommand : ISfxCommand
{
    public string Name => "crow";
    public string Description => "Crow like a rooster!";
    public string FlavorText => "crowed!";
    public float Range => 48f;
    public float Volume => 1.5f;
    public string[] Variants => [];
    public string DefaultVariant => "";

    private static readonly SfxSound[] _sounds =
    [
        new(new AssetLocation("sounds/creature/chicken/rooster-call"), 2200),
    ];

    public SfxSound[] GetSounds(string? variant) => _sounds;
}
