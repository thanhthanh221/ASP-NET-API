using System;
using Domain_Layer.Base;

namespace Domain_Layer.Entities.Order
{
    public class OrderProduct 
    {
        public Guid ProductId {get; set;}
        public int amonut {get; set;}        
    }
}