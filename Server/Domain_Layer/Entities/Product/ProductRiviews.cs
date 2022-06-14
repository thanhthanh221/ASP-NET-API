using System;
using System.Collections.Generic;

namespace Domain_Layer.Entities.Product
{
    public class ProductRiviews
    {
        public Guid userId {get; set;}
        public Guid ProductId {get; set;}
        public string Comment {get; set;}
        public List<String> ImgFileProductUser {get; set;}
        public int numberOfStars {get; set;}
        public DateTimeOffset dateTimeCreate {get; set;}
        
    }
}