using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BackEnd.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BackEnd.Repositories
{
    public class MongoDbProductRepository : IProductRepository
    {
        public const String dataBaseName = "Item_Product";
        public const String CollectionName = "Product";
        private readonly IMongoCollection<Product> ProductCollection;
        private readonly FilterDefinitionBuilder<Product> filterDefinitionBuilder = Builders<Product>.Filter;

        public MongoDbProductRepository(IMongoClient mongoClient)
        {
            IMongoDatabase database = mongoClient.GetDatabase(dataBaseName);
            ProductCollection = database.GetCollection<Product>(CollectionName);
        }
        public async Task<Boolean> CreateProductAsync(Product product)
        {
            await ProductCollection.InsertOneAsync(product);
            return true;
        }
        public async Task DeleteProductAsync(Product product)
        {
            FilterDefinition<Product> filter = filterDefinitionBuilder.Eq(pr => pr.Id, product.Id);
            await ProductCollection.DeleteOneAsync(filter);
        }

        public async Task<IReadOnlyCollection<Product>> GetAllAsync()
        {
            return await ProductCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<Product> GetIdAsync(Guid Id)
        {
            FilterDefinition<Product> filter = filterDefinitionBuilder.Eq(pr => pr.Id, Id);
            Product product =  await (ProductCollection.Find(filter)).SingleOrDefaultAsync();

            return product;
        }

        public async Task UpdateProductAsync(Product product)
        {
            FilterDefinition<Product> filter = filterDefinitionBuilder.Eq(pr => pr.Id, product.Id);
            await ProductCollection.ReplaceOneAsync(filter, product);
        }
    }
}