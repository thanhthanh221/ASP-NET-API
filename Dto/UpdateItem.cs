using System.ComponentModel.DataAnnotations;

namespace BackEnd.Dto{
    public record UpdateItem{
        // Chỉ cần nhập 2 trường này thôi
        [Required]
        public string Name {get; init;}
        [Required]
        [Range(1,1000)]
        public decimal Price {get; init;}
    }
}