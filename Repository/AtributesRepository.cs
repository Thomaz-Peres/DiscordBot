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
    public class AtributesRepository
    {
        private readonly DataContext _context;

        public AtributesRepository(DataContext context) =>
            _context = context;

        public async Task PlayerStatus(CommandContext ctx, DiscordUser user)
        {
            var character = await _context.CollectionPlayers.Find(x => x.PlayerId == user.Id).FirstOrDefaultAsync();

            if (character == null)
                await ctx.RespondAsync("Você não possui um personagem");
            else
            {
                var embed = new DiscordEmbedBuilder();
                embed.WithTitle($"{character.NamePlayer}");
                embed.WithThumbnail(user.AvatarUrl);

                embed.AddField("HP", $"{character.MyCharacter.LifePoints.CurrentOrMinValuePoints} / {character.MyCharacter.LifePoints.MaxValuePoints}", true);
                embed.AddField("MP", $"{character.MyCharacter.ManaPoints.CurrentOrMinValuePoints} / {character.MyCharacter.ManaPoints.MaxValuePoints}", true);
                embed.AddField("Karma", character.MyCharacter.KarmaPoints.CurrentOrMinValuePoints.ToString(), true);

                embed.AddField("Sua raça:", character.MyCharacter.Race.ToString());
                embed.AddField("Sua classe atual:", character.MyCharacter.MyClass.ToString());

                embed.AddField("Monstros mortos:", character.MyCharacter.MonsterKill.ToString(), true);
                embed.AddField("Mortes:", character.MyCharacter.Deaths.ToString(), true);
                embed.AddField("Players abatidos:", character.MyCharacter.PlayersKill.ToString(), true);

                embed.AddField("Localização atual:", character.MyCharacter.CurrentLocalization.ChannelName);

                embed.WithFooter("Se quiser verificar os status e atributos utilize o comando 'sa'");

                await ctx.RespondAsync(embed.Build());
            }
        }

        public async Task CharacterStatus(CommandContext ctx, DiscordUser user)
        {
            var status = await _context.CollectionPlayers.Find(x => x.PlayerId == user.Id).FirstOrDefaultAsync();

            if (status == null)
                await ctx.RespondAsync("Você não possui um personagem para ver os status dele");

            else
            {
                var embed = new DiscordEmbedBuilder();
            
                embed.WithTitle($"Status e atributos {status.NamePlayer}");
            
                var atb = status.MyCharacter.AtributesCharacter;

                embed.WithDescription($@"{new StringBuilder().AppendLine("STATUS       MIN/MAX")}" +
                                      $"Ataque fisico: {status.MyCharacter.PhysicalAttack.CurrentOrMinValuePoints} / {status.MyCharacter.PhysicalAttack.MaxValuePoints} \n" +
                                      $"Ataque magico: {status.MyCharacter.MagicAttack.CurrentOrMinValuePoints} / {status.MyCharacter.MagicAttack.MaxValuePoints} \n" +
                                      $"Armadura: {status.MyCharacter.Armor.CurrentOrMinValuePoints} / {status.MyCharacter.Armor.MaxValuePoints} \n" +
                                      $"Resistencia magica: {status.MyCharacter.MagicResistence.CurrentOrMinValuePoints} / {status.MyCharacter.MagicResistence.MaxValuePoints} \n" +
                                      $"Persuassão: {status.MyCharacter.Persuation.CurrentOrMinValuePoints} / {status.MyCharacter.MagicResistence.MaxValuePoints} \n" +
                                      $"Sorte: {status.MyCharacter.Luck.CurrentOrMinValuePoints} / {status.MyCharacter.Luck.MaxValuePoints} \n" +
                                      $"Evasão: {status.MyCharacter.Evasion.CurrentOrMinValuePoints} / {status.MyCharacter.Evasion.MaxValuePoints} \n\n" +

                                      $"{new StringBuilder().AppendLine("ATRIBUTOS")}" +
                                      $"Força: {atb.Forca.CurrentValuePoints} | " +
                                      $"Inteligencia: {atb.Inteligencia.CurrentValuePoints}\n" +
                                      $"Vitalidade: {atb.Vitalidade.CurrentValuePoints} | " +
                                      $"Agilidade: {atb.Agilidade.CurrentValuePoints}\n" +
                                      $"Carisma: {atb.Carisma.CurrentValuePoints} | " +
                                      $"Sabedoria: {atb.Sabedoria.CurrentValuePoints}\n" +
                                      $"Sorte: {atb.Sorte.CurrentValuePoints} | " +
                                      $"Pontos Livres: {atb.PontosLivres.CurrentValuePoints}");

                embed.WithFooter("Se possui atributos livres, use o comando 'ap' para utiliza-los");

                await ctx.RespondAsync(embed.Build());
            }
        }
    }
}
