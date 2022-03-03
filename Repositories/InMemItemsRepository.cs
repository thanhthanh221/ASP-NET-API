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

        public void CreateItem(Item item)
        {
            items.Add(item);
        }

        public IEnumerable<Item> GetItems()
        {
            return items;
        }
        public Item GetItem(Guid id)
        {
            return items.Where(Item => Item.Id == id).FirstOrDefault();
        }

        public void UpdateItem(Item item)
        {
            Item Check_item = items.Where(p=> p.Id == item.Id).FirstOrDefault();

            Check_item = item;
        }
    }
}