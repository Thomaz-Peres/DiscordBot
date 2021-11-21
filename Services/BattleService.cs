using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using FirstBotDiscord.Database;
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
                embed.WithTitle($"Monstro encontrado: {monster.MonsterName}");
                embed.AddField("Vida do monstro: ", monster.MonsterLifePoints.CurrentOrMinValuePoints.ToString());
                embed.AddField("Mana do monstro: ", monster.MonsterManaPoints.CurrentOrMinValuePoints.ToString());
                var boss = monster.IsBoss ? "sim" : "nao";
                embed.AddField("O monstro é um boss? ", boss, true);

                await ctx.RespondAsync(embed.Build());
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
    }
}
