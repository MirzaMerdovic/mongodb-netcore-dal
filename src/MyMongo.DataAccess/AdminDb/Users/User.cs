using System.Text.Json.Serialization;

namespace MyMongo.DataAccess.AdminDb.Users
{
    public class User
    {
        [JsonPropertyName("_id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
