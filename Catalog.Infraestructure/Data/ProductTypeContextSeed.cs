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
    public static class ProductTypeContextSeed
    {
        public static void SeedData(IMongoCollection<ProductType> productTypeMongoCollection)
        {
            bool checkProductTypes = productTypeMongoCollection.Find(p => true).Any();
            string path = Path.Combine("Data", "SeedData", "types.json");

            if (!checkProductTypes)
            {
                string productTypesData = File.ReadAllText(path);
                List<ProductType>? productTypes = JsonSerializer.Deserialize<List<ProductType>>(productTypesData);

                if (productTypes != null)
                {
                    foreach (ProductType productType in productTypes)
                    {
                        productTypeMongoCollection.InsertOneAsync(productType);
                    }
                }
            }
        }
    }
}
