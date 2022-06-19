using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MyMongo.Infrastructure.Clients;
using System.Collections.Concurrent;

namespace MyMongo.Infrastructure.Databases
{
    public abstract class MongoDatabaseFactory<TOptions>
        : IMongoDatabaseFactory
        where TOptions : MongoDatabaseOptions, new()
    {
        private readonly ConcurrentDictionary<string, IMongoDatabase> _cache = new();

        private readonly TOptions _options;
        private readonly IMongoClientFactory _factory;
        private readonly ILogger _logger;

        protected MongoDatabaseFactory(IOptions<TOptions> options, IMongoClientFactory factory, ILogger logger)
        {
            _options = options.Value;
            _factory = factory;
            _logger = logger;
        }

        public async ValueTask<IMongoDatabase> Get(CancellationToken ct)
        {
            using var _ = _logger.BeginScope("{Method}", $"{nameof(MongoDatabaseFactory<TOptions>)}.{nameof(Get)}");

            var name = _options.Name.Trim().ToLowerInvariant();
            using var nameScope = _logger.BeginScope("{ClientName}", name);

            if (_cache.ContainsKey(name))
            {
                LoggerExtensions.LogDebug(_logger, "Database retrieved from cache");

                return _cache[name];
            }

            var client = _factory.GetOrCreate();
            var names = await (await client.ListDatabaseNamesAsync().ConfigureAwait(false)).ToListAsync(ct).ConfigureAwait(false);

            if (!names.Any(x => x.Equals(name, StringComparison.InvariantCultureIgnoreCase)))
                throw new ArgumentOutOfRangeException($"Database: {name} doesn't exist.");

            _cache[name] = client.GetDatabase(name);

            LoggerExtensions.LogInformation(_logger, "Database added to cache");

            return _cache[name];
        }
    }
}
