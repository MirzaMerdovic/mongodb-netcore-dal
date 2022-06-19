using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyMongo.DataAccess.AdminDb;
using MyMongo.DataAccess.AdminDb.Users;
using MyMongo.DataAccess.ProductDb;
using MyMongo.DataAccess.ProductDb.Producers;
using MyMongo.DataAccess.ProductDb.Products;
using MyMongo.Infrastructure;
using MyMongo.Infrastructure.Clients;
using System.Configuration;

namespace MyMongo.DataAccess
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAdminDb(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMongoClient(configuration);

            services.Configure<AdminDbDatabaseOptions>(configuration.GetSection("MongoOptions:Databases:0"));
            services.Configure<UsersCollectionOptions>(configuration.GetSection("MongoOptions:Databases:0:Collections:0"));

            services.AddSingleton<IAdminDbDatabaseFactory, AdminDbDatabaseFactory>();
            services.AddSingleton<IUsersCollectionFactory, UsersCollectionFactory>();

            return services;
        }

        public static IServiceCollection AddProductDb(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMongoClient(configuration);

            services.Configure<ProductDbDatabaseOptions>(configuration.GetSection("MongoOptions:Databases:1"));
            services.Configure<ProductsCollectionOptions>(configuration.GetSection("MongoOptions:Databases:1:Collections:0"));
            services.Configure<ProducersCollectionOptions>(configuration.GetSection("MongoOptions:Databases:1:Collections:1"));

            services.AddSingleton<IProductDbDatabaseFactory, ProductDbDatabaseFactory>();
            services.AddSingleton<IProductsCollectionFactory, ProductsCollectionFactory>();
            services.AddSingleton<IProducersCollectionFactory, ProducersCollectionFactory>();

            return services;
        }

        private static IServiceCollection AddMongoClient(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MongoConnectionOptions>(configuration.GetSection("MongoOptions:Client"));

            services.AddSingleton<IMongoClientFactory, MongoClientFactory>();

            return services;
        }
    }
}
