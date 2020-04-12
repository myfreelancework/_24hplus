using MongoDB.Bson.Serialization.Attributes;

namespace _24hplusdotnetcore.Models
{
    public class Partner
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        public string PartnerId { get; set; }
        public string PartnerName { get; set; }
    }
}
