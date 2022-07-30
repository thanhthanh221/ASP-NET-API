using System.ComponentModel.DataAnnotations;

namespace API.Dto.OrderDtos
{
    public class DeliveryInformationDto
    {
        [Required]
        public string Status {get; set;}
        
    }
}