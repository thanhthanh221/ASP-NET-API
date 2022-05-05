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
    [Route("Product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private static IWebHostEnvironment _environment;
        private readonly IProductRepository productRepository;
        private readonly IImgProduct imgProductRepository;

        public ProductController(IProductRepository productRepository,
            IWebHostEnvironment environment,
            IImgProduct imgProductRepository)
        {
            this.productRepository = productRepository;
            _environment = environment;
            this.imgProductRepository = imgProductRepository;
            
        }
        [HttpGet]
        public async Task<ActionResult> GetProductsAsync()
        {
            // Trả về sản phẩm
            IEnumerable<GetProductDto> productsDto = (await productRepository.GetAllAsync()).ToList().Select(p => p.AsDtoGetProduct());

            var imgcheck = from a in await imgProductRepository.GetImgProductsAsync()
                            orderby a.Id
                            group a by a.ProductId;

            List<IGrouping<Guid, ImgProduct>> hash = new List<IGrouping<Guid, ImgProduct>>();

            var productAndImg = from product in await productRepository.GetAllAsync()
                                join img in imgcheck on product.Id equals img.Key into t
                                from img in t.DefaultIfEmpty()
                                orderby product.Name                      
                                select new GetProductDto {
                                    Name = product.Name,
                                    Price = product.Price,
                                    Describe = product.Describe,
                                    numberOfStars = product.numberOfStars,
                                    files = (img == null) ? null : img.Select(p => p.Photo)
                                };
                                
            if(productsDto.Count() == 0)
            {
                return NotFound();
            }
            
            return Ok(productAndImg);
        }
        [HttpGet("Id")]
        public async Task<ActionResult<GetProductDto>> GetProductAsync(Guid Id)
        {
            GetProductDto productDto = (await productRepository.GetIdAsync(Id)).AsDtoGetProduct();

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
                        Photo = await SaveImage(file),
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
        // Xử lý nhập file ảnh
        [NonAction]
        public async Task<string> SaveImage(IFormFile file)
        {
            if(file.Length > 0)
            {
                try
                {
                    if(!Directory.Exists(_environment.ContentRootPath+ "\\Images\\"))
                    // Kiểm tra xem đã tồn tại thư mục chưa
                    {
                        Directory.CreateDirectory(_environment.ContentRootPath + "\\Images\\");
                    }
                    using (FileStream fileStream = System.IO.File.Create(_environment.ContentRootPath + "\\Images\\"+file.FileName))
                    {
                        await file.CopyToAsync(fileStream);
                        await fileStream.FlushAsync(); // giải phóng bộ đệm
                        
                        return "\\Images\\" + file.FileName;
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
    }
}