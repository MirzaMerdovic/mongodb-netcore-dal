using System.Text.Json.Serialization;

namespace MyMongo.DataAccess.ProductDb.Products
{
    public class Product
    {
        [JsonPropertyName("_id")]
        public string Id { get; set; }
    }
}
