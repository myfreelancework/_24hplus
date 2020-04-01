using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace _24hplusdotnetcore.Models
{
    public class ProductCategory
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string MaLoaiSanPham { get; set; }
        [BsonRequired]
        public string TenLoaiSanPham { get; set; }
        public string GhiChuLoaiSanPhan { get; set; }
    }
}
