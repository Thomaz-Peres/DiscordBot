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
        public static LavalinkNodeConnection LavalinkNode { get; private set; }
        LavalinkTrack musicaAtual = new();
        Queue<LavalinkTrack> musicQueue = new();

        bool musicPlaying = false;

        //[Command]
        //public async Task Join(CommandContext context, DiscordChannel channel) 
        //{
        //    var lava = context.Client.GetLavalink();
        //    if (!lava.ConnectedNodes.Any())
        //    {
        //        await context.RespondAsync("The Lavalink connection is not established");
        //        return;
        //    }

        //    var node = lava.ConnectedNodes.Values.First();

        //    if (channel.Type != ChannelType.Voice)
        //    {
        //        await context.RespondAsync("Not a valid voice channel.");
        //        return;
        //    }

        //    await node.ConnectAsync(channel);
        //    await context.RespondAsync($"Joined {channel.Name}!");
        //}

        [Command]
        public async Task Leave(CommandContext context, DiscordChannel channel)
        {
            var lava = context.Client.GetLavalink();
            var node = lava.ConnectedNodes.Values.First();
            var conn = node.GetGuildConnection(channel.Guild);
            if (!lava.ConnectedNodes.Any())
            {
                await context.RespondAsync("Conexão com lavalink não é estável ou nao existe");
                return;
            }

            if (channel.Type != ChannelType.Voice)
            {
                await context.RespondAsync("Não é um canal valido");
                return;
            }

            if (conn == null)
            {
                await context.RespondAsync("Lavalink não esta conectado");
                return;
            }

            await conn.DisconnectAsync();
            await context.RespondAsync($"Left {channel.Name}!");
        }

        [Command]
        public async Task Play(CommandContext context, [RemainingText] string search)
        {
            var lava = context.Client.GetLavalink();
            var node = lava.ConnectedNodes.Values.First();
            var conn = node.GetGuildConnection(context.Member.Guild);

            //var musicaAtual; 

            if (context.Member.VoiceState.Channel != null)
            {
                await node.ConnectAsync(context.Member.VoiceState.Channel);
            }

            if (context.Member.VoiceState.Channel != context.Channel)
            {
                await node.ConnectAsync(context.Member.VoiceState.Channel);
            }

            if (conn == null)
            {
                await context.RespondAsync("LavaLink não esta conectado");
                return;
            }

            var loadResult = await node.Rest.GetTracksAsync(search);

            if (loadResult.LoadResultType == LavalinkLoadResultType.LoadFailed || loadResult.LoadResultType == LavalinkLoadResultType.NoMatches)
            {
                await context.RespondAsync($"Falha ao localizar a musica do link {search}");
                return;
            }

            LavalinkTrack track = loadResult.Tracks.First();

            if(musicQueue.Count == 0 && musicPlaying != true)
            {
                await conn.PlayAsync(track);
                await context.RespondAsync($"Música tocando: {track.Title} :headphones:").ConfigureAwait(false);
                musicPlaying = true;
                musicaAtual = track;
            } 
            else
            {
                musicQueue.Enqueue(track);
                await context.RespondAsync($"A música: {track.Title} foi adicionada à playlist de músicas para tocar!\n " +
                                           $"Lugar na fila {musicQueue.Count}").ConfigureAwait(false);
            }

            conn.PlaybackFinished += Conn_PlaybackFinished;
        }

        private async Task Conn_PlaybackFinished(LavalinkGuildConnection sender, DSharpPlus.Lavalink.EventArgs.TrackFinishEventArgs e)
        {
            musicPlaying = false;
            if (musicQueue.Count != 0)
            {
                musicaAtual = musicQueue.Dequeue();
                musicPlaying = true;
                await e.Player.PlayAsync(musicaAtual);
            }
            return;
        }

        [Command]
        public async Task Pause(CommandContext context)
        {
            var lava = context.Client.GetLavalink();
            var node = lava.ConnectedNodes.Values.First();
            var conn = node.GetGuildConnection(context.Member.VoiceState.Guild);

            if (context.Member.VoiceState == null || context.Member.VoiceState.Channel == null)
            {
                await context.RespondAsync("Você nao esta conectado a um canal de voz");
                return;
            }

            if (conn == null)
            {
                await context.RespondAsync("LavaLink não esta conectado");
                return;
            }

            if (conn.CurrentState.CurrentTrack == null)
            {
                await context.RespondAsync("Não tem nenhuma musica tocando");
                return;
            }

            await conn.PauseAsync();
        }

        [Command]
        public async Task Volts(CommandContext context)
        {
            var lava = context.Client.GetLavalink();
            var node = lava.ConnectedNodes.Values.First();
            var conn = node.GetGuildConnection(context.Member.VoiceState.Guild);

            if (context.Member.VoiceState == null || context.Member.VoiceState.Channel == null)
            {
                await context.RespondAsync("Você nao esta conectado a um canal de voz");
                return;
            }

            if (conn == null)
            {
                await context.RespondAsync("LavaLink não esta conectado");
                return;
            }

            if (conn.CurrentState.CurrentTrack == null)
            {
                await context.RespondAsync("Não tem nenhuma musica na fila");
                return;
            }

            await conn.ResumeAsync();
        }

        [Command]
        public async Task Stop(CommandContext context)
        {
            var lava = context.Client.GetLavalink();
            var node = lava.ConnectedNodes.Values.First();
            var conn = node.GetGuildConnection(context.Member.VoiceState.Guild);

            if (context.Member.VoiceState == null || context.Member.VoiceState.Channel == null)
            {
                await context.RespondAsync("Você nao esta conectado a um canal de voz");
                return;
            }

            if (conn == null)
            {
                await context.RespondAsync("LavaLink não esta conectado");
            }

            await conn.StopAsync();
        }

        [Command]
        public async Task Skipas(CommandContext context)
        {
            var lava = context.Client.GetLavalink();
            var node = lava.ConnectedNodes.Values.First();
            var conn = node.GetGuildConnection(context.Member.VoiceState.Guild);

            if (context.Member.VoiceState == null || context.Member.VoiceState.Channel == null)
            {
                await context.RespondAsync("Você nao esta conectado a um canal de voz");
                return;
            }

            if (conn == null)
            {
                await context.RespondAsync("LavaLink não esta conectado");
            }

            if(musicQueue.Count != 0 || musicPlaying == false)
            {
                musicaAtual = musicQueue.Dequeue();
                await context.RespondAsync($"Musica skipada com sucesso, música a seguir {musicaAtual.Title}");
                await conn.PlayAsync(musicaAtual);
            }
            else
            {
                await conn.StopAsync();
                await context.RespondAsync("Como a fila está vazia, a música foi parada");
            }
        }
    }
}
