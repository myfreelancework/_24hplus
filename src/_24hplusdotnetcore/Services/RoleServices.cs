using _24hplusdotnetcore.Common;
using _24hplusdotnetcore.Models;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _24hplusdotnetcore.Services
{
    public class RoleServices
    {
        private readonly ILogger<RoleServices> _logger;
        private readonly IMongoCollection<Roles> _role;
        public RoleServices(IMongoDbConnection connection, ILogger<RoleServices> logger)
        {
            var client = new MongoClient(connection.ConnectionString);
            var database = client.GetDatabase(connection.DataBase);
            _role = database.GetCollection<Roles>(MongoCollection.RolesCollection);
            _logger = logger;
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
                _logger.LogError(ex, ex.Message);
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
                 _logger.LogError(ex, ex.Message);
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
                 _logger.LogError(ex, ex.Message);
                return lstRoles;
            }
        }
        public Roles GetRoleById(string Id)
        {
            var objRole = new Roles();
            try
            {
                objRole = _role.Find(r => r.Id == Id).FirstOrDefault();
                return objRole;
            }
            catch (Exception ex)
            {
                 _logger.LogError(ex, ex.Message);
                return objRole;
            }
        }
        public Roles GetRoleByName(string roleName)
        {
            var objRole = new Roles();
            try
            {
                objRole = _role.Find(r => r.RoleName == roleName).FirstOrDefault();
                return objRole;
            }
            catch (Exception ex)
            {
                 _logger.LogError(ex, ex.Message);
                return objRole;
            }
        }
        public long Delete(string Id)
        {
            long deleteCount = 0;
            try
            {                
                deleteCount = _role.DeleteOne(r => r.Id == Id).DeletedCount;
                return deleteCount;
            }
            catch (System.Exception ex)
            { 
                _logger.LogError(ex, ex.Message);
                return deleteCount;
            }
        }
    }
}
