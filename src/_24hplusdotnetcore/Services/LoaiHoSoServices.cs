using System.Collections.Generic;
using _24hplusdotnetcore.Models;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace _24hplusdotnetcore.Services
{
    public class LoaiHoSoServices
    {
        private readonly ILogger<LoaiHoSoServices> _logger;
        private readonly IMongoCollection<LoaiHoSo> _loaiHS;
        public LoaiHoSoServices(ILogger<LoaiHoSoServices> logger, IMongoDbConnection connection)
        {
            _logger = logger;
            var client = new MongoClient(connection.ConnectionString);
            var database = client.GetDatabase(connection.DataBase);
            _loaiHS = database.GetCollection<LoaiHoSo>(Common.MongoCollection.LoaiHoSo);
        }
        public List<LoaiHoSo> GetList()
        {
            var lstLoaiHoSo = new List<LoaiHoSo>();
            try
            {
                lstLoaiHoSo = _loaiHS.Find(h => true).ToList();
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
            return lstLoaiHoSo;
        }
        public LoaiHoSo GetLoaiHobyMaHS(string MaHS)
        {
            var objLoaiHoSo = new LoaiHoSo();
            try
            {
                objLoaiHoSo = _loaiHS.Find(h => h.MaHS == MaHS).FirstOrDefault();
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
            return objLoaiHoSo;
        }
    }
}