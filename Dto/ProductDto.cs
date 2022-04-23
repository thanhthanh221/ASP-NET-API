using System;
using System.ComponentModel.DataAnnotations;
namespace BackEnd.Dto
{
    public record CreateProductDto([Required] string Name, [Required] string Describe, [Required] decimal Price);
    public record ReadProductDto([Required] string Name, [Required] string Describe, [Required] decimal Price, [Required] DateTimeOffset DateTimeCreate);
    public record UpdateProductDto([Required] string Name, [Required] string Describe, [Required] decimal Price);
    public record GetProductDto
    {
        public String Name {get; init;}
        public String Describe {get; init;}
        public decimal Price {get; init;}
    }
}