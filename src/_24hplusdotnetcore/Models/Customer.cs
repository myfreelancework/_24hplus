using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace _24hplusdotnetcore.Models
{
    public class Customer
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string MaKH { get; set; }
        [BsonRequired]
        public string HoTen { get; set; }
        [BsonRequired]
        public string CMND { get; set; }
        [BsonRequired]
        public DateTime NgayHetHangCMD { get; set; }
        [BsonRequired]
        public DateTime NgaySinh { get; set; }
        [BsonRequired]
        public string SDT { get; set; }
        [BsonRequired]
        public bool SDTChinhChu { get; set; }
        [BsonRequired]
        public int GioiTinh { get; set; } //0: Male; 1: Female
        [BsonRequired]
        public string Tinh { get; set; }
        [BsonRequired]
        public string Huyen { get; set; }
        [BsonRequired]
        public string Xa { get; set; }
        [BsonRequired]
        public string SoNhaTenDuong { get; set; }
        [BsonRequired]
        public string LoaiSanPham { get; set; }
        [BsonRequired]
        public string TenSanPham { get; set; }
        [BsonRequired]
        public string TenSale { get; set; }
        [BsonRequired]
        public string SaleCode { get; set; }
        [BsonRequired]
        public string SDTSale { get; set; }
        [BsonRequired]
        public string SaleGhiChu { get; set; }
    }
}
