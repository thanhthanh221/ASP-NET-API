using BackEnd.Entities;
using System.Threading.Tasks;
using System.Threading;
using System;
namespace BackEnd.Repositories
{
    public interface IUserRepository 
    {
        void CreateUser(User user);
        User LoginUser(String Email);
             
    }
}