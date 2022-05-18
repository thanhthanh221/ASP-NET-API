using System;
using System.Collections.Generic;
using System.IO;
using BackEnd.Dto;
using BackEnd.Entities;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace BackEnd {
    // Các trường hợp xử lý biến đổi
    public static class Extensions{
        public static ItemDto AsDto(this Item item){
            return new ItemDto{
                Id = item.Id,
                Name = item.Name,
                Price =  item.Price,
                CreateDate = item.CreateDate
            }; 
        }
        public static GetProductDto AsDtoGetProduct(this Product product)
        {
            return new GetProductDto 
            {
                Name = product.Name,
                Price = product.Price,
                Describe = product.Describe,
                numberOfStars = product.numberOfStars,
                files =  null
            };
        }
        public static GetCategoryDto AsGetCategory(this Category category)
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
    }

}