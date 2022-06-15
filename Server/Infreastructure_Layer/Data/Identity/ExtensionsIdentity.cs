using System;
using Domain_Layer.Entities.Identity;
using Infreastructure_Layer.Settings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Infreastructure_Layer.Data.Identity
{
    public static class ExtensionsIdentity
    {
        public static IServiceCollection AddIdentityMongoDb(this IServiceCollection Services, IConfiguration Configuration)
        {
            MongoDbSettings mongoDbsettings =  Configuration.GetSection(nameof(MongoDbSettings)).Get<MongoDbSettings>();
            Services.AddIdentity<ApplicationUser, ApplicationRole>().
                    AddMongoDbStores<ApplicationUser, ApplicationRole, Guid>(
                        mongoDbsettings.ConnectionString, "Item_Product");
            return Services;
        }
        
    }
}