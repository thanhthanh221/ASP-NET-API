using System;
using System.ComponentModel.DataAnnotations;

namespace API.Dto.OrderDtos
{
    public class GetAddressDto
    {
        [Required]
        public String City {get; set;}
        [Required]
        public String District {get; set;}
        [Required]
        public String Commune {get; set;}
        [Required]
        public String Street {get; set;}
    }
}