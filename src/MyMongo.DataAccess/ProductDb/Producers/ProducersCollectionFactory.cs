using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MyMongo.Infrastructure.Collections;

namespace MyMongo.DataAccess.ProductDb.Producers
{
    public interface IProducersCollectionFactory : IMongoCollectionFactory
    {
    }

    public sealed class ProducersCollectionFactory :
        MongoCollectionFactory<ProducersCollectionOptions, IProductDbDatabaseFactory>,
        IProducersCollectionFactory
    {
        public ProducersCollectionFactory(
            IOptions<ProducersCollectionOptions> options,
            IProductDbDatabaseFactory databaseFactory,
            ILogger<ProducersCollectionFactory> logger)
            : base(options, databaseFactory, logger)
        {
        }
    }
}
