using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System.Threading.Tasks;
using FirstBotDiscord.Entities.Rpg.Player;
using DSharpPlus.Entities;
using FirstBotDiscord.Extensions;
using System;

namespace FirstBotDiscord.Commands
{
    public class StartCommands : BaseCommandModule, IServiceProvider
    {
        [Command("create")]
        [Description("Criar um personagem")]
        public async Task CreateCharacter(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            var player = new PlayerEntity();

            var embed = new DiscordEmbedBuilder();

            player.NamePlayer = ctx.User.Username;
            player.PlayerId = ctx.User.Id;

            embed.AddField("ID do personagem:", $"{player.PlayerId}");
            embed.AddField("Nome do seu personagem:", $"{player.NamePlayer}");
            embed.AddField("Seu personagem foi criado em:", $"{player.DateCreatePlayer}");
            embed.WithThumbnail(ctx.User.AvatarUrl);
            embed.WithColor(DiscordColor.Blue);
            embed.WithFooter("Se estiver de acordo, responda 'sim', caso não estiver, responda 'não'");

            await ctx.RespondAsync(embed.Build());
        }

        public object GetService(Type serviceType)
        {
            throw new NotImplementedException();
        }
    }
}