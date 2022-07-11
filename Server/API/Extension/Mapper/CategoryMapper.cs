using System;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Dto;
using Domain_Layer.Entities;
using Domain_Layer.Services;

namespace API.Extension.Mapper
{
    public static class CategoryMapper
    {
        public static GetCategoryDto ToCategoryDto(this Category category)
        {
            return new GetCategoryDto
            {
                name = category.name,
                parentsCategoryId = category.parentsCategoryId,
                subCategoryId = category.subCategoryId,
                imgCategory = category.imgCategory,
                sumProduct = category.products.Count()
            };
        }
        public static async Task<Category> ToCategoryEntity(this CreateCategoryDto categoryDto)
        {
            return new Category {
                Id = Guid.NewGuid(),
                name = categoryDto.name,
                parentsCategoryId = categoryDto.parentsCategoryId,
                subCategoryId = categoryDto.subCategoryId,
                products =  categoryDto.products,
                imgCategory = await UpLoadFileService.SaveImage(categoryDto.imgCategory, "ImgCategory")
                
            };

        }
        
    }
}