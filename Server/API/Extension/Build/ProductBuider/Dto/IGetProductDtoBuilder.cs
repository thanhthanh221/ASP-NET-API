using System;
using System.Collections.Generic;
using API.Dto;

namespace API.Extension.Build.ProductDtoBuider
{
    public interface IGetProductDtoBuilder
    {
        GetProductDtoBuilder AddId(Guid Id);
        GetProductDtoBuilder AddName(String Name);
        GetProductDtoBuilder AddUserSellId(Guid UserSellId);
        GetProductDtoBuilder AddPrice(decimal Price);
        GetProductDtoBuilder AddDescribe(String Describe);
        GetProductDtoBuilder AddCategoryId(List<Guid> CategoryId);
        GetProductDtoBuilder AddNumberOfStars(double numberOfStars);
        GetProductDtoBuilder AddImgAndVideoProducts(List<string> imgAndVideoProducts);
        GetProductDto Build();
    }
}