﻿using _24hplusdotnetcore.Common;
using _24hplusdotnetcore.Models;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

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
        public List<Customer> GetList(string UserName, DateTime? DateFrom, DateTime? DateTo, string Status, string greentype,string customername, int? pagenumber, int? pagesize, ref int totalPage, ref int totalrecord)
        {
            var lstCustomer = new List<Customer>();
            DateTime _datefrom = DateFrom.HasValue ? Convert.ToDateTime(DateFrom) : new DateTime(0001, 01, 01);
            DateTime _dateto = DateTo.HasValue ? Convert.ToDateTime(DateTo) : new DateTime(9999, 01, 01);
            try
            {
                int _pagesize = !pagesize.HasValue ? Common.Config.PageSize : (int)pagesize;
                var filterUserName = Builders<Customer>.Filter.Regex(c => c.UserName, "/^" + UserName + "$/i");
               
                var filterCreateDate = Builders<Customer>.Filter.Gte(c => c.CreatedDate, _datefrom) & Builders<Customer>.Filter.Lte(c => c.CreatedDate, _dateto);
                filterUserName = filterUserName & filterCreateDate;
                if (!string.IsNullOrEmpty(greentype))
                {
                    var filterGreenType = Builders<Customer>.Filter.Eq(c => c.GreenType, greentype);
                    filterUserName = filterUserName & filterGreenType;
                }
                if (!string.IsNullOrEmpty(Status))
                {
                    var filterStatus = Builders<Customer>.Filter.Regex(c => c.Status, "/^"+Status+"$/i");
                    filterUserName = filterUserName & filterStatus;
                }
                if (!string.IsNullOrEmpty(customername))
                {
                    var filterCustomerName = Builders<Customer>.Filter.Regex(c => c.Personal.Name, ".*" + customername + ".*");
                    filterUserName = filterUserName & filterCustomerName;
                }
                var lstCount = _customer.Find(filterUserName).SortBy(c => c.CreatedDate).ToList().Count;
                lstCustomer = _customer.Find(filterUserName).SortByDescending(c => c.ModifiedDate)
               .Skip((pagenumber != null && pagenumber > 0) ? ((pagenumber - 1) * _pagesize) : 0).Limit(_pagesize).ToList();
                totalrecord = lstCount;
                if (lstCount == 0)
                {
                    totalPage = 0;
                }
                else
                {
                    if (lstCount <= _pagesize)
                    {
                        totalPage = 1;
                    }
                    else
                    {
                        totalPage = lstCount / _pagesize + ((lstCount % _pagesize) > 0? 1 : 0);
                    }
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
                customer.CreatedDate = Convert.ToDateTime(DateTime.Today.ToShortDateString());
                customer.ModifiedDate = Convert.ToDateTime(DateTime.Today.ToShortDateString());
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
                customer.ModifiedDate = Convert.ToDateTime(DateTime.Today.ToShortDateString());
                customer.CreatedDate = _customer.Find(c => c.Id == customer.Id).FirstOrDefault().CreatedDate;
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
                    var statusdraft = lstCustomer.Where(l => string.Equals(l.Status, CustomerStatus.DRAFT, StringComparison.CurrentCultureIgnoreCase)).ToList().Count;

                    var statusreturn = lstCustomer.Where(l => string.Equals(l.Status, CustomerStatus.RETURN, StringComparison.CurrentCultureIgnoreCase)).ToList().Count;

                    var statussubmit = lstCustomer.Where(l => string.Equals(l.Status, CustomerStatus.SUBMIT, StringComparison.CurrentCultureIgnoreCase)).ToList().Count;

                    var statusreject = lstCustomer.Where(l => string.Equals(l.Status, CustomerStatus.REJECT, StringComparison.CurrentCultureIgnoreCase)).ToList().Count;

                    var statusapprove = lstCustomer.Where(l => string.Equals(l.Status, CustomerStatus.APPROVE, StringComparison.CurrentCultureIgnoreCase)).ToList().Count;

                    var all = lstCustomer.Count;

                    statusCount.Draft = statusdraft;
                    statusCount.Return = statusreturn;
                    statusCount.Submit = statussubmit;
                    statusCount.Approve = statusapprove;
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
