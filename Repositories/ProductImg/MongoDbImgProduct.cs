using System;
using System.Threading.Tasks;
using BackEnd.Entities;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Linq;

namespace BackEnd.Repositories
{
    public class MongoDbImgProduct : IImgProduct
    {
        public const string dataBaseName = "Item_Product";
        public const string CollectionName = "ImgProduct";
        // Nơi lưu trữ dữ dữ liệu ánh xạ từ mongodb bảng item
        private readonly IMongoCollection<ImgProduct> imgProductsCollection;
        // Lọc tìm kiếm đối tượng lưu trong mongodb
        private readonly FilterDefinitionBuilder<ImgProduct> filterBuilder = Builders<ImgProduct>.Filter;
        // Inj mongodb vào trang
        public MongoDbImgProduct(IMongoClient mongoClient)
        {
            IMongoDatabase database = mongoClient.GetDatabase(dataBaseName);
            
            imgProductsCollection = database.GetCollection<ImgProduct>(CollectionName);
            
        }

        public async Task CreateImgProductAsync(ImgProduct imgProduct)
        {
            await imgProductsCollection.InsertOneAsync(imgProduct);
        }

        public async Task DeleteImgProductAsync(ImgProduct imgProduct)
        {
            FilterDefinition<ImgProduct> filter = filterBuilder.Eq(pr => pr.Id, imgProduct.Id);
            await imgProductsCollection.DeleteOneAsync(filter);
        }

        public async Task<ImgProduct> GetImgProductAsync(Guid Id)
        {
            FilterDefinition<ImgProduct> filter = filterBuilder.Eq(pr => pr.Id, Id);
            ImgProduct imgProduct = await imgProductsCollection.Find(filter).SingleOrDefaultAsync();
            return imgProduct;
        }
        public async Task<IReadOnlyCollection<ImgProduct>> GetImgProductsUserAsync(Guid ProductId)
        {
            List<ImgProduct> ListImgProduct = await imgProductsCollection.Find(new BsonDocument()).ToListAsync();

            IEnumerable<ImgProduct> imgUseProduct = from a in ListImgProduct where a.ProductId.Equals(ProductId) select a;

            return imgUseProduct.ToList();
        }

        public async Task UpdateImgProductAsync(Guid Id, ImgProduct imgProduct)
        {
            FilterDefinition<ImgProduct> filter = filterBuilder.Eq(pr => pr.Id, Id);
            await imgProductsCollection.ReplaceOneAsync(filter, imgProduct);
        }
    }
}