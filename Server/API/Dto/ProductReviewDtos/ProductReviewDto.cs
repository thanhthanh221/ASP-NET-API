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
        public Guid userCommentId {set; get;}
        [Required]
        public string comment {set; get;}
        [Range(1,5)] 
        public int numberOfStars {set; get;}
        public  IFormFile[] files {set; get;}
    }
}