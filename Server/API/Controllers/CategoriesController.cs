using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain_Layer.Entities;
using Domain_Layer.Interfaces;
using Domain_Layer.Services;
using API.Extension.Mapper;
using API.Dto;

namespace API.Controllers
{
    [ApiController]
    [Route("Categories")]
    public class CategoriesController : ControllerBase
    {
        private readonly IAsyncRepository<Category> categoryRepository;

        public CategoriesController(IAsyncRepository<Category> categoryRepository)
        {
            this.categoryRepository  = categoryRepository;
            
        }
        [HttpGet]
        public async Task<ActionResult> GetCategories()
        {
            return Ok((await categoryRepository.GetAllAsync()).Select(p => p.ToCategoryDto()));
        }
        [HttpGet("Id")]
        public async Task<ActionResult<Category>> GetCategory(Guid Id)
        {
            Category category = (await categoryRepository.GetAsync(Id));
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
            Category categoryRepositoryValue = await categoryDto.ToCategoryEntity();
            await categoryRepository.CreateAsync(categoryRepositoryValue);            
            
            return CreatedAtAction(nameof(PostCategory), new {Id = categoryRepositoryValue.Id});
        }
        [HttpPut]
        public async Task<ActionResult> UpdateCategoryAsync([FromForm] CreateCategoryDto categoryDto, Guid CategoryId)
        {
            Category category = await categoryRepository.GetAsync(CategoryId);
            if(category is null)
            {
                return NotFound(
                    new {
                        message = "Không tìm thấy Danh mục đã yêu cầu"
                    }
                );
            }
            Category newCategory = new Category(CategoryId,
                                        category.name, 
                                        category.imgCategory, 
                                        category.parentsCategoryId, 
                                        category.subCategoryId);
            await categoryRepository.UpdateAsync(newCategory);
            
            return Ok(newCategory);

        }
        [HttpDelete("Id")]
        public async Task<ActionResult> DeleteCategory(Guid Id)
        {
            Category category = (await categoryRepository.GetAsync(Id));
            if(category == null)
            {
                return NotFound();
            }
            UpLoadFileService.DeleteImage(category.imgCategory, "ImgCategory");
            await categoryRepository.DeleteAsync(category);
            return NoContent();
        }
    }
}