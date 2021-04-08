using FirstBotDiscord.Entities.Rpg;
using FirstBotDiscord.Entities.Rpg.Class;
using FirstBotDiscord.Entities.Rpg.Items;
using FirstBotDiscord.Entities.Rpg.Player;
using FirstBotDiscord.Entities.Rpg.RpgMonsters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BaseMonstersEntity>().HasKey(x => x.MonsterId);
            modelBuilder.Entity<BaseItemsEntity>().HasKey(x => x.ItemId);
            modelBuilder.Entity<MapsEntity>().HasKey(x => x.MapId);

            modelBuilder.Entity<PlayerEntity>().HasKey(x => x.PlayerDbId);

            modelBuilder.Entity<AtributesEntity>().HasNoKey();
            modelBuilder.Entity<ClassEntity>().HasNoKey();
            modelBuilder.Entity<InventoryEntity>().HasNoKey();
            modelBuilder.Entity<CharacterEntity>().HasNoKey()
                .Ignore(x => x.Inventory)
                .Ignore(x => x.Money)
                .Ignore(x => x.MyClass)
                .Ignore(x => x.AtributesCharacter)
                .Ignore(x => x.Race)
                .Ignore(x => x.KarmaPoints)
                .Ignore(x => x.LifePoints)
                .Ignore(x => x.ManaPoints);

            modelBuilder.Entity<LevelEntity>().HasNoKey();
            modelBuilder.Entity<LocalizationEntity>();
            modelBuilder.Entity<MoneyEntity>().HasNoKey();
            modelBuilder.Entity<RaceEntity>().HasNoKey();
            modelBuilder.Entity<StatePointsEntity>().HasNoKey();

            //modelBuilder.Ignore<AtributesEntity>();
            //modelBuilder.Ignore<ClassEntity>();
            //modelBuilder.Ignore<InventoryEntity>();
            //modelBuilder.Ignore<CharacterEntity>();
            //modelBuilder.Ignore<LevelEntity>();
            //modelBuilder.Ignore<LocalizationEntity>();
            //modelBuilder.Ignore<MoneyEntity>();
            //modelBuilder.Ignore<RaceEntity>();
            //modelBuilder.Ignore<StatePointsEntity>();

            base.OnModelCreating(modelBuilder);
        }
    }
}
