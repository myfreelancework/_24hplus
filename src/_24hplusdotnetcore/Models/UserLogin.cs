using MongoDB.Bson.Serialization.Attributes;

namespace _24hplusdotnetcore.Models
{
    public class UserLogin
    {
        [BsonRequired]
        public string UserName { get; set; }
        [BsonRequired]
        public string Password { get; set; }
        public string uuid { get; set; }
        public string ostype { get; set; }
        public string token { get; set; }
        
    }
}