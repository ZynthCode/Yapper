using System.Collections.Generic;
using Vintagestory.API.Common;
using Yapper.Ports;

namespace Yapper.Commands;

public class GrowlCommand : ISfxCommand
{
    public string Name => "growl";
    public string Description => "Growl like a beast!";
    public string FlavorText => "growled!";
    public float Range => 48f;
    public float Volume => 1f;
    public string[] Variants => ["wolf", "bear", "fox", "hyena"];
    public string DefaultVariant => "wolf";

    private static readonly Dictionary<string, SfxSound[]> _variants = new()
    {
        ["bear"] =
        [
            new(new AssetLocation("sounds/creature/bear/growl1"), 3900),
            new(new AssetLocation("sounds/creature/bear/growl2"), 4000),
        ],
        ["wolf"] =
        [
            new(new AssetLocation("sounds/creature/wolf/growl"), 2600),
        ],
        ["fox"] =
        [
            new(new AssetLocation("sounds/creature/fox/growl"), 2000),
        ],
        ["hyena"] =
        [
            new(new AssetLocation("sounds/creature/hyena/growl"), 4000),
        ],
    };

    public SfxSound[] GetSounds(string? variant)
    {
        if (variant != null && _variants.TryGetValue(variant, out var sounds))
            return sounds;
        return _variants[DefaultVariant];
    }
}
