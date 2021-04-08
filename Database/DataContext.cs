using FirstBotDiscord.Entities.Rpg;
using FirstBotDiscord.Entities.Rpg.Items;
using FirstBotDiscord.Entities.Rpg.Player;
using FirstBotDiscord.Entities.Rpg.RpgMonsters;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstBotDiscord.Database
{
    public class DataContext
    {
        public IMongoClient MongoClient { get; }
        public IMongoDatabase MongoDatabase { get; }
        public IMongoCollection<PlayerEntity> CollectionJogadores { get; }
        public IMongoCollection<BaseItemsEntity> CollectionServidores { get; }
        public IMongoCollection<BaseMonstersEntity> CollectionMonsters { get; }
        public IMongoCollection<MapsEntity> CollectionMaps { get; }


        public DataContext()
        {
            MongoClient = new MongoClient("mongodb://localhost");
            MongoDatabase = MongoClient.GetDatabase("RpgDiscord");

            MapsBuilders.MapBuilderCharacter();
            MapsBuilders.MapBuilderLevel();
            MapsBuilders.MapBuilderMaps();
            MapsBuilders.MapBuilderMoney();

        }
    }
}
