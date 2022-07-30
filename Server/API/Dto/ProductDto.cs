using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Domain_Layer.Entities.Product;

namespace API.Dto
{
    public record CreateUpdateProductDto(
        [Required] Guid UserId,
        [Required] string Name,
        [Required] string Describe,
        [Required] int numberStart, 
        [Required] decimal Price,
        [Required] List<Guid> CategoryId,
        [Required] IFormFile[] files);
    public class GetProductDto
    {
        public Guid Id {get; set;}
        public Guid UserSellId {get; set;}
        public String Name {get; set;}
        public String Describe {get; set;}
        public decimal Price {get; set;}
        public List<Guid> CategoryId {get; set;}
        public double numberOfStars {get; set;}
        public IList<string> imgAndVideoProducts {get; set;}
    }
}