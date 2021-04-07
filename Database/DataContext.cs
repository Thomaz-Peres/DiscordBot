using FirstBotDiscord.Entities.Rpg;
using FirstBotDiscord.Entities.Rpg.Items;
using FirstBotDiscord.Entities.Rpg.Player;
using FirstBotDiscord.Entities.Rpg.RpgMonsters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstBotDiscord.Database
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> opt) : base(opt) { }

        public DbSet<PlayerEntity> Players { get; set; }
        public DbSet<BaseItemsEntity> BaseItems { get; set; }
        public DbSet<BaseMonstersEntity> BaseMonsters { get; set; }
        public DbSet<MapsEntity> Maps { get; set; }
    }
}
