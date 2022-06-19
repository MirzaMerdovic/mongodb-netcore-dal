using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MyMongo.Infrastructure.Databases;
using System.Collections.Concurrent;

namespace MyMongo.Infrastructure.Collections
{
    public abstract class MongoCollectionFactory<TOptions, TFactory>
        : IMongoCollectionFactory
        where TOptions : MongoCollectionOptions, new()
        where TFactory : IMongoDatabaseFactory
    {
        private readonly ConcurrentDictionary<string, object> _cache = new();

        private readonly TOptions _options;
        private readonly TFactory _databaseFactory;
        private readonly ILogger _logger;

        protected MongoCollectionFactory(
            IOptions<TOptions> options,
            TFactory databaseFactory,
            ILogger logger)
        {
            _options = options.Value;
            _databaseFactory = databaseFactory;
            _logger = logger;
        }

        public async ValueTask<IMongoCollection<T>> Get<T>(CancellationToken ct) where T : class, new()
        {
            using var _ = _logger.BeginScope("{Method}", $"MongoCollectionFactory.{nameof(Get)}");

            var name = _options.Name.Trim().ToLowerInvariant();
            using var nameScope = _logger.BeginScope("{ClientName}", name);

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("CollectionName not configured.");

            IMongoDatabase database = await _databaseFactory.Get(ct).ConfigureAwait(false);

            if (_cache.ContainsKey(name))
            {
                LoggerExtensions.LogDebug(_logger, "Collection retrieved from cache");
            }
            else
            {
                _cache[name] = GetMongoCollection(database, name);
                LoggerExtensions.LogInformation(_logger, "Collection added to cache");
            }

            return (IMongoCollection<T>)_cache[name];

            static IMongoCollection<T> GetMongoCollection(IMongoDatabase database, string name)
            {
                var getCollectionMethod = database!.GetType()!.GetMethod(nameof(IMongoDatabase.GetCollection));
                var definition = getCollectionMethod!.GetGenericMethodDefinition();
                var getCollection = getCollectionMethod.MakeGenericMethod(new Type[] { typeof(T) });
                var collection = getCollection!.Invoke(database!, new object[] { name, new MongoCollectionSettings() });

                return (IMongoCollection<T>)collection!;
            }
        }
    }
}
