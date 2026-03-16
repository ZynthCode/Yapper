using Vintagestory.API.Common;
using Yapper.Ports;

namespace Yapper.Commands;

public class LaughCommand : ISfxCommand
{
    public string Name => "laugh";
    public string Description => "Laugh like a hyena!";
    public string FlavorText => "laughed!";
    public float Range => 48f;
    public float Volume => 1f;
    public string[] Variants => [];
    public string DefaultVariant => "";

    private static readonly SfxSound[] _sounds =
    [
        new(new AssetLocation("sounds/creature/hyena/laugh"), 2000),
    ];

    public SfxSound[] GetSounds(string? variant) => _sounds;
}
