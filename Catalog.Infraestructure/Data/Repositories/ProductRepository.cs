using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Catalog.Infraestructure.Data.Repositories
{
    public class ProductRepository : IProductRespository, IProductTypeRepository, IBrandRespository
    {
        public ICatalogContext _dbContext;
        public ProductRepository(ICatalogContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public async Task<Product> CreateProduct(Product product)
        {
            await _dbContext.Products.InsertOneAsync(product);
            return product;
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            var updateResult= await _dbContext.Products.ReplaceOneAsync(
                                p => p.Id == product.Id, product);
            return updateResult.IsAcknowledged && updateResult.ModifiedCount>0;
        }

        public async Task<bool> DeleteProduct(string id)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Id,id);
            DeleteResult deleteResult=await _dbContext.Products.DeleteOneAsync(filter);
            return deleteResult.IsAcknowledged && deleteResult.DeletedCount>0;
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _dbContext.Products.Find(p => true).ToListAsync<Product>();
        }

        public async Task<Product> GetProduct(string id)
        {
            return await _dbContext.Products.Find(p => p.Id==id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByName(string name)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p=> p.Name,name);
            return await _dbContext.Products.Find(filter).ToListAsync<Product>();
        }

        public async Task<IEnumerable<Product>> GetProductsByBrand(string brand)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Brand.Name, brand);
            return await _dbContext.Products.Find(filter).ToListAsync<Product>();
        }

        public async Task<IEnumerable<ProductType>> GetProductTypes()
        {
            return await _dbContext.ProductTypes.Find(p=>true).ToListAsync<ProductType>();
        }  

        public async Task<IEnumerable<Brand>> GetBrands()
        {
            return await _dbContext.Brands.Find(b=>true).ToListAsync<Brand>();
        }



        
    }
}
