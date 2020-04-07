using System.Collections.Generic;
using _24hplusdotnetcore.Models;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace _24hplusdotnetcore.Services
{
    public class LoaiCVServices
    {
        private readonly ILogger<LoaiHoSoServices> _logger;
        private readonly IMongoCollection<LoaiHinhCongViec> _loaiCV;
        public LoaiCVServices(ILogger<LoaiHoSoServices> logger, IMongoDbConnection connection)
        {
            _logger = logger;
            var client = new MongoClient(connection.ConnectionString);
            var database = client.GetDatabase(connection.DataBase);
            _loaiCV = database.GetCollection<LoaiHinhCongViec>(Common.MongoCollection.LoaiCV);
        }
        public List<LoaiHinhCongViec> GetList()
        {
            var lstLoaiCV = new List<LoaiHinhCongViec>();
            try
            {
                lstLoaiCV = _loaiCV.Find(l => true).ToList();
            }
            catch (System.Exception ex)
            {
                 _logger.LogError(ex, ex.Message);
            }
            return lstLoaiCV;
        }
    }
}
