using Domain_Layer.Base;
using System;

namespace Domain_Layer.Entities.Order
{
    public class ProductOrder : BaseEntity
    {
        public Guid OrderId {get; set;}
        public int count {get; set;}
        public Guid ProductId {get; set;}
        public Double Price {get; set;}       
    }
}