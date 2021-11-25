using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Extensions;
using FirstBotDiscord.Database;
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

        public BattleService(DataContext dataContext) =>
            _dataContext = dataContext;

        public async Task SearchEnemy(CommandContext ctx, int monsterLevel)
        {
            var embed = new DiscordEmbedBuilder();
            var monster = await _dataContext.CollectionMonsters.Find(x => x.Level == monsterLevel).FirstOrDefaultAsync();

            if (monster != null)
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
                    await Battle(ctx, monster);
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

        public async Task Battle(CommandContext ctx, BaseMonstersEntity monster)
        {
            var player = await _dataContext.CollectionPlayers.Find(x => x.PlayerId == ctx.User.Id && x.NamePlayer == ctx.User.Username).FirstOrDefaultAsync();
            var message = ctx.Message;
                        

            if (message.Author.Id == player.PlayerId && (message.Content.ToLower() == "sim" || message.Content.ToLower() == "s"))
            {
                var battleInteractivity = ctx.Client.UseInteractivity().WaitForMessageAsync(x => x.Author.Id == player.PlayerId && x.ChannelId == ctx.Channel.Id);


            }
        }
    }
}
