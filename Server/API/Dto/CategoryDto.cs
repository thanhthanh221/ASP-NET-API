using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace BackEnd.Dto
{
    public record CreateCategoryDto(
        [Required] String name, 
        IEnumerable<Guid> parentsCategoryId,
        IEnumerable<Guid> subCategoryId, 
        [Required] IFormFile imgCategory, 
        List<Guid> products
    );
    public record GetCategoryDto
    {
        [Required]
        public String name {get; init;}
        public int sumProduct {get; init;}
        [Required]
        public string imgCategory {get; set;}
        public IEnumerable<Guid> parentsCategoryId {get; init;}
        public IEnumerable<Guid> subCategoryId {get; init;}
    }
}