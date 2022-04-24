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

namespace BackEnd.Controllers
{
    [Route("Product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository productRepository;

        public ProductController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }
        [HttpGet]
        public async Task<ActionResult> GetProductsAsync()
        {
            IEnumerable<GetProductDto> productsDto = (await productRepository.GetAllAsync()).ToList().Select(p => p.AsDtoGetProduct());
            if(productsDto.Count() == 0)
            {
                return NotFound();
            }
            
            return Ok(productsDto);
        }
        [HttpGet("Id")]
        public async Task<ActionResult<GetProductDto>> GetProductAsync(Guid Id)
        {
            GetProductDto productDto = (await productRepository.GetIdAsync(Id)).AsDtoGetProduct();
            if(productDto is null)
            {
                return NotFound();
            }
            return productDto;
        }
        [HttpPost]
        public async Task<ActionResult> CreateProductAsync(CreateProductDto productDto)
        {
            Product product = new Product() 
            {
                Name = productDto.Name,
                Price = productDto.Price,
                Describe = productDto.Describe,
                DateTimeCreate = DateTimeOffset.UtcNow,
                Id = Guid.NewGuid(),
                numberOfStars = productDto.numberOfStars
            };

            try
            {
                var files = HttpContext.Request.Form.Files;
                if(files != null && files.Count() > 0) 
                {
                    foreach (var file in files)
                    {
                        FileInfo fi = new FileInfo(file.FileName);
                        var newFileName = "img" + DateTimeOffset.Now.TimeOfDay.Milliseconds + fi.Extension;
                        
                    }
                }
            }
            catch (System.Exception)
            {
                
                throw;
            }


            await productRepository.CreateProductAsync(product);

            return CreatedAtAction(nameof(CreateProductAsync), new {Id = product.Id}, product); 
        }
        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteProductAsync(Guid Id)
        {
            Product product = (await productRepository.GetIdAsync(Id));
            if(product is null)
            {
                return NotFound();
            }
            await productRepository.DeleteProductAsync(product);
            return NoContent();            
        }
        [HttpPut("{Id}")]
        public async Task<ActionResult> UpdateItem(Guid Id, UpdateProductDto productDto)
        {
            Product product = await productRepository.GetIdAsync(Id);
            if(product is null)
            {
                return NotFound();
            }
            Product productUpdate = new(){
                Id = product.Id,
                Name = productDto.Name,
                Price = productDto.Price,
                Describe = productDto.Describe,
                DateTimeCreate = product.DateTimeCreate
            };
            await productRepository.UpdateProductAsync(productUpdate);

            return NoContent();
        }
    }
}