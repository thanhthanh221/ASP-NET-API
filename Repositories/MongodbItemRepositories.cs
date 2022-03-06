using System;
using System.Collections.Generic;
using BackEnd.Entities;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace BackEnd.Repositories{
    public class MongodbItemRepositories : IItemsRepository
    {
        public const string dataBaseName = "Item_Product";
        public const string CollectionName = "items";
        // Nơi lưu trữ dữ dữ liệu ánh xạ từ mongodb bảng item
        private readonly IMongoCollection<Item> itemsCollection;
        // Lọc tìm kiếm đối tượng lưu trong mongodb
        private readonly FilterDefinitionBuilder<Item> filterBuilder = Builders<Item>.Filter;
        // Inj mongodb vào trang
        public MongodbItemRepositories(IMongoClient mongoClient)
        {
            IMongoDatabase database = mongoClient.GetDatabase(dataBaseName);
            
            itemsCollection = database.GetCollection<Item>(CollectionName);
            
        }

        public async Task CreateItemAsync(Item item)
        {
            // Phương thức thêm 1 đối tượng vào mongodb (InsertOne)
            await itemsCollection.InsertOneAsync(item);
            
        }

        public async Task DeleteItemAsync(Item item)
        {
            var filter = filterBuilder.Eq(p => p.Id,item.Id);
            await itemsCollection.DeleteOneAsync(filter);
        }

        public async Task<Item> GetItemAsync(Guid id)
        {
            FilterDefinition<Item> Filter = filterBuilder.Eq(item => item.Id, id);
            Item item = await itemsCollection.Find(Filter).SingleOrDefaultAsync();
            
            return item;
            
        }

        public async Task<IEnumerable<Item>> GetItemsAsync()
        {
            // Lọc tất cả các bson cho yêu cầu
            return await itemsCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task UpdateItemAsync(Item item)
        {
            FilterDefinition<Item> filter = filterBuilder.Eq(p => p.Id,item.Id);
            // Phương thức RelpaceOne() thay thế 1 tài liệu trong Mongodb 
            await itemsCollection.ReplaceOneAsync(filter, item);
        }
    }
}