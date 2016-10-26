using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelloMongo.Contexts;
using HelloMongo.Models;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Driver;

namespace HelloMongo.Service
{
    public interface IFruitService
    {
        Task<string> FruitQuery();
        Task<string> FruitQuery(string name);
        void FruitAdd(BsonDocument bsonDocument);
        void FruitUpdate(FruitDto fruitDto);
        void FruitDelete(string name);
    }
    public class FruitService : IFruitService
    {
        IMongoCollection<BsonDocument> _collection = null;
        public FruitService()
        {
            _collection = DbContext.Current.MongoDatabase.GetCollection<BsonDocument>("Fruit");
        }
        public async Task<string> FruitQuery()
        {

            var build = Builders<BsonDocument>.Filter;
            var filter = build.Empty;
            var result = await _collection.Find(filter).ToListAsync();
            var jsonWriterSettings = new JsonWriterSettings { OutputMode = JsonOutputMode.Strict };
            return result.ToJson(jsonWriterSettings);
        }

        public async Task<string> FruitQuery(string name)
        {
            var build = Builders<BsonDocument>.Filter;
            var filter = build.Eq("Name", name);
            var result = await _collection.Find(filter).ToListAsync();
            var jsonWriterSettings = new JsonWriterSettings { OutputMode = JsonOutputMode.Strict };
            return result.ToJson(jsonWriterSettings);
        }

        public void FruitAdd(BsonDocument bsonDocument)
        {
            _collection.InsertOne(bsonDocument);
        }

        public void FruitUpdate(FruitDto fruitDto)
        {
            var filterBuild = Builders<BsonDocument>.Filter;
            var filter = filterBuild.Eq("Name", fruitDto.Name);
            var updaterBuild = Builders<BsonDocument>.Update;
            var updater = updaterBuild.Set("Price", fruitDto.Price)
                            .Set("Color", fruitDto.Color)
                            .Set("Code", fruitDto.Code)
                            .Set("StoreCode", fruitDto.StoreCode);

            _collection.UpdateOne(filter, updater);
        }

        public void FruitDelete(string name)
        {

            var filterBuild = Builders<BsonDocument>.Filter;
            var filter = filterBuild.Eq("Name", name);

            _collection.DeleteOne(filter);
        }

    }
}
