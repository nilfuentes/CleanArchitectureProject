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
    public static class BrandContextSeed
    {
        public static void SeedData(IMongoCollection<Brand> brandMongoCollection)
        {
            bool checkBrands = brandMongoCollection.Find(b => true).Any();
            string path = Path.Combine("Data", "SeedData", "brands.json");

            if (!checkBrands)
            {
                string brandsData = File.ReadAllText(path);
                List<Brand>? brands = JsonSerializer.Deserialize<List<Brand>>(brandsData);

                if (brands != null)
                {
                    foreach (Brand brand in brands)
                    {
                        brandMongoCollection.InsertOneAsync(brand);
                    }
                }
            }
        }
    }
}
