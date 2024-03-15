using Catalog.Core.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Infraestructure.Data
{
    public interface ICatalogContext
    {
        IMongoCollection<Brand> Brands { get; }
        IMongoCollection<ProductType> ProductTypes { get; }
        IMongoCollection<Product> Products { get; }
    }
}
