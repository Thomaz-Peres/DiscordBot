﻿using DSharpPlus.CommandsNext;
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
                embed.AddField("Karma", $"{character.MyCharacter.KarmaPoints.CurrentOrMinValuePoints}", true);

                //embed.AddField("Sua raça:", character.MyCharacter.Race);
                //embed.AddField("Sua classe atual:", character.MyCharacter.Class);
                //embed.AddField("SEu titulo atual:", character.MyCharacter.Title);

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

                embed.WithDescription($@"{new StringBuilder().AppendLine("STATUS       MIN/MAX")}" + GetPlayerStatus(status));

                embed.WithFooter("Se possui atributos livres, use o comando 'ap' para utiliza-los");

                await ctx.RespondAsync(embed.Build());
            }
        }

        private string GetPlayerStatus(Entities.Rpg.Player.PlayerEntity Status)
        {
            var atb = Status.MyCharacter.AtributesCharacter;
            return $"Ataque fisico: {Status.MyCharacter.PhysicalAttack.CurrentOrMinValuePoints} / {Status.MyCharacter.PhysicalAttack.MaxValuePoints} \n" +
                $"Ataque magico: {Status.MyCharacter.MagicAttack.CurrentOrMinValuePoints} / {Status.MyCharacter.MagicAttack.MaxValuePoints} \n" +
                $"Armadura: {Status.MyCharacter.Armor.CurrentOrMinValuePoints} / {Status.MyCharacter.Armor.MaxValuePoints} \n" +
                $"Resistencia magica: {Status.MyCharacter.MagicResistence.CurrentOrMinValuePoints} / {Status.MyCharacter.MagicResistence.MaxValuePoints} \n" +
                $"Persuassão: {Status.MyCharacter.Persuation.CurrentOrMinValuePoints} / {Status.MyCharacter.MagicResistence.MaxValuePoints} \n" +
                $"Sorte: {Status.MyCharacter.Luck.CurrentOrMinValuePoints} / {Status.MyCharacter.Luck.MaxValuePoints} \n" +
                $"Evasão: {Status.MyCharacter.Evasion.CurrentOrMinValuePoints} / {Status.MyCharacter.Evasion.MaxValuePoints} \n\n" +

                $"{new StringBuilder().AppendLine("ATRIBUTOS")}" +
                $"Força: {atb.Forca.CurrentValuePoints} | " +
                $"Inteligencia: {atb.Inteligencia.CurrentValuePoints}\n" +
                $"Vitalidade: {atb.Vitalidade.CurrentValuePoints} | " +
                $"Agilidade: {atb.Agilidade.CurrentValuePoints}\n" +
                $"Carisma: {atb.Carisma.CurrentValuePoints} | " +
                $"Sabedoria: {atb.Sabedoria.CurrentValuePoints}\n" +
                $"Sorte: {atb.Sorte.CurrentValuePoints} | " +
                $"Pontos Livres: {atb.PontosLivres.CurrentValuePoints}";
        }
    }
}
