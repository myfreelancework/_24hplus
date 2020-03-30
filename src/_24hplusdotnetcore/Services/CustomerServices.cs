using _24hplusdotnetcore.Common;
using _24hplusdotnetcore.Models;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace _24hplusdotnetcore.Services
{
    public class CustomerServices
    {
        private readonly ILogger<CustomerServices> _logger;
        private readonly IMongoCollection<Customer> _customer;
        public CustomerServices(IMongoDbConnection connection, ILogger<CustomerServices> logger)
        {
            var client = new MongoClient(connection.ConnectionString);
            var database = client.GetDatabase(connection.DataBase);
            _customer = database.GetCollection<Customer>(MongoCollection.CustomerCollection);
            _logger = logger;
        }
        public List<Customer> GetList()
        {
            var lstCustomer = new List<Customer>();
            try
            {
                lstCustomer = _customer.Find(c => true).ToList();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
            return lstCustomer;
        }
        public Customer GetCustomer(string MaKH)
        {
            var objCustomer = new Customer();
            try
            {
                objCustomer = _customer.Find(c => c.MaKH == MaKH).FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
            return objCustomer;
        }
        public Customer CreateCustomer(Customer customer)
        {
            try
            {
                _customer.InsertOne(customer);
                return customer;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }
        public long UpdateCustomer(Customer customer)
        {
            long updateCount = 0;
            try
            {
                updateCount = _customer.ReplaceOne(c => c.MaKH == customer.MaKH, customer).ModifiedCount;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
            return updateCount;
        }
        public long DeleteCustomer(string MaKH)
        {
            long DeleteCount = 0;
            try
            {
                DeleteCount = _customer.DeleteOne(c => c.MaKH == MaKH).DeletedCount;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
            return DeleteCount;
        }
    }
}
