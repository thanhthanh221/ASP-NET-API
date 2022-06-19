using System;
using Domain_Layer.Base;

namespace Domain_Layer.Entities.Order
{
    public class Order : BaseEntity
    {
        public Double price {get; set;}
        public Guid userId {get; set;}
    }
}