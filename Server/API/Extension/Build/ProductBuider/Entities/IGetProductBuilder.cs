using System;
using System.Collections.Generic;
using Domain_Layer.Entities.Product;

namespace API.Extension.Build.ProductDtoBuider
{
    public interface IGetProductBuilder
    {
        GetProductBuilder AddId(Guid Id);
        GetProductBuilder AddPrice(decimal Price);
        GetProductBuilder AddUserSellId(Guid UserSellId);
        GetProductBuilder AddNameProduct(String name);
        GetProductBuilder AddDescribe(string Describe);
        GetProductBuilder AddCategiries(List<Guid> categories);
        GetProductBuilder AddStarProduct(double numberOfStars);
        GetProductBuilder AddDateCreate(DateTimeOffset dateTimeOffset);
        GetProductBuilder AddFileProduct(List<string> imgAndVideoProducts);
        Product Build();
    }
}