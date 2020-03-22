using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace _24hplusdotnetcore.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        
        [BsonRequired]
        public string UserEmail { get; set; }

        [BsonRequired]
        public string UserPassword { get; set; }

        [BsonRequired]
        [BsonRepresentation(BsonType.Int32)]
        public int RoleId { get; set; }
    }
}