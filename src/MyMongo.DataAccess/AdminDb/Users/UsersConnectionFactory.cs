using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MyMongo.Infrastructure.Collections;

namespace MyMongo.DataAccess.AdminDb.Users
{
    public interface IUsersCollectionFactory : IMongoCollectionFactory
    {
    }

    public sealed class UsersCollectionFactory : MongoCollectionFactory<UsersCollectionOptions, IAdminDbDatabaseFactory>, IUsersCollectionFactory
    {
        public UsersCollectionFactory(
            IOptions<UsersCollectionOptions> options,
            IAdminDbDatabaseFactory databaseFactory,
            ILogger<UsersCollectionFactory> logger)
            : base(options, databaseFactory, logger)
        {
        }
    }
}
