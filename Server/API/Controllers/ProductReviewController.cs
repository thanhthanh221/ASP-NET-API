using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Threading;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Domain_Layer.Entities.Product;
using Domain_Layer.Entities.Identity;
using Domain_Layer.Interfaces;
using Domain_Layer.Services;
using API.Dto.ProductReviewDtos;
using API.Extension.Mapper;

namespace API.Controllers
{
    [ApiController]
    [Route("ProductReview")]
    public class ProductReviewController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IAsyncRepository<ProductReviews> productReviewsRepository;
        private readonly IAsyncRepository<Product> productRepository;
        private const int Page_Size = 6;

        public ProductReviewController(
                        UserManager<ApplicationUser> userManager, 
                        SignInManager<ApplicationUser> signInManager, 
                        IAsyncRepository<ProductReviews> productReviewsRepository, 
                        IAsyncRepository<Product> productRepository)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.productReviewsRepository = productReviewsRepository;
            this.productRepository = productRepository;
        }
        [HttpGet("Id")]
        public async Task<IActionResult> GetCommentProduct(Guid Id)
        {
            ProductReviews productReview = await productReviewsRepository.GetAsync(Id);
            if(productReview is null)
            {
                return NotFound(
                    new {
                        message = "Không tìm thấy bình luận"
                    }
                );
            }
            GetProductReviewDto getProductReviewDto = productReview.ConverToDto();
            return Ok(getProductReviewDto);
        }
        [HttpGet("ProductId")]
        public async Task<ActionResult> GetCommentProductAsync(int page, Guid ProductId)
        {
            Product product = await productRepository.GetAsync(ProductId);
            if(product is null)
            {
                return NotFound(
                    new {
                        message = "Không tìm thấy sản phẩm"
                    }
                );
            }
            List<GetProductReviewDto>productReviewDtos = new List<GetProductReviewDto>();

            List<ProductReviews> productReviews = 
                    (await productReviewsRepository
                        .GetsAsync((proRiviews) => proRiviews.ProductId.Equals(ProductId)))
                        .OrderByDescending(proRiviews => proRiviews.dateTimeCreate)
                        .Skip(page*Page_Size).Take(Page_Size).ToList();
            if(productReviews.Count != 0)
            {
                foreach (var item in productReviews)
                {
                    ApplicationUser user = await userManager.FindByIdAsync(item.userId.ToString());
                    GetProductReviewDto productReviewDto = new GetProductReviewDto() {
                        Id = item.Id,
                        userId = item.userId,
                        ProductId = item.ProductId,
                        Comment = item.Comment,
                        numberOfStars = item.numberOfStars,
                        dateTimeCreate = item.dateTimeCreate,
                        Photo = item.Photo != null ? item.Photo : null,
                        userName = user != null ? user.UserName : null
                    };
                    productReviewDtos.Add(productReviewDto);
                }
            }
                 
            return Ok(productReviewDtos);

        }
        [HttpPost]
        public async Task<IActionResult> PostCommentAsync( [FromForm] PostPutProductReviewDto postPutProductReviewDto)
        {
            List<string> photosAndVideos = new List<string>();
            if(postPutProductReviewDto.files != null)
            {
                foreach (var item in postPutProductReviewDto.files)
                {
                    string photoToString = await UpLoadFileService.SaveImage(item,  "ImgProductReview");
                    photosAndVideos.Add(photoToString);
                    
                }
            }
            ApplicationUser user = await userManager.FindByIdAsync(postPutProductReviewDto.userCommentId.ToString());
            Product product = await productRepository.GetAsync(postPutProductReviewDto.ProductId);
            if(user is null || product is null) 
            {
                return NotFound(
                    new {
                        message = "Không tìm thấy sản phẩm hoặc người dùng"
                    }
                );
            }
            
            ProductReviews productReviews = new ProductReviews() {
                Id = Guid.NewGuid(),
                userId = postPutProductReviewDto.userCommentId,
                ProductId = postPutProductReviewDto.ProductId,
                numberOfStars = postPutProductReviewDto.numberOfStars,
                dateTimeCreate = DateTimeOffset.UtcNow,
                Comment = postPutProductReviewDto.comment,
                Photo = photosAndVideos
            };
            await productReviewsRepository.CreateAsync(productReviews);

            List<ProductReviews> productReviewsOfProduct = (await productReviewsRepository.GetsAsync(
                    (productRiviews) => 
                        productRiviews.ProductId.Equals(product.Id) && 
                        productRiviews.numberOfStars != 0))
                    .ToList();

            if(productReviewsOfProduct.Count() != 0)
            {
                double countStartOfProduct = Convert.ToDouble(productReviewsOfProduct.
                    Sum((productReviews) => productReviews.numberOfStars))/productReviewsOfProduct.Count();

                Product ProductNew = product;
                ProductNew.numberOfStars = countStartOfProduct;
                await productRepository.UpdateAsync(ProductNew);  
            }
            
            return CreatedAtAction(nameof(PostCommentAsync),productReviews);
        }
        [HttpPatch("UpdatenumberOfStarsComments")]
        public async Task<ActionResult> PatchNumberStarOfCommentAsync(Guid CommentsId,[FromForm] int Stars)
        {
            ProductReviews productReviews = await productReviewsRepository.GetAsync(CommentsId);
            Product product = await productRepository.GetAsync(productReviews.ProductId);
            if(productReviews is null || product is null)
            {
                return NotFound(
                    new {
                        message = "Không có bình luận trên"
                    }
                );
            }
            ProductReviews newProductReview = productReviews;
            newProductReview.numberOfStars = Stars;
            await productReviewsRepository.UpdateAsync(newProductReview);

            List<ProductReviews> productReviewsOfProduct = (await productReviewsRepository.GetsAsync(
                    (productRiviews) => 
                        productRiviews.ProductId.Equals(product.Id) && 
                        productRiviews.numberOfStars != 0))
                    .ToList();

            if(productReviewsOfProduct.Count() != 0)
            {
                double countStartOfProduct = Convert.ToDouble(productReviewsOfProduct.
                    Sum((productReviews) => productReviews.numberOfStars))/productReviewsOfProduct.Count();

                Product ProductNew = product;
                ProductNew.numberOfStars = countStartOfProduct;
                await productRepository.UpdateAsync(ProductNew);  
            }
            return NoContent();
        }
        [HttpDelete("Id")]
        public async Task<IActionResult> DeleteCommentAsync(Guid CommentId)
        {
            ProductReviews productReviews = await productReviewsRepository.GetAsync(CommentId);
            Product product = await productRepository.GetAsync(productReviews.ProductId);
            if(productReviewsRepository is null) 
            {
                return NotFound(
                    new {
                        message = "Không tìm thấy bình luận"
                    }
                );
            }
            await productReviewsRepository.DeleteAsync(productReviews);

            if(productReviews.Photo != null)
            {
                foreach (var imageOfVideo in productReviews.Photo)
                {
                    UpLoadFileService.DeleteImage(imageOfVideo, "ImgProductReview");   
                }
            }

            List<ProductReviews> productReviewsOfProduct = (await productReviewsRepository.GetsAsync(
                    (productRiviews) => 
                        productRiviews.ProductId.Equals(product.Id) && 
                        productRiviews.numberOfStars != 0))
                    .ToList();

            if(productReviewsOfProduct.Count() != 0)
            {
                double countStartOfProduct = Convert.ToDouble(productReviewsOfProduct.
                    Sum((productReviews) => productReviews.numberOfStars))/productReviewsOfProduct.Count();

                Product ProductNew = product;
                ProductNew.numberOfStars = countStartOfProduct;
                await productRepository.UpdateAsync(ProductNew);  
            }

            return NoContent();
        }

    }
}