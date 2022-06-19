using MongoDB.Driver;

namespace MyMongo.Infrastructure.Collections
{
    public interface IMongoCollectionFactory
    {
        ValueTask<IMongoCollection<T>> Get<T>(CancellationToken ct) where T : class, new();
    }
}
