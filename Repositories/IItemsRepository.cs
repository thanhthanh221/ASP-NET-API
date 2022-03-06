using System;
using System.Collections.Generic;
using BackEnd.Entities;
using System.Threading.Tasks;
using System.Threading;

namespace BackEnd.Repositories 
{
    public interface IItemsRepository
    {
        Task<IEnumerable<Item>> GetItemsAsync();   
        Task<Item> GetItemAsync(Guid id);
        Task CreateItemAsync(Item item);
        Task UpdateItemAsync(Item item);
        Task DeleteItemAsync(Item item);
    }
}