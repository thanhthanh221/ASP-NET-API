using Domain_Layer.Entities;
using Domain_Layer.Entities.Identity;
using Domain_Layer.Entities.Order;
using Domain_Layer.Entities.Product;
using Domain_Layer.Helpers;
using Domain_Layer.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Infreastructure_Layer.Data
{
    public class UnitOfWork
    {
        public  readonly UserManager<ApplicationUser> userManager;
        public  readonly RoleManager<ApplicationRole> roleManager;
        public  readonly JwtService jwtService;
        public  readonly SignInManager<ApplicationUser> signInManager;
        public  readonly IAsyncRepository<Order> orderRepository;
        public  readonly IAsyncRepository<Product> productRepository;
        public  readonly IAsyncRepository<ImgProductReview> imgProductReview;
        public  readonly IAsyncRepository<Category> categoryProduct;

        public UnitOfWork(UserManager<ApplicationUser> userManager, 
            RoleManager<ApplicationRole> roleManager, 
            JwtService jwtService, 
            SignInManager<ApplicationUser> signInManager, 
            IAsyncRepository<Order> orderRepository, 
            IAsyncRepository<Product> productRepository, 
            IAsyncRepository<ImgProductReview> imgProductReview, 
            IAsyncRepository<Category> categoryProduct)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.jwtService = jwtService;
            this.signInManager = signInManager;
            this.orderRepository = orderRepository;
            this.productRepository = productRepository;
            this.imgProductReview = imgProductReview;
            this.categoryProduct = categoryProduct;
        }
    }
}