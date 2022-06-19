using System.Text.Json.Serialization;

namespace MyMongo.DataAccess.ProductDb.Producers
{
    public class Producer
    {
        [JsonPropertyName("_id")]
        public string Id { get; set; }
    }
}
