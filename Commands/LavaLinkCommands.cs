using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Lavalink;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstBotDiscord.Commands
{
    public class LavaLinkCommands : BaseCommandModule
    {
        [Command]
        public async Task Join(CommandContext context, DiscordChannel channel) 
        {
            var lava = context.Client.GetLavalink();
            if (!lava.ConnectedNodes.Any())
            {
                await context.RespondAsync("The Lavalink connection is not established");
                return;
            }

            var node = lava.ConnectedNodes.Values.First();

            if (channel.Type != ChannelType.Voice)
            {
                await context.RespondAsync("Not a valid voice channel.");
                return;
            }

            await node.ConnectAsync(channel);
            await context.RespondAsync($"Joined {channel.Name}!");
        }
        public async Task Leave(CommandContext context, DiscordChannel channel)
        {
            var lava = context.Client.GetLavalink();
            if (!lava.ConnectedNodes.Any())
            {
                await context.RespondAsync("Conexão com lavalink não é estavel ou nao existe");
                return;
            }

            var node = lava.ConnectedNodes.Values.First();

            if (channel.Type != ChannelType.Voice)
            {
                await context.RespondAsync("Não é um canal valido");
                return;
            }

            var conn = node.GetGuildConnection(channel.Guild);

            if (conn == null)
            {
                await context.RespondAsync("Lavalink não esta conectado");
                return;
            }

            await conn.DisconnectAsync();
            await context.RespondAsync($"Left {channel.Name}!");
        }

        [Command]
        public async Task Play(CommandContext context, [RemainingText]string search)
        {
            if(context.Member.VoiceState == null || context.Member.VoiceState.Channel == null)
            {
                await context.RespondAsync("Você não esta conectado em um canal de voz");
                return;
            }

            var lava = context.Client.GetLavalink();
            var node = lava.ConnectedNodes.Values.First();
            var conn = node.GetGuildConnection(context.Member.VoiceState.Guild);

            if(conn == null)
            {
                await context.RespondAsync("LavaLink não esta conectado");
                return;
            }

            var loadResult = await node.Rest.GetTracksAsync(search);

            if(loadResult.LoadResultType == LavalinkLoadResultType.LoadFailed || loadResult.LoadResultType == LavalinkLoadResultType.NoMatches)
            {
                await context.RespondAsync($"Falha ao localizar a musica do link {search}");
                return;
            }

            var track = loadResult.Tracks.First();

            await conn.PlayAsync(track);

            await context.RespondAsync($"Musica tocando no momento {track.Title} !");
        }

        [Command]
        public async Task Pause(CommandContext context)
        {
            if(context.Member.VoiceState == null || context.Member.VoiceState.Channel == null)
            {
                await context.RespondAsync("Você nao esta conectado a um canal de voz");
                return;
            }

            var lava = context.Client.GetLavalink();
            var node = lava.ConnectedNodes.Values.First();
            var conn = node.GetGuildConnection(context.Member.VoiceState.Guild);

            if(conn == null)
            {
                await context.RespondAsync("LavaLink não esta conectado");
                return;
            }

            if(conn.CurrentState.CurrentTrack == null)
            {
                await context.RespondAsync("Não tem nenhuma musica tocando");
                return;
            }

            await conn.PauseAsync();
        }
    }
}
