using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Repositories;
using BackEnd.Entities;

namespace BackEnd.Controllers 
{
    //Get/items

    [ApiController]
    [Route("Items")]
    public class ItemsController : ControllerBase
    {
        private readonly IItemsRepository repository;

        public ItemsController(IItemsRepository repository)
        {
            this.repository = repository;
        }
        [HttpGet]
        public IEnumerable<Item> GetItems(){
            // Dữ liệu lấy tại Repositories
            var items = repository.GetItems();
            return items;

        }
        [HttpGet("[action]/{id}")]
        public ActionResult<Item> GetItem(Guid id){
            Item item = repository.GetItems(id);
            return item;
        }
    }
}