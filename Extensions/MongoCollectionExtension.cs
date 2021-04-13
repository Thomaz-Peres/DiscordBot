using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstBotDiscord.Extensions
{
    public static class MongoCollectionExtension
    {
        public static IMongoCollection<T> CreateCollection<T>(this IMongoDatabase mongoDatabase, string collectionName, CreateCollectionOptions options = null)
        {
            var filter = new ListCollectionNamesOptions
            {
                Filter = Builders<BsonDocument>.Filter.Eq("name", typeof(T).Name)
            };

            if (!mongoDatabase.ListCollectionNames(filter).Any())
                mongoDatabase.CreateCollection(collectionName, options);

            return mongoDatabase.GetCollection<T>(collectionName);
        }
    }
}
