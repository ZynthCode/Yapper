using System.Collections.Generic;
using Vintagestory.API.Common;
using Yapper.Ports;

namespace Yapper.Commands;

public class BellowCommand : ISfxCommand
{
    public string Name => "bellow";
    public string Description => "Bellow like a mighty beast!";
    public string FlavorText => "bellowed!";
    public float Range => 48f;
    public float Volume => 1f;
    public string[] Variants => ["all", "elk", "redstag"];
    public string DefaultVariant => "all";

    private static readonly SfxSound[] _elk =
    [
        new(new AssetLocation("sounds/creature/hooved/large/elk/bellow1"), 3000),
        new(new AssetLocation("sounds/creature/hooved/large/elk/bellow2"), 2500),
    ];

    private static readonly SfxSound[] _redstag =
    [
        new(new AssetLocation("sounds/creature/hooved/large/redstag/bellow1"), 2000),
        new(new AssetLocation("sounds/creature/hooved/large/redstag/bellow2"), 2000),
        new(new AssetLocation("sounds/creature/hooved/large/redstag/roar1"), 1200),
        new(new AssetLocation("sounds/creature/hooved/large/redstag/roar2"), 1200),
        new(new AssetLocation("sounds/creature/hooved/large/redstag/roar3"), 1200),
    ];

    private static readonly SfxSound[] _all = [.._elk, .._redstag];

    private static readonly Dictionary<string, SfxSound[]> _variants = new()
    {
        ["all"] = _all,
        ["elk"] = _elk,
        ["redstag"] = _redstag,
    };

    public SfxSound[] GetSounds(string? variant)
    {
        if (variant != null && _variants.TryGetValue(variant, out var sounds))
            return sounds;
        return _variants[DefaultVariant];
    }
}
