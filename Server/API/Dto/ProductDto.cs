using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;

namespace BackEnd.Dto
{
    public record CreateUpdateProductDto(
        [Required] Guid UserId,
        [Required] string Name,
        [Required] string Describe,
        [Required] int numberStart, 
        [Required] decimal Price, 
        [Required] IFormFile[] files);
    public record GetProductDto
    {
        public Guid UserSellId {get; set;}
        public String Name {get; init;}
        public String Describe {get; init;}
        public decimal Price {get; init;}
        public double numberOfStars {get; init;}
        public IEnumerable<string> files {get; set;}
    }
}