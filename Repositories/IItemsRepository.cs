using System;
using System.Collections.Generic;
using BackEnd.Entities;

namespace BackEnd.Repositories 
{
    public interface IItemsRepository
    {
        IEnumerable<Item> GetItems();   
        Item GetItem(Guid id);
        void CreateItem(Item item);
        void UpdateItem(Item item);
        void DeleteItem(Item item);
    }
}