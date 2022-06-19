using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Collections.Concurrent;

namespace MyMongo.Infrastructure.Clients
{
    public sealed class MongoClientFactory : IMongoClientFactory
    {
        private readonly ConcurrentDictionary<string, IMongoClient> _cache = new ConcurrentDictionary<string, IMongoClient>();

        private readonly MongoConnectionOptions _options;
        private readonly ILogger _logger;

        public MongoClientFactory(IOptions<MongoConnectionOptions> options, ILogger<MongoClientFactory> logger)
        {
            _options = options.Value ?? throw new ArgumentNullException(nameof(options));
            _logger = logger;
        }

        public IMongoClient GetOrCreate()
        {
            using var _ = _logger.BeginScope("{Method}", $"{nameof(MongoClientFactory)}.{nameof(GetOrCreate)}");

            var name = _options.Name.Trim().ToLowerInvariant();
            using var nameScope = _logger.BeginScope("{ClientName}", name);

            if (_cache.ContainsKey(name))
            {
                LoggerExtensions.LogDebug(_logger, "Client retrieved from cache");

                return _cache[name];
            }

            _cache[name] = new MongoClient(new MongoUrl(_options.Url));

            using var urlScope = _logger.BeginScope("{Url}", _options.Url);
            LoggerExtensions.LogInformation(_logger, "Client add to cache");

            return _cache[name];
        }
    }
}
