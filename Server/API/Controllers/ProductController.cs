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
using Microsoft.AspNetCore.Authorization;
using Domain_Layer.Entities.Product;
using Domain_Layer.Interfaces;
using Domain_Layer.Services;
using API.Extension;
using API.Extension.Request;

namespace BackEnd.Controllers
{
    [Route("Product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IAsyncRepository<Product> productRepository;
        private readonly IAsyncRepository<ImgAndVideoProduct> imgProductRepository;

        private static int Page_Size {get; set;} = 6;

        public ProductController(
            IAsyncRepository<Product> productRepository,
            IAsyncRepository<ImgAndVideoProduct> imgProductRepository)
        {
            this.productRepository = productRepository;
            this.imgProductRepository = imgProductRepository;
            
        }
        [HttpGet]
        public async Task<ActionResult> GetProductsAsync([FromQuery] FilterRequestProduct filter)
        {
            var imgcheck = ProductLinq.GroupImgProductDto(await imgProductRepository.GetAllAsync()).ToList();
            var productFull = (await productRepository.GetAllAsync()); 

                //     // Tổng bộ tất cả các sản phẩm trả vể
                // var productAndImg = ProductLinq.ProductAndImgDto(imgcheck, productFull)
                //                     .Skip(filter.page* Page_Size).Take(Page_Size);

            if(filter.filerByCategory != null) 
            {
                foreach (var item in filter.filerByCategory)
                {
                    IEnumerable<Product> products = (await productRepository.GetsAsync((p) => 
                                                        p.categories.Contains(item))).ToHashSet();
                    productFull = productFull.Where(p => products.Contains(p)).ToList();
                }     
            }
            if(filter.filerByStar > 0)
            {
                productFull = (productFull.Where(p => p.numberOfStars >= filter.filerByStar)).ToList();
            }                               
                return Ok(
                    new 
                    {
                        page = filter.page,
                        data =  productFull.Skip(filter.page* Page_Size).Take(Page_Size)
                    }
                );           
            
            
        }
        [HttpGet("Id")]
        public async Task<ActionResult<GetProductDto>> GetProductAsync(Guid Id)
        {
            Product product = await productRepository.GetAsync(Id);
            if(product == null)
            {
                   return NotFound(new 
                {
                    message = "Không có sản phẩm"
                });
            }
            GetProductDto productDto = product.ToProductDto();

            productDto.files = ProductLinq.FindImgProduct(await imgProductRepository.GetAllAsync(), Id).Select(p => p.Photo); 
        
            return (productDto is null) ? NotFound() : Ok(productDto);
        }
        [HttpPost]
        public async Task<ActionResult> CreateProductAsync([FromForm] CreateUpdateProductDto productDto)
        {
            Product product = productDto.ToProductEntity();
            await productRepository.CreateAsync(product);

            // Xử lý ảnh
            foreach (var file in productDto.files)
            {
                ImgAndVideoProduct imgProduct = await ProductMapper.ToImgAndVideoProduct(file, product);
                await imgProductRepository.CreateAsync(imgProduct);                 
            }
            return CreatedAtAction(nameof(CreateProductAsync), new {Id = product.Id}, product); 
        }
        [Authorize]
        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteProductAsync(Guid Id)
        {
            Product product = (await productRepository.GetAsync(Id));
            if(product is null)
            {
                return NotFound();
            }
            await productRepository.DeleteAsync(product);

            var imgcheck = ProductLinq.FindImgProduct(await imgProductRepository.GetAllAsync(), Id);
            
            if(imgcheck.Count() != 0)
            {
                foreach (var item in imgcheck)
                {
                    UpLoadFileService.DeleteImage(item.Photo, "ImgProduct"); 
                    await imgProductRepository.DeleteAsync(item);            
                }
            }
            return NoContent();            
        }
        [HttpPut("{Id}")]
        [Authorize]
        public async Task<ActionResult> UpdateItem(Guid Id , [FromForm] CreateUpdateProductDto productDto)
        {
            Product product = await productRepository.GetAsync(Id);
            if(product is null)
            {
                return NotFound();
            }
            Product productUpdate = productDto.ToProductEntity();
            await productRepository.UpdateAsync(productUpdate);

            return NoContent();
        }
    }
}