using FirstBotDiscord.Entities.Rpg;
using FirstBotDiscord.Entities.Rpg.Items;
using FirstBotDiscord.Entities.Rpg.Player;
using FirstBotDiscord.Entities.Rpg.RpgMonsters;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstBotDiscord.Database
{
    public class MapsBuilders
    {
        public static void BuidAll()
        {
            MapBuilderPlayer();
            MapBuilderCharacter();
            MapBuilderMoney();
            MapBuilderLevel();
            MapBuilderMaps();
            MapBuilderMonstersBase();
            MapBuilderItemsBase();
        }


        private static void MapBuilderPlayer()
        {
            BsonClassMap.RegisterClassMap<PlayerEntity>(x =>
            {
                x.AutoMap();
                x.SetIgnoreExtraElements(true);
            });
        }

        private static void MapBuilderCharacter()
        {
            BsonClassMap.RegisterClassMap<CharacterEntity>(x =>
            {
                x.AutoMap();
                x.SetIgnoreExtraElements(true);
            });
        }

        private static void MapBuilderMoney()
        {
            BsonClassMap.RegisterClassMap<MoneyEntity>(x =>
            {
                x.AutoMap();
                x.SetIgnoreExtraElements(true);
            });
        }

        private static void MapBuilderLevel()
        {
            BsonClassMap.RegisterClassMap<LevelEntity>(x =>
            {
                x.AutoMap();
                x.SetIgnoreExtraElements(true);
            });
        }

        private static void MapBuilderMaps()
        {
            BsonClassMap.RegisterClassMap<MapsEntity>(x =>
            {
                x.AutoMap();
                x.SetIgnoreExtraElements(true);
            });
        }

        private static void MapBuilderMonstersBase()
        {
            BsonClassMap.RegisterClassMap<BaseMonstersEntity>(x =>
            {
                x.AutoMap();
                x.SetIgnoreExtraElements(true);
                //x.MapIdMember(x => x.MonsterId).SetIdGenerator(ObjectIdGenerator.Instance);
            });
        }

        private static void MapBuilderItemsBase()
        {
            BsonClassMap.RegisterClassMap<BaseItemsEntity>(x =>
            {
                x.AutoMap();
                x.SetIgnoreExtraElements(true);
                //x.MapIdMember(x => x.ItemId).SetIdGenerator(ObjectIdGenerator.Instance);
            });
        }
    }
}
