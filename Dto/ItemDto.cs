using System;

namespace BackEnd.Dto
{
    public record ItemDto // Biến dữ liệu 
    {
        public Guid Id {get; init;}
        public string Name {get; init;}
        public decimal Price {get; init;}
        public DateTimeOffset CreateDate{get; set;}
         
    }
}