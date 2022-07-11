using System;
using System.Collections.Generic;
using Domain_Layer.Base;

namespace Domain_Layer.Entities.Order
{
    public class Order : BaseEntity
    {
        public Guid BuyerId {get; set;}
        public DateTimeOffset OrderDate {get; set;}
        public Address address {get; set;}
        public String Description {get; set;}
        public IList<DeliveryInformation> deliveryInformation {get; set;}
        public decimal Price {get; set;}
        public List<OrderProduct> orderProducts {get; set;}
        
    }
}