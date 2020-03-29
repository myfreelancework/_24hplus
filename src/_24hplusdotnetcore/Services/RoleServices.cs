using _24hplusdotnetcore.Common;
using _24hplusdotnetcore.Models;
using MongoDB.Driver;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _24hplusdotnetcore.Services
{
    public class RoleServices
    {
        private readonly IMongoCollection<Roles> _role;
        public RoleServices(IMongoDbConnection connection)
        {
            var client = new MongoClient(connection.ConnectionString);
            var database = client.GetDatabase(connection.DataBase);
            _role = database.GetCollection<Roles>(MongoCollection.RolesCollection);
        }
        public Roles Create(Roles role)
        {
            var roleModel = new Roles();
            try
            {
                roleModel.RoleName = role.RoleName;
                roleModel.RoleDescription = role.RoleDescription;
                _role.InsertOne(roleModel);
                return roleModel;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return roleModel;
            }
        }
        public Roles Update(string Id ,Roles role)
        {
            try
            {
                _role.ReplaceOne(r => r.Id == Id, role);
                return role;
            }
            catch (Exception ex)
            {

                Log.Error(ex.Message, ex);
                return role;
            }
        }
        public List<Roles> Get()
        {
            List<Roles> lstRoles = new List<Roles>();
            try
            {
                lstRoles = _role.Find(r => true).ToList();
                return lstRoles;
            }
            catch (Exception ex)
            {

                Log.Error(ex.Message, ex);
                return lstRoles;
            }
        }
        public Roles Get(string Id)
        {
            var objRole = new Roles();
            try
            {
                objRole = _role.Find(r => r.Id == Id).FirstOrDefault();
                return objRole;
            }
            catch (Exception ex)
            {

                Log.Error(ex.Message, ex);
                return objRole;
            }
        }
    }
}
