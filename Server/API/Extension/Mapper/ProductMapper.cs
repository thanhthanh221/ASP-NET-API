using System;
using System.IO;
using System.Threading.Tasks;
using BackEnd.Dto;
using Domain_Layer.Entities.Product;
using Domain_Layer.Services;
using Microsoft.AspNetCore.Http;

namespace API.Extension
{
    public static class ProductMapper
    {
        public static Product ToProductEntity(this CreateUpdateProductDto productDto)
        {
            return new Product {
                Id = Guid.NewGuid(),
                Name = productDto.Name,
                Price = productDto.Price,
                Describe = productDto.Describe,
                DateTimeCreate = DateTimeOffset.UtcNow,
                UserSellId = productDto.UserId,
                numberOfStars = productDto.numberStart
            };
        }
        public static async Task<ImgAndVideoProduct> ToImgAndVideoProduct(IFormFile file, Product product)
        {
            return new ImgAndVideoProduct
            {
                Id = Guid.NewGuid(),
                Photo = await UpLoadFileService.SaveImage(file, "ImgProduct"),
                ProductId = product.Id
            };
        }
        public static GetProductDto ToProductDto(this Product product)
        {
            return new GetProductDto 
            {
                Id = product.Id,
                UserSellId = product.UserSellId,
                Name = product.Name,
                Price = product.Price,
                Describe = product.Describe,
                numberOfStars = product.numberOfStars,
                files =  null,
            };
        }
        
    }
}