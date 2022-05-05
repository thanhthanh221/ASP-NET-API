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
    [ApiController]
    [Route("Categories")]
    public class CategoriesController : ControllerBase
    {
        private static IWebHostEnvironment _environment;
        private readonly ICategoryProduct categoryProduct;

        public CategoriesController(
            ICategoryProduct categoryProduct,
            IWebHostEnvironment environment)
        {
            _environment = environment;
            this.categoryProduct  = categoryProduct;
            
        }

        
    }
}