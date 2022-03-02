using System.Collections.Generic;
using BackEnd.Entities;
using System;
using System.Linq;

namespace BackEnd.Repositories{

    public class InMemItemsRepository : IItemsRepository
    {
        private readonly List<Item> items = new()
        {
            new Item { Id = Guid.NewGuid(), Name = "Potion", Price = 9, CreateDate = DateTimeOffset.UtcNow },
            new Item { Id = Guid.NewGuid(), Name = "Iron Wrord", Price = 20, CreateDate = DateTimeOffset.UtcNow },
            new Item { Id = Guid.NewGuid(), Name = "Bronze Shield", Price = 18, CreateDate = DateTimeOffset.UtcNow },


        };
        public IEnumerable<Item> GetItems()
        {
            return items;
        }
        public Item GetItems(Guid id)
        {
            return items.Where(Item => Item.Id == id).FirstOrDefault();
        }
    }
}