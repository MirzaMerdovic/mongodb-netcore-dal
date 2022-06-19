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
                var count = await users.CountDocumentsAsync(x => x.Id != null);

                var products = await productsFactory.Get<Product>(httpContext.RequestAborted);
                count = await products.CountDocumentsAsync(x => x.Id != null);

                var producers = await producersFactory.Get<Producer>(httpContext.RequestAborted);
                count = await producers.CountDocumentsAsync(x => x.Id != null);

                return "OK";
            });

            app.Run();
        }
    }
}