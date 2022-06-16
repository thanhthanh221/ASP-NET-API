using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Domain_Layer.Base;
using Domain_Layer.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Infreastructure_Layer.Data.Repositories
{
    // Controller sẽ lấy từ đây với từng loại dữ liệu cụ thể
    // Thêm Sửa Xoá In => Từ dataBase
    public class MongoDbRepository<T> : IAsyncRepository<T> where T : BaseEntity
    {
        private readonly IMongoCollection<T> DbCollection;
        private readonly FilterDefinitionBuilder<T> filterBuilder = Builders<T>.Filter;
        public MongoDbRepository(IMongoDatabase database, string CollectionName)
        {
            DbCollection = database.GetCollection<T>(CollectionName);
        }
        public async Task CreateAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentException(nameof(entity));
            }
            await DbCollection.InsertOneAsync(entity);
        }

        public async Task DeleteAsync(T entity)
        {
            FilterDefinition<T> filter = filterBuilder?.Eq(itemlist => itemlist.Id, entity.Id);

            await DbCollection.DeleteOneAsync(filter);

        }

        public async Task<IReadOnlyCollection<T>> GetAllAsync()
        {
            return await DbCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<IReadOnlyCollection<T>> GetsAsync(Expression<Func<T, bool>> filter)
        {
            return await DbCollection.Find(filter).ToListAsync();
        }

        public async Task<T> GetAsync(Guid Id)
        {
            FilterDefinition<T> filter = filterBuilder?.Eq(p => p.Id, Id);

            return await DbCollection.Find(filter).FirstOrDefaultAsync();
        }
        // Delegate Tham số nhận vào là T kiểu trả về là bool
        public async Task<T> GetAsync(Expression<Func<T, bool>> filter) 
        {
            // Tìm kiếm theo hàm delegate
            return await DbCollection.Find(filter).FirstAsync();
        }
        public async Task UpdateAsync(T entity)
        {
            FilterDefinition<T> filter = filterBuilder?.Eq(itemlist => itemlist.Id, entity.Id);

            await DbCollection.ReplaceOneAsync(filter, entity);
        }
    }
}