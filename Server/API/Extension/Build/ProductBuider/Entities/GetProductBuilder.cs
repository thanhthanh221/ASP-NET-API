using System;
using System.Collections.Generic;
using Domain_Layer.Entities.Product;

namespace API.Extension.Build.ProductDtoBuider
{
    public class GetProductBuilder : IGetProductBuilder
    {
        public Guid Id {get; set;}
        public decimal Price {get; set;}
        public Guid UserSellId {get; set;}
        public string Name {get; set;}
        public string Describe {get; set;}
        public List<Guid> categories {get; set;}
        public DateTimeOffset DateTimeCreate {get; set;}
        public double numberOfStars {get; set;}
        public List<string> ImgAndVideoProducts {get; set;}

        public GetProductBuilder AddCategiries(List<Guid> categories)
        {
            this.categories = categories;
            return this;
        }

        public GetProductBuilder AddDateCreate(DateTimeOffset dateTimeOffset)
        {
            this.DateTimeCreate = dateTimeOffset;
            return this;
        }

        public GetProductBuilder AddDescribe(string Describe)
        {
            this.Describe = Describe;
            return this;
        }

        public GetProductBuilder AddFileProduct(List<string> imgAndVideoProducts)
        {
            this.ImgAndVideoProducts = imgAndVideoProducts;
            return this;
        }

        public GetProductBuilder AddId(Guid Id)
        {
            this.Id = Id;
            return this;
        }

        public GetProductBuilder AddNameProduct(string name)
        {
            this.Name = name;
            return this;
        }

        public GetProductBuilder AddPrice(decimal Price)
        {
            this.Price = Price;
            return this ;
        }

        public GetProductBuilder AddStarProduct(double numberOfStars)
        {
            this.numberOfStars = numberOfStars;
            return this;
        }

        public GetProductBuilder AddUserSellId(Guid UserSellId)
        {
           this.UserSellId = UserSellId;
           return this;
        }

        public Product Build()
        {
            return new Product {
                Id = Id,
                Price = Price,
                UserSellId = UserSellId,
                Name = Name,
                Describe = Describe,
                categories = categories,
                DateTimeCreate = DateTimeCreate,
                numberOfStars = numberOfStars,
                ImgAndVideoProducts = ImgAndVideoProducts

            };
        }
    }
}