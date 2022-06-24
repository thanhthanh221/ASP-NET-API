using System;
using System.Collections.Generic;
using System.IO;
using BackEnd.Dto;
using Microsoft.AspNetCore.Http;
using System.Linq;
using Domain_Layer.Entities;
using Domain_Layer.Entities.Product;

namespace BackEnd {
    // Các trường hợp xử lý biến đổi
    public static class Extensions{
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