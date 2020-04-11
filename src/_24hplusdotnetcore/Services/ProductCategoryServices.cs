using System;
using System.Collections.Generic;
using System.Linq;
using _24hplusdotnetcore.Common;
using _24hplusdotnetcore.Models;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace _24hplusdotnetcore.Services
{
    public class ProductCategoryServices
    {
        private readonly ILogger<ProductCategoryServices> _logger;
        private readonly IMongoCollection<ProductCategory> _productCategory;
        ProductCategoryServices(ILogger<ProductCategoryServices> logger, IMongoDbConnection connection)
        {
            _logger = logger;
            var client = new MongoClient(connection.ConnectionString);
            var database = client.GetDatabase(connection.DataBase);
            _productCategory = database.GetCollection<ProductCategory>(MongoCollection.ProductCategoryCollection);
        }
        public List<ProductCategory> GetProductCategories()
        {
            var lstProductCategories = new List<ProductCategory>();
            try
            {
                lstProductCategories = _productCategory.Find(p => true).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
            return lstProductCategories;
        }
        public ProductCategory GetProductCategory(string ProductCategoryId)
        {
            var objProductCategory = new ProductCategory();
            try
            {
                objProductCategory = _productCategory.Find(p => p.ProductCategoryId == ProductCategoryId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
            return objProductCategory;
        }
        public ProductCategory Create(ProductCategory productCategory)
        {
            try
            {
                _productCategory.InsertOne(productCategory);
                return productCategory;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }
        public long Update(ProductCategory productCategory)
        {
            long updateCount = 0;
            try
            {
                updateCount = _productCategory.ReplaceOne(r => r.Id == productCategory.Id, productCategory).ModifiedCount;
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, ex.Message);
            }
            return updateCount;
        }
        public long Detete(string Id)
        {
            long deleteCount = 0;
            try
            {
                deleteCount = _productCategory.DeleteOne(p => p.Id == Id).DeletedCount;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
            return deleteCount;
        }

    }
}
