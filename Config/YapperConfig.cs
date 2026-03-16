using System.Collections.Generic;
using Vintagestory.API.Server;

namespace Yapper.Config;

public class YapperConfig
{
    private readonly ICoreServerAPI _api;
    public YapperConfigData Data { get; private set; } = new();

    public YapperConfig(ICoreServerAPI api)
    {
        _api = api;
    }

    public void Load()
    {
        Data = _api.LoadModConfig<YapperConfigData>("YapperConfig.json") ?? new YapperConfigData();
    }

    public void Save()
    {
        _api.StoreModConfig(Data, "YapperConfig.json");
    }

    public string? GetVariant(string playerUid, string commandName)
    {
        if (Data.PlayerVariants.TryGetValue(playerUid, out var playerConfig))
            if (playerConfig.TryGetValue(commandName, out var variant))
                return variant;
        return null;
    }

    public void SetVariant(string playerUid, string commandName, string variant)
    {
        if (!Data.PlayerVariants.TryGetValue(playerUid, out var playerConfig))
        {
            playerConfig = new Dictionary<string, string>();
            Data.PlayerVariants[playerUid] = playerConfig;
        }
        playerConfig[commandName] = variant;
        Save();
    }
}
