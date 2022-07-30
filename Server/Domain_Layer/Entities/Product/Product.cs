using System;
using Domain_Layer.Base;
using System.Collections.Generic;

namespace Domain_Layer.Entities.Product
{
    public class Product : BaseEntity
    {
        public decimal Price {get; set;}
        public Guid UserSellId {get; set;}
        public string Name {get; set;}
        public string Describe {get; set;}
        public List<Guid> categories {get; set;}
        public DateTimeOffset DateTimeCreate {get; set;}
        public double numberOfStars {get; set;}
        public List<string> ImgAndVideoProducts {get; set;}
    }
}