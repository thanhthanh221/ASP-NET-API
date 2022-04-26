using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;

namespace BackEnd.Dto
{
    public record CreateProductDto([Required] string Name, [Required] string Describe, [Required] decimal Price, [Range(1,5)] double numberOfStars,[Required] IFormFile[] files);
    public record ReadProductDto([Required] string Name, [Required] string Describe, [Required] decimal Price, [Required] DateTimeOffset DateTimeCreate, [Range(1,5)] double numberOfStars);
    public record UpdateProductDto([Required] string Name, [Required] string Describe, [Required] decimal Price, [Range(1,5)] double numberOfStars);
    public record GetProductDto
    {
        public String Name {get; init;}
        public String Describe {get; init;}
        public decimal Price {get; init;}
        public double numberOfStars {get; init;}
    }
}