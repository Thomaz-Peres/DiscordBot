using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using FirstBotDiscord.Entities.Rpg;
using FirstBotDiscord.Entities.Rpg.Items;
using FirstBotDiscord.Entities.Rpg.Player;
using FirstBotDiscord.Entities.Rpg.RpgMonsters;
using MongoDB.Driver;

namespace FirstBotDiscord.Database
{
    public class DataContext
    {
        public IMongoClient MongoClient { get; }
        public IMongoDatabase MongoDatabase { get; }
        public IMongoCollection<PlayerEntity> CollectionPlayers { get; }
        public IMongoCollection<BaseItemsEntity> CollectionItems { get; }
        public IMongoCollection<BaseMonstersEntity> CollectionMonsters { get; }
        public IMongoCollection<MapsEntity> CollectionMaps { get; }


        public DataContext()
        {
            MongoClient = new MongoClient("mongodb://localhost:27017");
            MongoDatabase = MongoClient.GetDatabase("rpgdiscord");

            MapsBuilders.BuidAll();

            CollectionPlayers = MongoDatabase.GetCollection<PlayerEntity>("Players");
            CollectionItems = MongoDatabase.GetCollection<BaseItemsEntity>("Items");
            CollectionMonsters = MongoDatabase.GetCollection<BaseMonstersEntity>("Monster");
            CollectionMaps = MongoDatabase.GetCollection<MapsEntity>("Maps");

            MongoClient.StartSession();
        }
    }
}
