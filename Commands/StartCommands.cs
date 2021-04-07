using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System.Threading.Tasks;
using FirstBotDiscord.Entities.Rpg.Player;
using DSharpPlus.Entities;
using FirstBotDiscord.Extensions;
using System;

namespace FirstBotDiscord.Commands
{
    public class StartCommands : BaseCommandModule
    {
        [Command("create")]
        [Description("Criar um personagem")]
        public async Task CreateCharacter(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            var player = new CharacterEntity();

            var embed = new DiscordEmbedBuilder();

            player.NameCharacter = ctx.User.Username;
            player.CharacterId = ctx.User.Id;

            embed.AddField("ID do personagem:", $"{player.CharacterId}");
            embed.AddField("Nome do seu personagem:", $"{player.NameCharacter}");
            embed.AddField("Seu personagem foi criado em:", $"{player.DateCreateCharacter}");
            embed.WithThumbnail(ctx.User.AvatarUrl);
            embed.WithColor(DiscordColor.Blue);
            embed.WithFooter("Se estiver de acordo, responda 'sim', caso não estiver, responda 'não'");

            await ctx.RespondAsync(embed.Build());
        }
    }
}