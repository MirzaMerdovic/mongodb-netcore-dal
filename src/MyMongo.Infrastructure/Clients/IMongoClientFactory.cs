using MongoDB.Driver;

namespace MyMongo.Infrastructure.Clients
{
    public interface IMongoClientFactory
    {
        IMongoClient GetOrCreate();
    }
}
