using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Dto;
using Domain_Layer.Entities;
using Domain_Layer.Interfaces;
using Domain_Layer.Services;
using API.Extension.Mapper;

namespace BackEnd.Controllers
{
    [ApiController]
    [Route("Categories")]
    public class CategoriesController : ControllerBase
    {
        private readonly IAsyncRepository<Category> categoryProduct;

        public CategoriesController(IAsyncRepository<Category> categoryProduct)
        {
            this.categoryProduct  = categoryProduct;
            
        }
        [HttpGet]
        public async Task<ActionResult> GetCategories()
        {
            return Ok((await categoryProduct.GetAllAsync()).Select(p => p.ToCategoryDto()));
        }
        [HttpGet("Id")]
        public async Task<ActionResult<Category>> GetCategory(Guid Id)
        {
            Category category = (await categoryProduct.GetAsync(Id));
            if(category is null)
            {
                return NotFound();
            }
            return Ok(category.ToCategoryDto());
        }
        [HttpPost]
        public async Task<ActionResult<GetCategoryDto>> PostCategory([FromForm] CreateCategoryDto categoryDto)
        {
            // Tạo sản phẩm mới từ bên Phía Client Nhập vào
            Category categoryProductValue = await categoryDto.ToCategoryEntity();
            await categoryProduct.CreateAsync(categoryProductValue);            
            
            return CreatedAtAction(nameof(PostCategory), new {Id = categoryProductValue.Id});
        }
        [HttpPut]
        public async Task<IActionResult> UpdateCategory()
        {
            return NoContent();
        }
        [HttpDelete("Id")]
        public async Task<ActionResult> DeleteCategory(Guid Id)
        {
            Category category = (await categoryProduct.GetAsync(Id));
            if(category == null)
            {
                return NotFound();
            }
            UpLoadFileService.DeleteImage(category.imgCategory, "ImgCategory");
            await categoryProduct.DeleteAsync(category);
            return NoContent();
        }
    }
}