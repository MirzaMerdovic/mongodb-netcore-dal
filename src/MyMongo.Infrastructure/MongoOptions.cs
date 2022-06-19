namespace MyMongo.Infrastructure
{
    public sealed class MongoConnectionOptions
    {
        public string Name { get; set; } = string.Empty;

        public string Url { get; set; } = string.Empty;
    }

    public abstract class MongoDatabaseOptions
    {
        public string Name { get; set; } = string.Empty;
    }

    public abstract class MongoCollectionOptions
    {
        public string Name { get; set; } = string.Empty;
    }
}
