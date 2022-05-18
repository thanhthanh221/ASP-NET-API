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
using Microsoft.AspNetCore.Identity;

namespace BackEnd.Controllers
{
    [ApiController]
    [Route("ProductReview")]
    public class ProductReviewController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private static IWebHostEnvironment _environment;
        private readonly IProductsReviews productsReviews;

        public ProductReviewController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
                                         IProductsReviews productsReviews, IWebHostEnvironment environment)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.productsReviews = productsReviews;
            _environment = environment;
        }

        [HttpGet]
        public async Task<IActionResult> GetComment(Guid ProductId)
        {
            IEnumerable<ProductsReviews> commentInProduct =  await productsReviews.
                                        GetProductsReviewsAsync(ProductId);
            if(commentInProduct.Count() == 0)
            {
                return Ok(new {
                    message = "Không tìm thấy Bình Luận"
                });

            }
            return Ok();
        }
        // [HttpPost]
        // [Authorize]
        // public async Task<IActionResult> PostCommentAsync(CreateProductReviewDto createProductReviewDto)
        // {
        //     ProductsReviews productsReviews = new () {
        //         Id = Guid.NewGuid(),
        //         ProductId = createProductReviewDto.ProductId

        //     };

        // }
    }
}