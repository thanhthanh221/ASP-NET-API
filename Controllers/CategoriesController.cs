using Microsoft.AspNetCore.Mvc;
using BackEnd.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using BackEnd.Repositories;
using System.Threading.Tasks;
using BackEnd.Dto;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading;
using Microsoft.AspNetCore.Hosting;

namespace BackEnd.Controllers
{
    [ApiController]
    [Route("Categories")]
    public class CategoriesController : ControllerBase
    {
        private static IWebHostEnvironment _environment;
        private readonly ICategoryProduct categoryProduct;

        public CategoriesController(
            ICategoryProduct categoryProduct,
            IWebHostEnvironment environment)
        {
            _environment = environment;
            this.categoryProduct  = categoryProduct;
            
        }
        [HttpGet]
        public async Task<ActionResult> GetCategories()
        {
            IReadOnlyCollection<Category> categories = new List<Category>();
            categories = (await categoryProduct.GetCategorysAsync()).ToList();
            return Ok(categories);
        }
        [HttpGet("Id")]
        public async Task<ActionResult<Category>> GetCategory(Guid Id)
        {
            GetCategoryDto categoryDto = (await categoryProduct.GetCategoryAsync(Id)).AsGetCategory();
            if(categoryDto == null)
            {
                return NotFound();
            }
            return Ok(categoryDto);
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
                products = categoryDto.products,
                imgCategory = await SaveImage(categoryDto.imgCategory)
                
            };
            await categoryProduct.CreateCategoryAsync(categoryProductValue);            
            
            return CreatedAtAction(nameof(PostCategory), new {Id = categoryProductValue.Id});
        }
        [HttpDelete]
        public async Task<ActionResult> DeleteCategory(Guid Id)
        {
            if(Id == null)
            {
                return NotFound();
            }
            Category category = (await categoryProduct.GetCategoryAsync(Id));
            if(category == null)
            {
                return NotFound();
            }
            DeleteImage(category.imgCategory);
            await categoryProduct.DeleteCategoryAsync(category);
            return NoContent();


        }
        // Xử lý nhập file ảnh
        [NonAction]
        public async Task<String> SaveImage(IFormFile file)
        {
            if(file.Length > 0)
            {
                try
                {
                    if(!Directory.Exists(_environment.ContentRootPath+ "\\Images\\" + "\\imgCategory\\"))
                    // Kiểm tra xem đã tồn tại thư mục chưa
                    {
                        Directory.CreateDirectory(_environment.ContentRootPath + "\\Images\\" + "\\imgCategory\\");
                    }
                    using (FileStream fileStream = System.IO.File.Create(_environment.ContentRootPath + "\\Images\\"+ "\\imgCategory\\" + file.FileName))
                    {
                        await file.CopyToAsync(fileStream);
                        await fileStream.FlushAsync(); // giải phóng bộ đệm
                        
                        return file.FileName;
                    }
                }
                catch (Exception ex)
                {
                    return ex.ToString();
                }
            }
            else
            {
                return "Không Up được file";
            }
        }
        [NonAction]  
        public void DeleteImage(String imageName)
        {
            var imagePath = Path.Combine(_environment.ContentRootPath + "\\Images\\" + "\\imgCategory\\" + imageName);
            if(System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }

        }
    }
}