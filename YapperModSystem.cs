using Vintagestory.API.Common;
using Vintagestory.API.Server;
using Yapper.Commands;
using Yapper.Features;

namespace Yapper;

public class YapperModSystem : ModSystem
{
    private SfxFeature? _feature;

    public override void StartServerSide(ICoreServerAPI api)
    {
        api.Logger.Notification("[Yapper] Starting server-side initialization...");

        var version = Mod.Info.Version;

        _feature = new SfxFeature(api, new Ports.ISfxCommand[]
        {
            new AwooCommand(),
            new GrowlCommand(),
            new RoarCommand(),
            new BarkCommand(),
            new BellowCommand(),
            new BugleCommand(),
            new BaaCommand(),
            new BleatCommand(),
            new CluckCommand(),
            new CrowCommand(),
            new OinkCommand(),
            new LaughCommand(),
            new ChirpCommand(),
            new PeepCommand(),
            new ChatterCommand(),
            new SniffCommand(),
        }, version);
        _feature.Start();

        api.Logger.Notification("[Yapper] Initialization complete.");
    }

    public override void Dispose()
    {
        _feature?.Stop();
        _feature = null;
        base.Dispose();
    }
}
