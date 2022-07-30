using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace API.Dto
{
    public record CreateCategoryDto(
        [Required] String name, 
        IEnumerable<Guid> parentsCategoryId,
        IEnumerable<Guid> subCategoryId, 
        [Required] IFormFile imgCategory
    );
    public record GetCategoryDto
    {
        [Required]
        public Guid Id {get; set;}
        [Required]
        public String name {get; init;}
        [Required]
        public string imgCategory {get; set;}
        public IEnumerable<Guid> parentsCategoryId {get; init;}
        public IEnumerable<Guid> subCategoryId {get; init;}
    }
}