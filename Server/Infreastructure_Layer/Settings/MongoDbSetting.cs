using AspNetCore.Identity.MongoDbCore.Models;
namespace Infreastructure_Layer.Settings
{
    public class MongoDbSettings
    {
        public string Host {get; init;}
        public string Port {get; init;}
        public string Name {get; init;} 
        public string ConnectionString{
            get{
                return $"mongodb://{Host}:{Port}";  
            }
        }        
    }
}