using _24hplusdotnetcore.Common;
using _24hplusdotnetcore.Models;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
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
        public List<Customer> GetList(string UserName, DateTime? DateFrom, DateTime? DateTo, string Status, int? pagenumber, int? pagesize)
        {
            var lstCustomer = new List<Customer>();
            DateTime _datefrom = DateFrom.HasValue ? Convert.ToDateTime(DateFrom) : new DateTime(0001, 01, 01);
            DateTime _dateto = DateTo.HasValue ? Convert.ToDateTime(DateTo) : new DateTime(9999, 01, 01);
            try
            {
                int _pagesize = !pagesize.HasValue ? Common.Config.PageSize : (int)pagesize;

                if (string.IsNullOrEmpty(Status))
                {
                    lstCustomer = _customer.Find(c => c.UserName == UserName && c.CreatedDate >= _datefrom && c.CreatedDate <= _dateto && (c.Status.ToUpper() == Common.CustomerStatus.DRAFT || c.Status.ToUpper() == Common.CustomerStatus.RETURN))
                        .SortBy(c => c.CreatedDate)
                        .Skip((pagenumber != null && pagenumber > 0) ? ((pagenumber - 1) * _pagesize) : 0).Limit(_pagesize).ToList();
                }
                else
                {
                    lstCustomer = _customer.Find(c => c.UserName == UserName && c.CreatedDate >= _datefrom && c.CreatedDate <= _dateto && (c.Status.ToUpper() == Status.ToUpper()))
                        .SortBy(c => c.CreatedDate)
                        .Skip((pagenumber != null && pagenumber > 0) ? ((pagenumber - 1) * _pagesize) : 0).Limit(_pagesize).ToList();
                }


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
            return lstCustomer;
        }
        public Customer GetCustomer(string Id)
        {
            var objCustomer = new Customer();
            try
            {
                objCustomer = _customer.Find(c => c.Id == Id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
            return objCustomer;
        }
        public List<Customer> GetCustomerByUserName(string UserName, int? pagenumber)
        {
            var lstCustomer = new List<Customer>();
            try
            {
                lstCustomer = _customer.Find(c => c.UserName == UserName).SortByDescending(c => c.ModifiedDate).Skip((pagenumber != null && pagenumber > 0) ? ((pagenumber - 1) * Common.Config.PageSize) : 0).Limit(Common.Config.PageSize).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
            return lstCustomer;
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
                updateCount = _customer.ReplaceOne(c => c.Id == customer.Id, customer).ModifiedCount;
            }
            catch (Exception ex)
            {
                updateCount = -1;
                _logger.LogError(ex, ex.Message);
            }
            return updateCount;
        }
        public long DeleteCustomer(string[] Ids)
        {
            long DeleteCount = 0;
            try
            {
                for (int i = 0; i < Ids.Length; i++)
                {
                    DeleteCount += _customer.DeleteOne(c => c.Id == Ids[i] && c.Status.ToUpper() == Common.CustomerStatus.DRAFT).DeletedCount;
                }
            }
            catch (Exception ex)
            {
                DeleteCount = -1;
                _logger.LogError(ex, ex.Message);
            }
            return DeleteCount;
        }
        public StatusCount GetStatusCount(string userName, string GreenType)
        {
            var statusCount = new StatusCount();
            try
            {
                var lstCustomer = _customer.Find(c => c.UserName == userName && c.GreenType == GreenType).ToList();
                if (lstCustomer != null && lstCustomer.Count > 0)
                {
                    var statusdraft = lstCustomer.FindAll(l => l.Status.ToUpper() == CustomerStatus.DRAFT).Count;
                    var statusreturn = lstCustomer.FindAll(l => l.Status.ToUpper() == CustomerStatus.RETURN).Count;
                    var all = lstCustomer.Count;
                    statusCount.Draft = statusdraft;
                    statusCount.Return = statusreturn;
                    statusCount.All = all;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
            return statusCount;
        }
        internal long[] CustomerPagesize(List<Customer> lstCustomer)
        {
            long customersize = lstCustomer.Count;
            if (customersize <= Common.Config.PageSize)
            {
                return new long[]{
                    customersize,
                    1
                };
            }
            long totalpage = (customersize % Common.Config.PageSize) > 0 ? (customersize / Common.Config.PageSize + 1) : (customersize / Common.Config.PageSize + 1);
            return new long[]{
                customersize,
                totalpage
            };
        }
    }
}
