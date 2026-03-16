using System;
using System.Collections.Generic;
using System.Linq;
using Vintagestory.API.Common;
using Vintagestory.API.Server;
using Yapper.Config;
using Yapper.Ports;
using Yapper.Utilities;

namespace Yapper.Features;

public class SfxFeature
{
    private readonly ICoreServerAPI _api;
    private readonly ISfxCommand[] _commands;
    private readonly string _modVersion;
    private readonly YapperConfig _config;
    private readonly Dictionary<string, long> _cooldowns = new();
    private readonly Dictionary<string, int> _cooldownDurations = new();
    private readonly Random _random = new();

    public SfxFeature(ICoreServerAPI api, ISfxCommand[] commands, string modVersion)
    {
        _api = api;
        _commands = commands;
        _modVersion = modVersion;
        _config = new YapperConfig(api);
    }

    public void Start()
    {
        _config.Load();

        _api.ChatCommands
            .Create("yapper")
            .WithDescription("Yapper SFX mod")
            .RequiresPrivilege(Privilege.chat)
            .BeginSubCommand("version")
                .WithDescription("Show mod version")
                .RequiresPrivilege(Privilege.chat)
                .HandleWith(_ => TextCommandResult.Success($"Yapper v{_modVersion}"))
            .EndSubCommand()
            .BeginSubCommand("help")
                .WithDescription("Show available SFX commands")
                .RequiresPrivilege(Privilege.chat)
                .HandleWith(_ => HandleHelp())
            .EndSubCommand()
            .BeginSubCommand("config")
                .WithDescription("Show current variant config")
                .RequiresPrivilege(Privilege.chat)
                .RequiresPlayer()
                .HandleWith(args => HandleConfig(args))
            .EndSubCommand()
            .BeginSubCommand("play")
                .WithDescription("Play any sound: /yapper play <sound path>")
                .RequiresPrivilege(Privilege.controlserver)
                .RequiresPlayer()
                .WithArgs(_api.ChatCommands.Parsers.Word("sound"))
                .HandleWith(args => HandlePlay(args))
            .EndSubCommand();

        foreach (var command in _commands)
        {
            var sfx = command;
            var builder = _api.ChatCommands
                .Create(sfx.Name)
                .WithDescription(sfx.Description)
                .RequiresPrivilege(Privilege.chat)
                .RequiresPlayer();

            if (sfx.Variants.Length > 0)
            {
                builder
                    .WithArgs(_api.ChatCommands.Parsers.OptionalWord("subcommand"))
                    .HandleWith(args => HandleSfxWithToggle(args, sfx));
            }
            else
            {
                builder.HandleWith(args => HandleSfx(args, sfx));
            }
        }
    }

    public void Stop()
    {
        _cooldowns.Clear();
        _cooldownDurations.Clear();
    }

    private TextCommandResult HandleSfxWithToggle(TextCommandCallingArgs args, ISfxCommand sfx)
    {
        var subcommand = (string?)args.Parsers[0].GetValue();

        if (subcommand == "toggle")
        {
            var player = args.Caller.Player;
            var uid = player.PlayerUID;
            var current = _config.GetVariant(uid, sfx.Name) ?? sfx.DefaultVariant;
            var variants = sfx.Variants;
            var currentIndex = Array.IndexOf(variants, current);
            var next = variants[(currentIndex + 1) % variants.Length];
            _config.SetVariant(uid, sfx.Name, next);
            return TextCommandResult.Success(MessageFormatter.Success($"Switched /{sfx.Name} to {MessageFormatter.Bold(next)}."));
        }

        return HandleSfx(args, sfx);
    }

    private TextCommandResult HandleSfx(TextCommandCallingArgs args, ISfxCommand sfx)
    {
        var byEntity = args.Caller.Entity;
        var player = args.Caller.Player;

        var now = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        var key = player.PlayerUID + ":" + sfx.Name;

        if (_cooldowns.TryGetValue(key, out var lastUsed))
        {
            var elapsed = now - lastUsed;
            var remaining = (_cooldownDurations.GetValueOrDefault(key) - elapsed) / 1000.0;
            if (remaining > 0)
                return TextCommandResult.Error($"Wait {remaining:0.0}s before using /{sfx.Name} again.");
        }

        var variant = sfx.Variants.Length > 0 ? _config.GetVariant(player.PlayerUID, sfx.Name) : null;
        var sounds = sfx.GetSounds(variant);
        var sfxSound = sounds[_random.Next(sounds.Length)];

        _cooldowns[key] = now;
        _cooldownDurations[key] = sfxSound.CooldownMs;

        try
        {
            byEntity.World.PlaySoundAt(sfxSound.Location, byEntity, null, true, sfx.Range, sfx.Volume);
        }
        catch (Exception ex)
        {
            _api.Logger.Error($"Yapper: {ex}");
            return TextCommandResult.Error($"Error: {ex.Message}");
        }

        var pos = byEntity.Pos;
        var message = MessageFormatter.Flavor(player.PlayerName, sfx.FlavorText);

        var nearbyPlayers = _api.World.GetPlayersAround(pos.AsBlockPos.ToVec3d(), sfx.Range, sfx.Range);
        foreach (var nearby in nearbyPlayers)
        {
            var serverPlayer = nearby as IServerPlayer;
            serverPlayer?.SendMessage(0, message, EnumChatType.Notification);
        }

        return TextCommandResult.Success();
    }

    private TextCommandResult HandleConfig(TextCommandCallingArgs args)
    {
        var variantCommands = _commands.Where(c => c.Variants.Length > 0).ToArray();

        if (variantCommands.Length == 0)
            return TextCommandResult.Success("No commands have variants.");

        var uid = args.Caller.Player.PlayerUID;
        var lines = variantCommands.Select(c =>
        {
            var current = _config.GetVariant(uid, c.Name) ?? c.DefaultVariant;
            var available = string.Join(", ", c.Variants);
            return $"  /{c.Name}: {MessageFormatter.Bold(current)} (available: {available})";
        });

        return TextCommandResult.Success(MessageFormatter.Info($"Your variant config:\n{string.Join("\n", lines)}"));
    }

    private TextCommandResult HandlePlay(TextCommandCallingArgs args)
    {
        var soundPath = (string?)args.Parsers[0].GetValue();
        if (string.IsNullOrEmpty(soundPath))
            return TextCommandResult.Error("Sound path is required.");
        var byEntity = args.Caller.Entity;

        try
        {
            var location = new AssetLocation(soundPath);
            byEntity.World.PlaySoundAt(location, byEntity, null, true, 48f, 1f);
            return TextCommandResult.Success(MessageFormatter.Success($"Playing {MessageFormatter.Bold(soundPath)}"));
        }
        catch (Exception ex)
        {
            _api.Logger.Error($"Yapper: {ex}");
            return TextCommandResult.Error($"Error: {ex.Message}");
        }
    }

    private TextCommandResult HandleHelp()
    {
        var lines = _commands.Select(c =>
        {
            var desc = c.Description;
            if (c.Variants.Length > 0)
                desc += $" (use /{c.Name} toggle to switch variant)";
            return $"  {MessageFormatter.Bold("/" + c.Name)} - {desc}";
        });
        var list = string.Join("\n", lines);
        return TextCommandResult.Success(MessageFormatter.Info($"Yapper v{_modVersion}\nAvailable SFX commands:\n{list}"));
    }
}
