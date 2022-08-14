using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Domain_Layer.Entities.Product;
using Domain_Layer.Interfaces;
using Domain_Layer.Services;
using Domain_Layer.Entities.Identity;
using API.Extension.Request;
using API.Extension.Build.ProductDtoBuider;
using API.Dto;
using API.Extension.Mapper;

namespace API.Controllers
{
    [Route("Product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IAsyncRepository<Product> productRepository;
        private readonly IAsyncRepository<ProductReviews> productReviewsRepository;
        private readonly UserManager<ApplicationUser> userManager;
        private static int Page_Size {get; set;} = 6;
        public ProductController(IAsyncRepository<Product> productRepository,
                                UserManager<ApplicationUser> userManager,
                                IAsyncRepository<ProductReviews> productReviewsRepository)
        {
            this.userManager = userManager;
            this.productRepository = productRepository;
            this.productReviewsRepository = productReviewsRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetProductsAsync([FromQuery] FilterRequestProduct filter)
        {
            IEnumerable<Product> productFull = (await productRepository.GetAllAsync());
            List<GetProductDto> productDtos = new List<GetProductDto>();                                            
            foreach (var product in productFull)
            { 
                productDtos.Add(new GetProductDtoBuilder()
                                    .AddId(product.Id)
                                    .AddCategoryId(product.categories)
                                    .AddDescribe(product.Describe)
                                    .AddUserSellId(product.UserSellId)
                                    .AddImgAndVideoProducts(product.ImgAndVideoProducts)
                                    .AddNumberOfStars(Math.Round(product.numberOfStars,1))
                                    .AddName(product.Name)
                                    .AddPrice(product.Price)
                                    .Build());    
            }

            if(filter.filerByCategory != null) 
            {
                foreach (var item in filter.filerByCategory)
                {
                    IEnumerable<Product> products = (await productRepository.GetsAsync((p) => 
                                                        p.categories.Contains(item))).ToList();
                    productFull = productFull.Where(p => products.Contains(p)).ToList();
                }     
            }
            if(filter.filerByStar > 0)
            {
                productFull = (productFull.Where(p => p.numberOfStars > filter.filerByStar - 1)).ToList();
            }                               
                return Ok(
                    new 
                    {
                        page = filter.page,
                        data = productFull.Skip(filter.page* Page_Size).Take(Page_Size)
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

            // productDto.files = ProductLinq.FindImgProdu
            // ct(await imgProductRepository.GetAllAsync(), Id).Select(p => p.Photo); 
        
            return (productDto is null) ? NotFound() : Ok(product);
        }
        [HttpPost]
        public async Task<ActionResult> CreateProductAsync([FromForm] CreateUpdateProductDto productDto)
        {
            ApplicationUser user = await userManager.FindByIdAsync(productDto.UserId.ToString());
            if(user is null) 
            {
                return NotFound(
                    new {
                        message = "Không tìm thấy thông tin người dùng"
                    }
                );
            }
            // Xử lý ảnh
            List<string> imgAndVideoProducts = new List<string>();
            foreach (var file in productDto.files)
            {
                string photo = await UpLoadFileService.SaveImage(file, "ImgProduct");  
                imgAndVideoProducts.Add(photo);        
            }
            Product product = new GetProductBuilder()
                            .AddId(Guid.NewGuid())
                            .AddDateCreate(DateTimeOffset.Now)
                            .AddCategiries(productDto.CategoryId)
                            .AddPrice(productDto.Price)
                            .AddDescribe(productDto.Describe)
                            .AddStarProduct(productDto.numberStart)
                            .AddUserSellId(productDto.UserId)
                            .AddNameProduct(productDto.Name)
                            .AddFileProduct(imgAndVideoProducts)
                            .Build();
            await productRepository.CreateAsync(product);
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
            
            foreach (var item in product.ImgAndVideoProducts)
            {
                UpLoadFileService.DeleteImage(item, "ImgProduct");            
            }
            return NoContent();            
        }
        [HttpPut("{Id}")]
        [Authorize]
        public async Task<ActionResult> UpdateItem(Guid Id , [FromForm] CreateUpdateProductDto productDto)
        {
            Product product = await productRepository.GetAsync(Id);
            ApplicationUser user = await userManager.FindByIdAsync(productDto.UserId.ToString());
            if(product is null || user is null)
            {
                return NotFound();
            }
            List<string> imgAndVideoProducts = new List<string>();
            foreach (var file in productDto.files)
            {
                string photo = await UpLoadFileService.SaveImage(file, "ImgProduct");  
                imgAndVideoProducts.Add(photo);        
            }
            Product productUpdate = new GetProductBuilder()
                            .AddId(Id)
                            .AddDateCreate(DateTimeOffset.Now)
                            .AddCategiries(productDto.CategoryId)
                            .AddPrice(productDto.Price)
                            .AddDescribe(productDto.Describe)
                            .AddStarProduct(productDto.numberStart)
                            .AddUserSellId(productDto.UserId)
                            .AddNameProduct(productDto.Name)
                            .AddFileProduct(imgAndVideoProducts)
                            .Build();
            await productRepository.UpdateAsync(productUpdate);

            return NoContent();
        }
    }
}