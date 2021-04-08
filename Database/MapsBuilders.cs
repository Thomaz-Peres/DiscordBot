using FirstBotDiscord.Entities.Rpg;
using FirstBotDiscord.Entities.Rpg.Player;
using MongoDB.Bson.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstBotDiscord.Database
{
    public class MapsBuilders
    {
        public static void MapBuilderCharacter()
        {
            BsonClassMap.RegisterClassMap<CharacterEntity>(x =>
            {
                x.AutoMap();
                x.SetIgnoreExtraElements(true);
            });
        }

        public static void MapBuilderMoney()
        {
            BsonClassMap.RegisterClassMap<MoneyEntity>(x =>
            {
                x.AutoMap();
                x.SetIgnoreExtraElements(true);
            });
        }

        public static void MapBuilderLevel()
        {
            BsonClassMap.RegisterClassMap<LevelEntity>(x =>
            {
                x.AutoMap();
                x.SetIgnoreExtraElements(true);
            });
        }

        public static void MapBuilderMaps()
        {
            BsonClassMap.RegisterClassMap<MapsEntity>(x =>
            {
                x.AutoMap();
                x.SetIgnoreExtraElements(true);
            });
        }

        public static void MapBuilderMaps()
        {
            BsonClassMap.RegisterClassMap<MapsEntity>(x =>
            {
                x.AutoMap();
                x.SetIgnoreExtraElements(true);
            });
        }
    }
}
