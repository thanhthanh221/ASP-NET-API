using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BackEnd.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BackEnd.Repositories
{
    public class MongoDbProductsReviews : IProductsReviews
    {
        public const string dataBaseName = "Item_Product";
        public const string CollectionName = "Products Reviews";
  
        private readonly IMongoCollection<ProductsReviews> PrReviewsCollection;
        private readonly FilterDefinitionBuilder<ProductsReviews> filterBuilder = Builders<ProductsReviews>.Filter;

        public MongoDbProductsReviews(IMongoClient mongoClient)
        {
            IMongoDatabase database = mongoClient.GetDatabase(dataBaseName);
            
            PrReviewsCollection = database.GetCollection<ProductsReviews>(CollectionName);
            
        }

        public async Task CreateProductsReviewsAsync(ProductsReviews productsReviews)
        {
            // Phương thức thêm 1 đối tượng vào mongodb (InsertOne)
            await PrReviewsCollection.InsertOneAsync(productsReviews);
        }

        public async Task DeleteCategoryAsync(ProductsReviews productsReviews)
        {
            var filter = filterBuilder.Eq(p => p.Id,productsReviews.Id);
            await PrReviewsCollection.DeleteOneAsync(filter);
        }

        public async Task<ProductsReviews> GetProductsReviewAsync(Guid Id)
        {
            FilterDefinition<ProductsReviews> Filter = filterBuilder.Eq(PrR => PrR.Id, Id);
            ProductsReviews productsReviews = await PrReviewsCollection.Find(Filter).SingleOrDefaultAsync();
            
            return productsReviews;
        }

        public async Task<IEnumerable<ProductsReviews>> GetProductsReviewsAsync()
        {
            return await PrReviewsCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task UpdateProductsReviewsAsync(ProductsReviews productsReviews)
        {
            FilterDefinition<ProductsReviews> Filter = filterBuilder.Eq(PrR => PrR.Id, productsReviews.Id);
            // Phương thức RelpaceOne() thay thế 1 tài liệu trong Mongodb 
            await PrReviewsCollection.ReplaceOneAsync(Filter, productsReviews);
        }

    }
}