using _24hplusdotnetcore.Models;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _24hplusdotnetcore.Services
{
    public class PartnerServices
    {
        private readonly ILogger<PartnerServices> _logger;
        private readonly IMongoCollection<Partner> _partner;
        public PartnerServices(ILogger<PartnerServices> logger, IMongoDbConnection connection)
        {
            _logger = logger;
            var client = new MongoClient(connection.ConnectionString);
            var database = client.GetDatabase(connection.DataBase);
            _partner = database.GetCollection<Partner>(Common.MongoCollection.Partner);
        }
        public List<Partner> GetPartners()
        {
            var lstPartner = new List<Partner>();
            try
            {
                lstPartner = _partner.Find(p => true).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
            return lstPartner;
        }
        public Partner GetPartner(string PartnerId)
        {
            var objPartner = new Partner();
            try
            {
                objPartner = _partner.Find(p => p.PartnerId == PartnerId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
            return objPartner;
        }
        public Partner Create(Partner partner)
        {
            try
            {
                _partner.InsertOne(partner);
                return partner;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }
    }
}
