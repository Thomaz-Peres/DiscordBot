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

        public async Task SearchEnemy(int monsterLevel)
        {
            var monster = await _dataContext.CollectionMonsters.Find(x => x.Level == monsterLevel).FirstOrDefaultAsync();
        }
    }
}
