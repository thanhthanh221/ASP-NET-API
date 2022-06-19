using System;
using System.Collections.Generic;
using Domain_Layer.Base;

namespace Domain_Layer.Entities.Product
{
    public class ProductReviews : BaseEntity
    {
        public Guid userId {get; set;}
        public Guid ProductId {get; set;}
        public string Comment {get; set;}
        public int numberOfStars {get; set;}
        public DateTimeOffset dateTimeCreate {get; set;}
        
    }
}