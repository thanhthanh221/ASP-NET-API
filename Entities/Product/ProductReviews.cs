using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Collections.Generic;

namespace BackEnd.Entities
{
    // Tầng cuối cùng Entites của DDD
    public class ProductsReviews : Entities
    {
        public Guid userId {get; set;}
        public string Comment {get; set;}
        public List<String> ImgFileProductUser {get; set;}
        public int numberOfStars {get; set;}
    }
}