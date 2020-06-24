using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Interactivity;
using DSharpPlus.VoiceNext;
using DSharpPlus;


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
        //         await ctx.RespondAsync($"Te interessa verme ?");
        // }

        // [Command("random")]
        // public async Task Random(CommandContext ctx, int a, int b)
        // {
        //     var rnd = new Random();
        //     //  o comando Next pode converter automaticamente.
        //     await ctx.RespondAsync($"🎲 Your random number is: {rnd.Next(a, b)}");
        // }

        [Command("join")]
        public async Task Join(CommandContext ctx)
        {
            var vnext = ctx.Client.GetVoiceNextClient();

            var vnc = vnext.GetConnection(ctx.Guild);
            if (vnc != null)
            {
                throw new InvalidOperationException("Already connected in this guild.");
            }
            var chn = ctx.Member?.VoiceState?.Channel;
            if (chn == null)
            {
                throw new InvalidOperationException("You need to be in a voice channel.");
            }
            vnc = await vnext.ConnectAsync(chn);
            await ctx.RespondAsync($"🖕🏿");
        }

        [Command("leave")]
        public async Task Leave(CommandContext ctx)
        {
            var vnext = ctx.Client.GetVoiceNextClient();

            var vnc = vnext.GetConnection(ctx.Guild);
            if(vnc == null)
            {
                throw new InvalidOperationException("Not connected in this guild");
            }

            vnc.Disconnect();
            await ctx.RespondAsync("Verme");
        }
    }
}