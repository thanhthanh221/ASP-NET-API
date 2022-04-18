using System;
using System.ComponentModel.DataAnnotations;
namespace BackEnd.Dto
{
    public record CreateItemDto([Required]string Name,[Required] [Range(1,100)] decimal Price);
    public record DeleteItem(Guid Id);
    public record UpdateItem([Required]string Name,[Required] [Range(1,100)] decimal Price);
    public record ItemDto // Biến dữ liệu 
    {
        public Guid Id {get; init;}
        public string Name {get; init;}
        public decimal Price {get; init;}
        public DateTimeOffset CreateDate{get; set;}
         
    }
}