using System;
using BackEnd.Entities;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using System.Collections.Generic;

namespace BackEnd.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserDbContext context;

        public UserRepository(UserDbContext context)
        {
            this.context = context;
        }

        public void CreateUser(User user)
        {
            context.users.Add(user);
            context.SaveChanges();
          
        }

        public User LoginUser(String Email)
        {
            return context.users.Where(p => p.Email.Equals(Email)).SingleOrDefault();          
        }
    }
}