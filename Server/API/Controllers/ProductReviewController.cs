using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Dto;
using Microsoft.AspNetCore.Http;
using System.Threading;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Domain_Layer.Entities.Product;
using Domain_Layer.Entities.Identity;
using Domain_Layer.Interfaces;

namespace BackEnd.Controllers
{
    [ApiController]
    [Route("ProductReview")]
    public class ProductReviewController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IAsyncRepository<ProductReviews> productsReviews;
        private readonly IAsyncRepository<ImgProductReview> imgProductReview;
        private const int Page_Size = 5;
        public ProductReviewController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IAsyncRepository<ProductReviews> productsReviews,
            IAsyncRepository<ImgProductReview> imgProductReview)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.productsReviews = productsReviews;
            this.imgProductReview = imgProductReview;
        }
        [HttpGet]
        public async Task<IActionResult> GetCommentsProduct(Guid ProductId, int page)
        {
            IReadOnlyCollection<ProductReviews> CommentRespones =  await productsReviews.GetsAsync((ProductReviews p) => p.ProductId.Equals(ProductId));
            var ImgAndVideoComment = from a in await imgProductReview.GetAllAsync() 
                                    group a by a.CommentId;

            var ApiRespone = (from pv in CommentRespones.ToList()
                            join v in ImgAndVideoComment on pv.Id equals v.Key into t
                            from v in t.DefaultIfEmpty()
                            orderby pv.Comment
                            select new {
                                userComment = pv.userId,
                                Comment = pv.Comment,
                                Start = pv.numberOfStars,
                                file = (v == null) ? null : v.Select(h => h.Photo) 
                            }).
                            Skip((page - 1)* Page_Size).Take(Page_Size);
            if(ApiRespone == null)
            {
                return NotFound();
            }
            return Ok(new {
                Api = ApiRespone,
                page = page
            });
        }

        [HttpGet("Id")]
        public async Task<IActionResult> GetCommentAsync(Guid ProductId)
        {
            ProductReviews commentInProduct = await productsReviews.GetAsync(ProductId);
            return Ok();
        }
        [HttpPost]
        public async Task<IActionResult> PostCommentAsync([FromForm]CreateProductReviewDto createProductReviewDto)
        {
            ProductReviews productsReviewsNew = new () {
                Id = Guid.NewGuid(),
                ProductId = createProductReviewDto.ProductId,   
                numberOfStars = createProductReviewDto.numberOfStars,
                Comment = createProductReviewDto.comment,
                dateTimeCreate = DateTimeOffset.Now
            };
            await productsReviews.CreateAsync(productsReviewsNew);
            return Ok();
        }
    }
}