using Catalog.API.Configurations;
using Catalog.API.Data.Interfaces;
using Catalog.API.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Catalog.API.Data.Implementations;

public class CatalogContext : ICatalogContext
{
    public IMongoCollection<Product> Products { get; }

    public CatalogContext(IOptions<MongoDbSettings> options)
    {
        var mongoDbSettings = options.Value;
        var client = new MongoClient(mongoDbSettings.ConnectionString);
        var database = client.GetDatabase(mongoDbSettings.DatabaseName);

        Products = database.GetCollection<Product>(mongoDbSettings.CollectionNames?["ProductsCollectionName"]);

        CatalogContextSeed.SeedData(Products);
    }
}
