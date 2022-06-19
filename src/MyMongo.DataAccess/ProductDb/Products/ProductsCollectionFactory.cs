using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MyMongo.Infrastructure.Collections;

namespace MyMongo.DataAccess.ProductDb.Products
{
    public interface IProductsCollectionFactory : IMongoCollectionFactory
    {
    }

    public sealed class ProductsCollectionFactory :
        MongoCollectionFactory<ProductsCollectionOptions, IProductDbDatabaseFactory>,
        IProductsCollectionFactory
    {
        public ProductsCollectionFactory(
            IOptions<ProductsCollectionOptions> options,
            IProductDbDatabaseFactory databaseFactory,
            ILogger<ProductsCollectionFactory> logger)
            : base(options, databaseFactory, logger)
        {
        }
    }
}
