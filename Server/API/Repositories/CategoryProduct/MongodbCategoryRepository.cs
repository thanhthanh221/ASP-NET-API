using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BackEnd.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BackEnd.Repositories
{
    public class MongodbCategoryRepository : ICategoryProduct
    {
        public const string dataBaseName = "Item_Product";
        public const string CollectionName = "Categories";
        // Nơi lưu trữ dữ dữ liệu ánh xạ từ mongodb bảng categories
        private readonly IMongoCollection<Category> categoriesCollection;
        // Lọc tìm kiếm đối tượng lưu trong mongodb
        private readonly FilterDefinitionBuilder<Category> filterBuilder = Builders<Category>.Filter;
        // Inj mongodb vào trang
        public MongodbCategoryRepository(IMongoClient mongoClient)
        {
            IMongoDatabase database = mongoClient.GetDatabase(dataBaseName);

            categoriesCollection = database.GetCollection<Category>(CollectionName);
            
        }
        public async Task CreateCategoryAsync(Category category)
        {
            await categoriesCollection.InsertOneAsync(category);
        }

        public async Task DeleteCategoryAsync(Category category)
        {
            var filter = filterBuilder.Eq(p => p.Id,category.Id);
            await categoriesCollection.DeleteOneAsync(filter);
        }

        public async Task<Category> GetCategoryAsync(Guid id)
        {
            FilterDefinition<Category> Filter = filterBuilder.Eq(item => item.Id, id);
            Category category = await categoriesCollection.Find(Filter).SingleOrDefaultAsync();
            
            return category;
        }

        public async Task<IEnumerable<Category>> GetCategorysAsync()
        {
            // Lọc tất cả danh mục
            return await categoriesCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            FilterDefinition<Category> filter = filterBuilder.Eq(p => p.Id,category.Id);
            // Phương thức RelpaceOne() thay thế 1 tài liệu trong Mongodb 
            await categoriesCollection.ReplaceOneAsync(filter, category);
        }
    }
}