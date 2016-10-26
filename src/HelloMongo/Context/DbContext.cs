using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;

namespace HelloMongo.Contexts
{
    public class DbContext
    {
        private DbContext()
        {

        }
        private static DbContext current;

        public static DbContext Current
        {
            get
            {
                if (current == null)
                    current = new DbContext();
                return current;
            }
        }
        public IMongoDatabase MongoDatabase
        {
            get
            {
                string connectionString = PangContext.Current.Configuration["Data:MongoDb:ConnectionString"];
                string dataBase = PangContext.Current.Configuration["Data:MongoDb:DatabaseName"];
                var client = new MongoClient(connectionString);
                var database = client.GetDatabase(dataBase);
                return database;
            }
        }

        public string SqlConnection {
            get
            {
                return PangContext.Current.Configuration["Data:SqlServer:ConnectionString"];
            }
        }
    }
}
