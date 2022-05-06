using System;
// Setup Các thuộc tính liên quan đến Identity
using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;

namespace BackEnd.Repositories
{
    [CollectionName("Roles")]
    public class ApplicationRole : MongoIdentityRole<Guid>
    {

        
    }
}