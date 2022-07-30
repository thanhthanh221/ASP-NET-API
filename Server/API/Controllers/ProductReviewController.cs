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

        // [HttpGet("ProductId")]
        // public async Task<IActionResult> GetCommentsProduct(Guid ProductId, int page)
        // {
        //     IReadOnlyCollection<ProductReviews> CommentRespones =  await productsReviews.GetsAsync((ProductReviews p) => p.ProductId.Equals(ProductId));
        //     // var ImgAndVideoComment = from a in await imgProductReview.GetAllAsync() 
        //     //                         group a by a.CommentId;

        //     // var ApiRespone = (from pv in CommentRespones.ToList()
        //     //                 join v in ImgAndVideoComment on pv.Id equals v.Key into t
        //     //                 from v in t.DefaultIfEmpty()
        //     //                 orderby pv.Comment
        //     //                 select new {
        //     //                     userComment = pv.userId,
        //     //                     Comment = pv.Comment,
        //     //                     Start = pv.numberOfStars,
        //     //                     file = (v == null) ? null : v.Select(h => h.Photo) 
        //     //                 }).
        //     //                 Skip((page - 1)* Page_Size).Take(Page_Size);
        //     // if(ApiRespone == null)
        //     // {
        //     //     return NotFound();
        //     // }
        //     // return Ok(new {
        //     //     Api = ApiRespone,
        //     //     page = page
        //     // });
        //     return Ok();
        // }
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
        // [HttpPost]
        // public async Task<IActionResult> PostCommentAsync( [FromForm] PostPutProductReviewDto postPutProductReviewDto)
        // {
        //     // ProductReviews prv = new () {
        //     //     Id = Guid.NewGuid(),
        //     //     ProductId = postPutProductReviewDto.ProductId,
        //     //     userId = postPutProductReviewDto.userId,   
        //     //     numberOfStars = postPutProductReviewDto.numberOfStars,
        //     //     Comment = postPutProductReviewDto.comment,
        //     //     dateTimeCreate = DateTimeOffset.Now
        //     // };
        //     // await productsReviews.CreateAsync(prv);

        //     // if(postPutProductReviewDto.files.Length != 0)
        //     // {
        //     //     foreach (var file in postPutProductReviewDto.files)
        //     //     {
        //     //         ImgProductReview imgPrv = new()
        //     //         {
        //     //             Id = Guid.NewGuid(),
        //     //             Photo = await UpLoadFileService.SaveImage(file, "ImgProductReview"),
        //     //             CommentId = prv.Id,
                        
        //     //         };
        //     //         await imgProductReview.CreateAsync(imgPrv);                         
        //     //     }
        //     // }       
        //     // return CreatedAtAction(nameof(PostCommentAsync), new {Id = prv.Id}, prv);
        //     return CreatedAtAction(nameof(PostCommentAsync));
        // }
        // [HttpPut("Id")]
        // public async Task<IActionResult> PutCommentAsync (Guid CommentId,[FromForm] PostPutProductReviewDto postPutProductReviewDto)
        // {
        //     ProductReviews comment = await productsReviews.GetAsync(CommentId);
        //     if(comment is null)
        //     {
        //         return NotFound();
        //     }
        //     ProductReviews prv = new ProductReviews()
        //     {
        //         Id = CommentId,
        //         ProductId = comment.ProductId,
        //         Comment = postPutProductReviewDto.comment,
        //         numberOfStars = postPutProductReviewDto.numberOfStars,
        //         dateTimeCreate = comment.dateTimeCreate
        //     };
        //     IReadOnlyCollection<ImgProductReview> imgAndVideoRv = await imgProductReview.
        //                         GetsAsync((ImgProductReview i) => i.CommentId.Equals(CommentId));
        //     if(imgAndVideoRv.Count() != 0)
        //     {
        //         foreach (ImgProductReview item in imgAndVideoRv)
        //         {
        //             await imgProductReview.DeleteAsync(item);
        //             UpLoadFileService.DeleteImage(item.Photo, "ImgProductReview");                
        //         };
        //     };
        //     await productsReviews.UpdateAsync(prv);
        //     return NoContent();
        // }
        // [HttpDelete("Id")]
        // public async Task<IActionResult> DeleteCommentAsync(Guid CommentId)
        // {
        //     ProductReviews prv = await productsReviews.GetAsync(CommentId);

        //     if(prv is null)
        //     {
        //         return NotFound();
        //     }
        //     IReadOnlyCollection<ImgProductReview> imgAndVideoRv = await imgProductReview.
        //                         GetsAsync((ImgProductReview i) => i.CommentId.Equals(CommentId));

        //     if(imgAndVideoRv.Count() != 0)
        //     {
        //         foreach (ImgProductReview item in imgAndVideoRv)
        //         {
        //             await imgProductReview.DeleteAsync(item);
        //             UpLoadFileService.DeleteImage(item.Photo, "ImgProductReview");                
        //         };
        //     };

        //     return NoContent();
        // }

    }
}