using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.VoiceNext;

namespace FirstBotDiscord
{
    public class MyCommands
    {
        // [Command("hi")] //sempre que eu chamar o comando HI, ele faz o metodo abaixo.
        // public async Task Hi(CommandContext ctx)
        // {
        //     // fazendo o bot mandar um HI com o nome marcado de volta.
        //     await ctx.RespondAsync($"👋 Hi, {ctx.User.Mention}!");  // menciona o usuario que invocou o comando.

        //     var interactivity = ctx.Client.GetInteractivityModule();
        //     var msg = await interactivity.WaitForMessageAsync(xm => xm.Author.Id == ctx.User.Id && xm.Content.ToLower()
        //                 == "how are you?", TimeSpan.FromMinutes(1));

        //     if(msg != null)
        //         await ctx.RespondAsync($"I'm fine thanks");
        // }

        // [Command("random")]
        // public async Task Random(CommandContext ctx, int a, int b)
        // {
        //     var rnd = new Random();
        //     //  o comando Next pode converter automaticamente.
        //     await ctx.RespondAsync($"🎲 Your random number is: {rnd.Next(a, b)}");
        // }

        [Command("join")]
        public async Task Join(CommandContext ctx, DiscordChannel chn = null)
        {
            // check whether VNext is enabled
            var vnext = ctx.Client.GetVoiceNextClient();
            if (vnext == null)
            {
                // not enabled
                await ctx.RespondAsync("VNext is not enabled or configured.");
                return;
            }

            // check whether we aren't already connected
            var vnc = vnext.GetConnection(ctx.Guild);
            if (vnc != null)
            {
                // already connected
                await ctx.RespondAsync("Already connected in this guild.");
                return;
            }

            // get member's voice state
            var vstat = ctx.Member?.VoiceState;
            if (vstat?.Channel == null && chn == null)
            {
                // they did not specify a channel and are not in one
                await ctx.RespondAsync("You are not in a voice channel.");
                return;
            }

            // channel not specified, use user's
            if (chn == null)
                chn = vstat.Channel;

            // connect
            vnc = await vnext.ConnectAsync(chn);
            await ctx.RespondAsync($"Connected to `{chn.Name}`");
        }

        [Command("leave")]
        public async Task Leave(CommandContext ctx)
        {
            // check whether VNext is enabled
            var vnext = ctx.Client.GetVoiceNextClient();
            if (vnext == null)
            {
                // not enabled
                await ctx.RespondAsync("VNext is not enabled or configured.");
                return;
            }

            // check whether we are connected
            var vnc = vnext.GetConnection(ctx.Guild);
            if (vnc == null)
            {
                // not connected
                await ctx.RespondAsync("Not connected in this guild.");
                return;
            }

            // disconnect
            vnc.Disconnect();
            await ctx.RespondAsync("Disconnected");
        }

        [Command("play")]
        public async Task Play(CommandContext ctx, [RemainingText] string link)
        {
            var voiceNext = ctx.Client.GetVoiceNextClient();

            var VoiceNextConection = voiceNext.GetConnection(ctx.Guild);
            if(VoiceNextConection == null)
            {
                throw new InvalidOperationException("Not connected in this guild.");
            }

            if(!File.Exists(link))
            {
                throw new FileNotFoundException("File was not found.");
            }

            await ctx.RespondAsync("ok");
            await VoiceNextConection.SendSpeakingAsync(true);   //  send a speaking indicator

            var psi = new ProcessStartInfo
            {
                FileName = "ffmpeg",
                Arguments = $@"-i ""{link}"" -ac 2 -f s16le -ar 48000 pipe:1",
                RedirectStandardOutput = true,
                UseShellExecute = false
            };

            var ffmpeg = Process.Start(psi);
            var ffout = ffmpeg.StandardOutput.BaseStream;

            var buff = new byte[3840];
            var br = 0;

            while((br = ffout.Read(buff, 0, buff.Length)) > 0)
            {
                if(br < buff.Length)    //  not a full sample, mute the rest
                {               
                    for(var i = br; i < buff.Length; i++){
                        buff[i] = 0;
                    }

                    await VoiceNextConection.SendAsync(buff, 20);
                }

                await VoiceNextConection.SendSpeakingAsync(false);  //  we're not speaking anymore
            }
        }
    }
}