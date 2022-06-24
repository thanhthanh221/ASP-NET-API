using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Dto;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading;
using Microsoft.AspNetCore.Hosting;
using Domain_Layer.Entities;
using Domain_Layer.Interfaces;
using Domain_Layer.Services;

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
            IReadOnlyCollection<Category> categories = new List<Category>();
            categories = await categoryProduct.GetAllAsync();
            return Ok(categories);
        }
        [HttpGet("Id")]
        public async Task<ActionResult<Category>> GetCategory(Guid Id)
        {
            Category category = (await categoryProduct.GetAsync(Id));
            if(category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }
        [HttpPost]
        public async Task<ActionResult<GetCategoryDto>> PostCategory([FromForm] CreateCategoryDto categoryDto)
        {
            // Tạo sản phẩm mới từ bên Phía Client Nhập vào
            Category categoryProductValue = new Category {
                Id = Guid.NewGuid(),
                name = categoryDto.name,
                parentsCategoryId = categoryDto.parentsCategoryId,
                subCategoryId = categoryDto.subCategoryId,
                products =  categoryDto.products,
                imgCategory = await UpLoadFileService.SaveImage(categoryDto.imgCategory, "ImgCategory")
                
            };
            await categoryProduct.CreateAsync(categoryProductValue);            
            
            return CreatedAtAction(nameof(PostCategory), new {Id = categoryProductValue.Id});
        }
        [HttpDelete]
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