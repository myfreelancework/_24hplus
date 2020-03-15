using _24hplusdotnetcore.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace _24hplusdotnetcore.Services
{
    public class DemoService
    {
        private readonly IMongoCollection<Demo> _demo;
        public DemoService(IMongoDbConnection connection)
        {
            var client = new MongoClient(connection.ConnectionString);
            var database = client.GetDatabase(connection.DataBase);
            _demo = database.GetCollection<Demo>("");
        }
    }
}