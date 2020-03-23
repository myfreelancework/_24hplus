using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _24hplusdotnetcore.Common;
using _24hplusdotnetcore.Models;
using MongoDB.Driver;

namespace _24hplusdotnetcore.Services
{
    public class UserServices
    {
        private readonly IMongoCollection<User> _user;
        public UserServices(IMongoDbConnection connection)
        {
            var client = new MongoClient(connection.ConnectionString);
            var database = client.GetDatabase(connection.DataBase);
            _user = database.GetCollection<User>(MongoCollection.UsersCollection);
        }

        public User Create(User user)
        {
            var userModel = new User();
            userModel.UserEmail = user.UserEmail;
            userModel.UserPassword = user.UserPassword;
            userModel.RoleId = user.RoleId;
            _user.InsertOne(userModel);
            return userModel;
        }
    }
}
