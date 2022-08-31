using System;
using AspNetCore.Identity.MongoDbCore.Models;
using Domain_Layer.Entities.Order;
using MongoDbGenericRepository.Attributes;

namespace Domain_Layer.Entities.Identity
{
    [CollectionName("User")]
    public class ApplicationUser : MongoIdentityUser<Guid>
    {
        public Address Address{get; set;}
        
    }
}