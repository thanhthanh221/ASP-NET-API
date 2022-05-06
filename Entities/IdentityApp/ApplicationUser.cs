using System;
// Setup Các thuộc tính liên quan đến Identity
using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;

namespace BackEnd.Entities
{
    [CollectionName("User")]
    public class ApplicationUser : MongoIdentityUser<Guid>
    {
        
    }
}