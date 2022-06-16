using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Infreastructure_Layer.Settings;
using Domain_Layer.Interfaces;
using Domain_Layer.Base;
using Infreastructure_Layer.Data.Repositories;

namespace Infreastructure_Layer.Data.MongoDb
{
    // Cài đặt kết nối Mongodb
    public static class Extensions
    {
        public static IServiceCollection AddMongoDb(this IServiceCollection Services)
        {
            Services.AddSingleton(ServiceProvider =>
            {
                IConfiguration Configuration = ServiceProvider.GetService<IConfiguration>();
                
                MongoDbSettings mongoDbSettings = Configuration.GetSection(nameof(MongoDbSettings)).Get<MongoDbSettings>();
                MongoClient mongoClient = new MongoClient(mongoDbSettings.ConnectionString);

                return mongoClient.GetDatabase(mongoDbSettings.Name);
                
            });
            return Services;
        }
        public static void ConverDataMongoDb()
        {
            BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String)); // Chỉnh Giud thành String
            BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String)); // Ngày tháng thành String

        }
        public static IServiceCollection AddMongoRepostory<T>(this IServiceCollection Services, string CollectionName) where T : BaseEntity
        {
            Services.AddSingleton<IAsyncRepository<T>>(serviceProvider => 
            {
                IMongoDatabase database = serviceProvider.GetService<IMongoDatabase>();
                return new MongoDbRepository<T>(database, CollectionName);
            });
            return Services;
        }
    }
}