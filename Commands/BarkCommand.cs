using System.Collections.Generic;
using Vintagestory.API.Common;
using Yapper.Ports;

namespace Yapper.Commands;

public class BarkCommand : ISfxCommand
{
    public string Name => "bark";
    public string Description => "Bark like a wolf!";
    public string FlavorText => "barked!";
    public float Range => 48f;
    public float Volume => 1f;
    public string[] Variants => ["adult", "pup"];
    public string DefaultVariant => "adult";

    private static readonly Dictionary<string, SfxSound[]> _variants = new()
    {
        ["adult"] =
        [
            new(new AssetLocation("sounds/creature/wolf/attack"), 1500),
        ],
        ["pup"] =
        [
            new(new AssetLocation("sounds/creature/wolf/pup-bark"), 1300),
        ],
    };

    public SfxSound[] GetSounds(string? variant)
    {
        if (variant != null && _variants.TryGetValue(variant, out var sounds))
            return sounds;
        return _variants[DefaultVariant];
    }
}
