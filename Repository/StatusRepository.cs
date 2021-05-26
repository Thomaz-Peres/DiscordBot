using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using FirstBotDiscord.Database;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstBotDiscord.Repository
{
    public class StatusRepository
    {
        private readonly DataContext _context;

        public StatusRepository(DataContext context) =>
            _context = context;

        public async Task CharacterStatus(CommandContext ctx, DiscordUser user)
        {
            var character = await _context.CollectionPlayers.Find(x => x.PlayerId == user.Id).FirstOrDefaultAsync();

            var embed = new DiscordEmbedBuilder();

            embed.WithTitle("Status do seu personagem");
            embed.WithThumbnail(user.AvatarUrl);

            embed.AddField("Name:", character.NamePlayer);
            embed.AddField("Vida:", character.MyCharacter.LifePoints.CurrentValuePoints.ToString(), true);
            embed.AddField("Mana:", character.MyCharacter.ManaPoints.CurrentValuePoints.ToString(), true);
            embed.AddField("Karma:", character.MyCharacter.KarmaPoints.CurrentValuePoints.ToString(), true);
            embed.AddField("Experiencia:", character.MyCharacter.Experience.CurrentValuePoints.ToString());

            embed.AddField("Sua raça:", character.MyCharacter.Race.ToString());
            embed.AddField("Sua classe atual:", character.MyCharacter.MyClass.ToString());

            embed.AddField("Monstros mortos:", character.MyCharacter.MonsterKill.ToString(), true);
            embed.AddField("Mortes:", character.MyCharacter.Deaths.ToString(), true);
            embed.AddField("Players abatidos:", character.MyCharacter.PlayersKill.ToString(), true);

            embed.AddField("Localização atual:", character.MyCharacter.CurrentLocalization.ChannelName);

            embed.AddField("Atributos livres:", character.MyCharacter.AtributesCharacter.PontosLivres.ToString());

            embed.WithFooter("Se possui atributos livres, use o comando 'ap' para utiliza-los");
            await ctx.RespondAsync(embed.Build());
        }
    }
}
