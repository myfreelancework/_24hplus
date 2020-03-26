﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _24hplusdotnetcore.Common;
using _24hplusdotnetcore.Models;
using Microsoft.AspNetCore.DataProtection;
using MongoDB.Driver;

namespace _24hplusdotnetcore.Services
{
    public class UserServices
    {
        private readonly IMongoCollection<User> _user;
        private readonly IDataProtectionProvider _dataProtectionProvider;
        public UserServices(IMongoDbConnection connection, IDataProtectionProvider dataProtectionProvider)
        {
            var client = new MongoClient(connection.ConnectionString);
            var database = client.GetDatabase(connection.DataBase);
            _user = database.GetCollection<User>(MongoCollection.UsersCollection);
            _dataProtectionProvider = dataProtectionProvider;
        }

        public User Create(User user)
        {
            CipherServices cipher = new CipherServices(_dataProtectionProvider);
            var userModel = new User();
            userModel.UserEmail = user.UserEmail;
            userModel.UserFirstName = user.UserFirstName;
            userModel.UserLastName = user.UserLastName;
            userModel.UserPassword = cipher.Encrypt(user.UserPassword);
            userModel.RoleId = user.RoleId;
            _user.InsertOne(userModel);
            return userModel;
        }

        public List<User> Get()
        {
           return _user.Find(user => true).ToList();
        }

        public User Get(string userEmail)
        {
            return _user.Find(user => user.UserEmail == userEmail).FirstOrDefault();
        }
    }
}
