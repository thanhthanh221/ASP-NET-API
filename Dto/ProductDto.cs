using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;

namespace BackEnd.Dto
{
    public record CreateProductDto([Required] string Name, [Required] string Describe, [Required] decimal Price, IFormFile[] files);
    public record ReadProductDto([Required] string Name, [Required] string Describe, [Required] decimal Price, [Required] DateTimeOffset DateTimeCreate, [Range(1,5)] double numberOfStars);
    public record UpdateProductDto([Required] string Name, [Required] string Describe, [Required] decimal Price, [Range(1,5)] double numberOfStars, IFormFile[] files);
    public class UpdateProductDto1
    {
        public UpdateProductDto1(string name, string describe, decimal price, double numberOfStars, IFormFile[] files)
        {
            Name = name;
            Describe = describe;
            Price = price;
            this.numberOfStars = numberOfStars;
            this.files = files;
        }

        [Required]
        public string Name {get; set;}
        [Required]
        public string Describe {get; set;}
        [Required]
        public decimal Price {get; set;}
        [Required]
        public double numberOfStars {get; set;}
        public IFormFile[] files {get; set;}

    }
    public record GetProductDto
    {
        public String Name {get; init;}
        public String Describe {get; init;}
        public decimal Price {get; init;}
        public double numberOfStars {get; init;}
        public IEnumerable<string> files {get; set;}
    }
}