using Vintagestory.API.Common;

namespace Yapper.Ports;

public record SfxSound(AssetLocation Location, int CooldownMs);

public interface ISfxCommand
{
    string Name { get; }
    string Description { get; }
    string FlavorText { get; }
    SfxSound[] GetSounds(string? variant);
    string[] Variants { get; }
    string DefaultVariant { get; }
    float Range { get; }
    float Volume { get; }
}
