using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using System;

namespace API.Dto.ProductReviewDtos
{
    public class PostPutProductReviewDto
    {
        [Required]
        public Guid ProductId {set; get;}
        [Required]
        public Guid userId;
        [Required]
        public string comment {set; get;}
        [Required]
        [Range(1,5)] 
        public int numberOfStars {set; get;}
        public  IFormFile[] files {set; get;}
    }
}