using System;
using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;

namespace Domain_Layer.Entities.Identity
{
   [CollectionName("Roles")]
    public class ApplicationRole : MongoIdentityRole<Guid>
    {
        

        
    }
}