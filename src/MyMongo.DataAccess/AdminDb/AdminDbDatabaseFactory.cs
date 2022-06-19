using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MyMongo.Infrastructure.Clients;
using MyMongo.Infrastructure.Databases;

namespace MyMongo.DataAccess.AdminDb
{
    public interface IAdminDbDatabaseFactory : IMongoDatabaseFactory
    {
    }

    public sealed class AdminDbDatabaseFactory : MongoDatabaseFactory<AdminDbDatabaseOptions>, IAdminDbDatabaseFactory
    {
        public AdminDbDatabaseFactory(
            IOptions<AdminDbDatabaseOptions> options,
            IMongoClientFactory factory,
            ILogger<AdminDbDatabaseFactory> logger)
            : base(options, factory, logger)
        {
        }
    }
}
