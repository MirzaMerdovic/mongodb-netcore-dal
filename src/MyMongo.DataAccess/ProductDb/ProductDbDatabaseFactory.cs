using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MyMongo.Infrastructure.Clients;
using MyMongo.Infrastructure.Databases;

namespace MyMongo.DataAccess.ProductDb
{
    public interface IProductDbDatabaseFactory : IMongoDatabaseFactory
    {
    }

    public sealed class ProductDbDatabaseFactory :
        MongoDatabaseFactory<ProductDbDatabaseOptions>,
        IProductDbDatabaseFactory
    {
        public ProductDbDatabaseFactory(
            IOptions<ProductDbDatabaseOptions> options,
            IMongoClientFactory factory,
            ILogger<ProductDbDatabaseFactory> logger)
            : base(options, factory, logger)
        {
        }
    }
}
