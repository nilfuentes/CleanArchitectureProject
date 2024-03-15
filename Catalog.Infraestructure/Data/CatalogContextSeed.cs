using Catalog.Core.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Catalog.Infraestructure.Data
{
    public static class CatalogContextSeed
    {
        public static void SeedData(IMongoCollection<Product> productMongoCollection)
        {
            bool checkProducts = productMongoCollection.Find(p => true).Any();
            //string path = Path.Combine("Data", "SeedData", "products.json");
            string path ="../Catalog/Infraestructure/Data/SeedData/products.json";
            if (!checkProducts)
            {
                string productsData = File.ReadAllText(path);
                List<Product>? products = JsonSerializer.Deserialize<List<Product>>(productsData);

                if (products != null)
                {
                    foreach (Product product in products)
                    {
                        productMongoCollection.InsertOneAsync(product);
                    }
                }
            }
        }
    }
}
