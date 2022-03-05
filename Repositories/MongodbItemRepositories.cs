using System;
using System.Collections.Generic;
using BackEnd.Entities;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;

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

        public void CreateItem(Item item)
        {
            // Phương thức thêm 1 đối tượng vào mongodb (InsertOne)
            itemsCollection.InsertOne(item);
            
        }

        public void DeleteItem(Item item)
        {
            var filter = filterBuilder.Eq(p => p.Id,item.Id);
            itemsCollection.DeleteOne(filter);
        }

        public Item GetItem(Guid id)
        {
            FilterDefinition<Item> Filter = filterBuilder.Eq(item => item.Id, id);
            Item item = itemsCollection.Find(Filter).SingleOrDefault();
            
            return item;
            
        }

        public IEnumerable<Item> GetItems()
        {
            // Lọc tất cả các bson cho yêu cầu
            return itemsCollection.Find(new BsonDocument()).ToList();
        }

        public void UpdateItem(Item item)
        {
            FilterDefinition<Item> filter = filterBuilder.Eq(p => p.Id,item.Id);
            // Phương thức RelpaceOne() thay thế 1 tài liệu trong Mongodb 
            itemsCollection.ReplaceOne(filter, item);
        }
    }
}