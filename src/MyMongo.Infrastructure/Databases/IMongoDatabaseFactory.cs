using MongoDB.Driver;

namespace MyMongo.Infrastructure.Databases
{
    public interface IMongoDatabaseFactory
    {
        ValueTask<IMongoDatabase> Get(CancellationToken ct);
    }
}
