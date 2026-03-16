using System.Collections.Generic;
using Vintagestory.API.Common;
using Yapper.Ports;

namespace Yapper.Commands;

public class AwooCommand : ISfxCommand
{
    public string Name => "awoo";
    public string Description => "Howl like a wolf!";
    public string FlavorText => "awooed!";
    public float Range => 48f;
    public float Volume => 1f;
    public string[] Variants => ["wolf", "fox"];
    public string DefaultVariant => "wolf";

    private static readonly Dictionary<string, SfxSound[]> _variants = new()
    {
        ["wolf"] =
        [
            new(new AssetLocation("sounds/creature/wolf/howl1"), 4900),
            new(new AssetLocation("sounds/creature/wolf/howl2"), 4000),
            new(new AssetLocation("sounds/creature/wolf/howl3"), 2700),
        ],
        ["fox"] =
        [
            new(new AssetLocation("sounds/creature/fox/yip"), 2000),
        ],
    };

    public SfxSound[] GetSounds(string? variant)
    {
        if (variant != null && _variants.TryGetValue(variant, out var sounds))
            return sounds;
        return _variants[DefaultVariant];
    }
}
