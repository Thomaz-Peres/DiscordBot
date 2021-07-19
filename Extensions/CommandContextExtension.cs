//using DSharpPlus.CommandsNext;
//using DSharpPlus.Entities;
//using DSharpPlus.Interactivity;
//using DSharpPlus.Interactivity.Extensions;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace FirstBotDiscord.Extensions
//{
//    public static class CommandContextExtension
//    {
//        public struct Answer<T>
//        {
//            public bool TimedOut;
//            public T Value;

//            public Answer(bool timedOut, T result)
//            {
//                TimedOut = timedOut;
//                Value = result;
//            }
//        }

//        public static Task<DiscordMessage> RespondAsync(this CommandContext ctx, string mensagem)
//            => ctx.RespondAsync($"{ctx.User.Mention}, {mensagem}");

//        public static Task RespondAsync(this CommandContext ctx, string mensagem, DiscordEmbed embed)
//            => ctx.RespondAsync($"{ctx.User.Mention}, {mensagem}", embed: embed);

//        public static Task RespondAsync(this CommandContext ctx, DiscordEmbed embed)
//            => ctx.RespondAsync($"{ctx.User.Mention}", embed: embed);

//        public static async Task<Answer<T>> WaitForEnumAsync<T>(this CommandContext ctx, Func<DiscordMessage, bool> predicate, TimeSpan? timeoutoverride = null) where T : Enum
//        {
//            while (true)
//            {
//                var interactivity = ctx.Client.GetInteractivity();
//                var embed = new DiscordEmbedBuilder();
//                embed.WithFooter("Digite um numero ou 'sair' para fechar | Somente numeros inteiros");


//                var wait = await interactivity.WaitForMessageAsync(x => x.Author.Id == ctx.User.Id && x.ChannelId == ctx.Channel.Id, timeoutoverride: timeoutoverride);

//                if (wait.TimedOut)
//                {
//                    await ctx.RespondAsync("Tempo de resposta expirado!");
//                    return new Answer<T>(true, default(T));
//                }

//                if (Enum.TryParse(typeof(T), wait.Result.Content, out object result))
//                {
//                    return new Answer<T>(false, (T)result);
//                }

//                if (wait.Result.Content.ToLower().Trim() == "sair")
//                {
//                    return new Answer<T>(true, default(T));
//                }
//            }
//        }
//    }
//}   
