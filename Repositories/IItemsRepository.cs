using System;
using System.Collections.Generic;
using BackEnd.Entities;

namespace BackEnd.Repositories 
{
    public interface IItemsRepository
    {
        IEnumerable<Item> GetItems();
        Item GetItems(Guid id);
    }
}