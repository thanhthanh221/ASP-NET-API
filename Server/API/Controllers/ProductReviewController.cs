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
using Microsoft.AspNetCore.Identity;
using Domain_Layer.Entities.Product;
using Infreastructure_Layer.Data.Repositories;

namespace BackEnd.Controllers
{
    [ApiController]
    [Route("ProductReview")]
    public class ProductReviewController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private static IWebHostEnvironment _environment;
        private readonly MongoDbRepository<ProductReviews> productsReviews;

        public ProductReviewController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            MongoDbRepository<ProductReviews> productsReviews,
            IWebHostEnvironment environment)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.productsReviews = productsReviews;
            _environment = environment;
        }

        [HttpGet]
        public async Task<IActionResult> GetComment(Guid ProductId)
        {
            ProductReviews commentInProduct =  await productsReviews.GetAsync(ProductId);
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