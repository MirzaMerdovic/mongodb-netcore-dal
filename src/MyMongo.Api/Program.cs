using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using MyMongo.DataAccess;
using MyMongo.DataAccess.AdminDb;
using MyMongo.DataAccess.AdminDb.Users;
using MyMongo.DataAccess.ProductDb;
using MyMongo.DataAccess.ProductDb.Producers;
using MyMongo.DataAccess.ProductDb.Products;
using MyMongo.Infrastructure;
using MyMongo.Infrastructure.Clients;

namespace MyMongo.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            ConfigurationManager configuration = builder.Configuration;
            var services = builder.Services;

            services.AddAdminDb(configuration);
            services.AddProductDb(configuration);
            //services.Configure<MongoConnectionOptions>(configuration.GetSection("MongoOptions:Client"));
            //services.Configure<AdminDbDatabaseOptions>(configuration.GetSection("MongoOptions:Databases:0"));
            //services.Configure<UsersCollectionOptions>(configuration.GetSection("MongoOptions:Databases:0:Collections:0"));
            //services.Configure<ProductDbDatabaseOptions>(configuration.GetSection("MongoOptions:Databases:1"));
            //services.Configure<ProductsCollectionOptions>(configuration.GetSection("MongoOptions:Databases:1:Collections:0"));
            //services.Configure<ProducersCollectionOptions>(configuration.GetSection("MongoOptions:Databases:1:Collections:1"));

            //services.AddSingleton<IMongoClientFactory, MongoClientFactory>();
            //services.AddSingleton<IAdminDbDatabaseFactory, AdminDbDatabaseFactory>();
            //services.AddSingleton<IUsersCollectionFactory, UsersCollectionFactory>();
            //services.AddSingleton<IProductDbDatabaseFactory, ProductDbDatabaseFactory>();
            //services.AddSingleton<IProductsCollectionFactory, ProductsCollectionFactory>();
            //services.AddSingleton<IProducersCollectionFactory, ProducersCollectionFactory>();

            var app = builder.Build();

            app.UseHttpsRedirection();


            app.MapGet(
                "/mongo/",
                async (
                    HttpContext httpContext,
                    IUsersCollectionFactory usersFactory,
                    IProductsCollectionFactory productsFactory,
                    IProducersCollectionFactory producersFactory) =>
            {
                var users = await usersFactory.Get<User>(httpContext.RequestAborted);
                var count = await users.CountDocumentsAsync<User>(x => x.Id != null);

                var products = await productsFactory.Get<Product>(httpContext.RequestAborted);
                count = await products.CountDocumentsAsync<Product>(x => x.Id != null);

                var producers = await producersFactory.Get<Producer>(httpContext.RequestAborted);
                count = await producers.CountDocumentsAsync<Producer>(x => x.Id != null);

                return "OK";
            });

            app.Run();
        }
    }
}