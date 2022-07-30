using System;
using System.Collections.Generic;
using API.Dto;

namespace API.Extension.Build.ProductDtoBuider
{
    public class GetProductDtoBuilder : IGetProductDtoBuilder
    {
        public Guid Id {get; set;}
        public Guid UserSellId {get; set;}
        public String Name {get; set;}
        public String Describe {get; set;}
        public decimal Price {get; set;}
        public List<Guid> CategoryId {get; set;}
        public double numberOfStars {get; set;}
        public IEnumerable<string> files {get; set;}
        public IList<string> imgAndVideoProducts {get; set;}

        public GetProductDtoBuilder AddCategoryId(List<Guid> CategoryId)
        {
            this.CategoryId = CategoryId;
            return this;
        }

        public GetProductDtoBuilder AddDescribe(string Describe)
        {
            this.Describe = Describe;
            return this;
        }

        public GetProductDtoBuilder AddId(Guid Id)
        {
            this.Id = Id;
            return this;
        }
        public GetProductDtoBuilder AddImgAndVideoProducts(List<string> imgAndVideoProducts)
        {
            this.imgAndVideoProducts = imgAndVideoProducts;
            return this;
        }

        public GetProductDtoBuilder AddName(string Name)
        {
            this.Name = Name;
            return this;
        }

        public GetProductDtoBuilder AddNumberOfStars(double numberOfStars)
        {
            this.numberOfStars = numberOfStars;
            return this;
        }

        public GetProductDtoBuilder AddPrice(decimal Price)
        {
            this.Price = Price;
            return this;
        }

        public GetProductDtoBuilder AddUserSellId(Guid UserSellId)
        {
            this.UserSellId = UserSellId;
            return this;
        }

        public GetProductDto Build()
        {
            return new GetProductDto() {
                Id = Id,
                Name = Name,
                Describe = Describe,
                Price = Price,
                CategoryId = CategoryId,
                numberOfStars = numberOfStars,
                imgAndVideoProducts = imgAndVideoProducts,
                UserSellId = UserSellId


            };
        }
    }
}