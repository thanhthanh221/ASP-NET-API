using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using System;

namespace BackEnd.Dto
{
    public record CreateProductReviewDto(string comment, [Required] [Range(1,5)] int numberOfStars, IFormFile[] files, Guid ProductId, Guid userId);

}