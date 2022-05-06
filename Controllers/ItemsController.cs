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
        public async Task<IEnumerable<ItemDto>> GetItems(){
            // Dữ liệu lấy tại Repositories
            IEnumerable<ItemDto> items = (await repository.GetItemsAsync()).Select(Item => Item.AsDto());
            return items;

        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDto>> GetItem(Guid id){
            
            Item item = (await repository.GetItemAsync(id));
            if(item is null){
                return NotFound();
            }
            ItemDto itemDto = item.AsDto();

            return itemDto;
        }
        [HttpPost]
        public ActionResult<ItemDto> CreateItem(CreateItemDto itemDto){
            Item item = new Item(){
                Id = Guid.NewGuid(),
                Name = itemDto.Name,
                Price = itemDto.Price,
                CreateDate = DateTimeOffset.UtcNow
            };
            repository.CreateItemAsync(item);

            // Tạo thành công Item
            
            return CreatedAtAction(nameof(GetItem), new {id = item.Id}, item.AsDto());
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<ItemDto>> UpdateItem(UpdateItem itemDto, Guid id){
            Item search_item = await repository.GetItemAsync(id);

            if(search_item is null){
                return NotFound();
            }
            Item updateItem = search_item with {
                Name = itemDto.Name,
                Price = itemDto.Price
            };
            await repository.UpdateItemAsync(updateItem);
            return NoContent();

        
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<ItemDto>> DeleteItem(Guid id){
            Item search_Item = await repository.GetItemAsync(id);
            if(search_Item is null){
                return NotFound();
            }
            await repository.DeleteItemAsync(search_Item);
            return NoContent();
        }   
    }
}