using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Extensions;
using FirstBotDiscord.Database;
using FirstBotDiscord.Entities.Rpg.Player;
using FirstBotDiscord.Entities.Rpg.RpgMonsters;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstBotDiscord.Services
{
    public class BattleService
    {
        private readonly DataContext _dataContext;
        private readonly MonsterAttacks _monsterAttacks;

        public BattleService(DataContext dataContext, MonsterAttacks monsterAttacks)
        {
            _dataContext = dataContext;
            _monsterAttacks = monsterAttacks;
        }

        public async Task SearchEnemy(CommandContext ctx, int monsterLevel)
        {
            var embed = new DiscordEmbedBuilder();
            var monster = await _dataContext.CollectionMonsters.Find(x => x.Level == monsterLevel).FirstOrDefaultAsync();
            var player = await _dataContext.CollectionPlayers.Find(x => x.PlayerId == ctx.User.Id && x.NamePlayer == ctx.User.Username).FirstOrDefaultAsync();

            if (monster != null && player.MyCharacter.CurrentLocalization == monster.MonsterLocalization)
            {
                embed.WithTitle($"Monstro encontrado 👾");
                embed.AddField("Nome do monstro: ", monster.MonsterName);
                embed.AddField("Vida do monstro: ", monster.MonsterLifePoints.CurrentOrMinValuePoints.ToString());
                embed.AddField("Mana do monstro: ", monster.MonsterManaPoints.CurrentOrMinValuePoints.ToString());
                var boss = monster.IsBoss ? "Boss" : "Normal";
                embed.AddField("Tipo de monstro: ", boss, true);

                await ctx.RespondAsync(embed.Build());

                embed = new DiscordEmbedBuilder();
                embed.WithDescription("Você quer lutar com esse mob ? Sim/Não");
                await ctx.RespondAsync(embed.Build());

                var fightOrNot = await ctx.Client.GetInteractivity().WaitForMessageAsync(x => x.Author.Id == ctx.User.Id && x.ChannelId == ctx.Channel.Id); 
                if(fightOrNot.Result.Content.ToLower() == "sim" || fightOrNot.Result.Content.ToLower() == "s")
                {
                    await Battle(ctx, monster, player, fightOrNot.Result.Content);
                    return;
                }
                else
                {
                    embed = new();
                    embed.WithDescription("Batalha evitada, seu patinho 🦢🦆");
                    await ctx.RespondAsync(embed.Build());
                }
                return;
            }
            else
            {
                embed = new DiscordEmbedBuilder();
                embed.WithDescription("Nenhum monstro encontrado, verifique o horario de respawn dos monstros");
                await ctx.RespondAsync(embed.Build());
                return;
            }
        }

        public async Task Battle(CommandContext ctx, BaseMonstersEntity monster, PlayerEntity player, string battleMessageAccepted)
        {
            string battleAccepted = battleMessageAccepted;
            var embed = new DiscordEmbedBuilder();        

            if (ctx.Message.Author.Id == player.PlayerId && (battleAccepted.ToLower() == "sim" || battleAccepted.ToLower() == "s"))
            {
                embed.WithDescription($"Batalha iniciada entre {player.NamePlayer} e {monster.MonsterName}\n" +
                    $"🎲🎲 rolando os dados para ver quer ira começar 🎲🎲");
                await ctx.RespondAsync(embed.Build());

                int playerDice = new Random().Next(1, 20);
                int monsterDice = new Random().Next(1, 20);
                var ganhador = playerDice > monsterDice ? player.NamePlayer : monster.MonsterName;

                embed = new DiscordEmbedBuilder();
                embed.WithDescription($"{player.NamePlayer} tirou: {playerDice}\n" +
                                      $"{monster.MonsterName} tirou: {monsterDice}\n" +
                                      $"O iniciante será {ganhador}");
                embed.WithFooter("Se você for quem ira começar, faça seu ataque em ate 1 minuto");
                await ctx.RespondAsync(embed.Build());

                var battleInteractivity = ctx.Client.UseInteractivity().WaitForMessageAsync(x => x.Author.Id == player.PlayerId && x.ChannelId == ctx.Channel.Id);

                if (ganhador == monster.MonsterName)
                    _monsterAttacks.MonsterChooses(ctx, monster, player.MyCharacter);
                else
                {
                    embed = new DiscordEmbedBuilder();
                    embed.WithDescription("Escolha seus ataques");
                }

            }
        }
    }
}
