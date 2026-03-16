using Vintagestory.API.Common;
using Yapper.Ports;

namespace Yapper.Commands;

public class CluckCommand : ISfxCommand
{
    public string Name => "cluck";
    public string Description => "Cluck like a chicken!";
    public string FlavorText => "clucked!";
    public float Range => 48f;
    public float Volume => 2f;
    public string[] Variants => [];
    public string DefaultVariant => "";

    private static readonly SfxSound[] _sounds =
    [
        new(new AssetLocation("sounds/creature/chicken/hen-idle1"), 1200),
        new(new AssetLocation("sounds/creature/chicken/hen-idle2"), 1800),
        new(new AssetLocation("sounds/creature/chicken/hen-idle3"), 1200),
        new(new AssetLocation("sounds/creature/chicken/hen-idle4"), 2100),
    ];

    public SfxSound[] GetSounds(string? variant) => _sounds;
}
