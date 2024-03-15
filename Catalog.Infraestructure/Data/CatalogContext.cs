using Catalog.Core.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Infraestructure.Data
{
    public class CatalogContext: ICatalogContext
    {
        public IMongoCollection<Brand> Brands { get; }
        public IMongoCollection<ProductType> ProductTypes { get; }
        public IMongoCollection<Product> Products { get; }

        public CatalogContext(IConfiguration configuration) 
        {
            MongoClient client = new MongoClient(
                configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var database = client.GetDatabase(
                configuration.GetValue<string>("DatabaseSettings:DatabaseName"));

            Brands = database.GetCollection<Brand>(
                configuration.GetValue<string>("DatabaseSettings:BrandsCollection"));
            ProductTypes = database.GetCollection<ProductType>(
                configuration.GetValue<string>("DatabaseSettings:ProductTypesCollection"));
            Products = database.GetCollection<Product>(
                configuration.GetValue<string>("DatabaseSettings:Products"));

            BrandContextSeed.SeedData(Brands);
            ProductTypeContextSeed.SeedData(ProductTypes);
            CatalogContextSeed.SeedData(Products);
        }
    }
}
