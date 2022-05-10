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
using BackEnd.Services;
using Microsoft.AspNetCore.Authorization;

namespace BackEnd.Controllers
{
    [Authorize]
    [Route("Product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private static IWebHostEnvironment _environment;
        private readonly IProductRepository productRepository;
        private readonly IImgProduct imgProductRepository;

        private static int Page_Size {get; set;} = 5;

        public ProductController(IProductRepository productRepository,
            IWebHostEnvironment environment,
            IImgProduct imgProductRepository)
        {
            this.productRepository = productRepository;
            _environment = environment;
            this.imgProductRepository = imgProductRepository;
            
        }
        [HttpGet]
        public async Task<ActionResult> GetProductsAsync(int page)
        {
            // Trả về sản phẩm
            IEnumerable<GetProductDto> productsDto = (await productRepository.GetAllAsync()).ToList().Select(p => p.AsDtoGetProduct());

            var imgcheck = from a in await imgProductRepository.GetImgProductsAsync()
                            orderby a.Id
                            group a by a.ProductId;

            List<IGrouping<Guid, ImgProduct>> hash = new List<IGrouping<Guid, ImgProduct>>();

            // Tổng bộ tất cả các sản phẩm trả vể
            var productAndImg = (from product in await productRepository.GetAllAsync()
                                join img in imgcheck on product.Id equals img.Key into t
                                from img in t.DefaultIfEmpty()
                                orderby product.Name                      
                                select new GetProductDto {
                                    Name = product.Name,
                                    Price = product.Price,
                                    Describe = product.Describe,
                                    numberOfStars = product.numberOfStars,
                                    files = (img == null) ? null : img.Select(p => p.Photo)
                                })
                                .Skip((page - 1)* Page_Size).Take(Page_Size);

            var productAndImgPage = new {
                page = page,
                data =  productAndImg
            };
                                
            if(productAndImg.Count() == 0)
            {
                return NotFound(new 
                    {
                    message = "Không có sản phẩm nào"
                });
            }
            
            return Ok(productAndImgPage);
        }
        [HttpGet("Id")]
        public async Task<ActionResult<GetProductDto>> GetProductAsync(Guid Id)
        {
            Product product = await productRepository.GetIdAsync(Id);
            if(product == null)
            {
                return NotFound(new 
                {
                    message = "Không có sản phẩm"
                });
            }
            GetProductDto productDto = product.AsDtoGetProduct();

            productDto.files =  from a in await imgProductRepository.GetImgProductsAsync()
                                where a.ProductId == Id
                                select a.Photo ; 
            if(productDto.files.Count() == 0)
            {
                productDto.files = null;
            }
                                       
            if(productDto is null)
            {
                return NotFound();
            }
            return productDto;
        }
        [HttpPost]
        public async Task<ActionResult> CreateProductAsync([FromForm] CreateProductDto productDto)
        {
            Product product = new Product() 
            {
                Name = productDto.Name,
                Price = productDto.Price,
                Describe = productDto.Describe,
                DateTimeCreate = DateTimeOffset.UtcNow,
                Id = Guid.NewGuid()
            };

            Boolean createProduct = await productRepository.CreateProductAsync(product);
            // Xử lý ảnh
            if(productDto.files.Length != 0)
            {
                foreach (var file in productDto.files)
                {
                    ImgProduct imgProduct = new()
                    {
                        Id = Guid.NewGuid(),
                        Photo = await UpLoadFileService.SaveImage(file, "ImgProduct"),
                        ProductId = product.Id
                    };
                    await imgProductRepository.CreateImgProductAsync(imgProduct);
                                
                }
            }
            
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

            var imgcheck = from a in await imgProductRepository.GetImgProductsAsync()
                            where a.ProductId == Id
                            select a;

            foreach (var item in imgcheck)
            {
                UpLoadFileService.DeleteImage(item.Photo, "ImgProduct"); 
                await imgProductRepository.DeleteImgProductAsync(item);            
            }

            return NoContent();            
        }
        [HttpPut("{Id}")]
        public async Task<ActionResult> UpdateItem(Guid Id,[FromForm] UpdateProductDto productDto)
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