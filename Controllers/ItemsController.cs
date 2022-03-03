using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Repositories;
using BackEnd.Entities;
using BackEnd.Dto;

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
        public IEnumerable<ItemDto> GetItems(){
            // Dữ liệu lấy tại Repositories
            var items = repository.GetItems().Select(Item => Item.AsDto());
            return items;

        }
        [HttpGet("[action]/{id}")]
        public ActionResult<ItemDto> GetItem(Guid id){
            ItemDto item = repository.GetItem(id).AsDto();

            if(item is null){
                return NotFound();
            }

            return item;
        }
        [HttpPost]
        public ActionResult<ItemDto> CreateItem(CreateItemDto itemDto){
            Item item = new Item(){
                Id = Guid.NewGuid(),
                Name = itemDto.Name,
                Price = itemDto.Price,
                CreateDate = DateTimeOffset.UtcNow
            };
            repository.CreateItem(item);
            
            return CreatedAtAction(nameof(GetItem), new {id = item.Id}, item.AsDto());
        }
        [HttpPut("[action]]/{id}")]
        public ActionResult<ItemDto> UpdateItem(UpdateItem itemDto, Guid id){
            Item search_item = repository.GetItem(id);

            if(search_item is null){
                return NotFound();
            }
            Item new_Item = new Item(){
                Name = itemDto.Name,
                Id = search_item.Id,
                Price = itemDto.Price,
                CreateDate = search_item.CreateDate
            };
            repository.UpdateItem(new_Item);
            return NoContent();

        }
    }
}