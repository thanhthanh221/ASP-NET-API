using System;
using System.IO;
using System.Threading.Tasks;
using API.Dto;
using Domain_Layer.Entities.Product;

namespace API.Extension.Mapper
{
    public static class ProductMapper
    {
        public static GetProductDto ToProductDto(this Product product)
        {
            return new GetProductDto 
            {
                Id = product.Id,
                UserSellId = product.UserSellId,
                Name = product.Name,
                Price = product.Price,
                Describe = product.Describe,
                numberOfStars = product.numberOfStars
            };
        }
        
    }
}