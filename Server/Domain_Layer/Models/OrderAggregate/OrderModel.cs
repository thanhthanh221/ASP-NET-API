using System;
using System.Collections.Generic;
using System.Linq;
using Domain_Layer.Entities.Order;

namespace Domain_Layer.Models.OrderAggregate
{
    public class OrderModel 
    {

        public Guid BuyerId {get; set;}
        public DateTimeOffset OrderDate {get; set;}
        public Address Address { get; set; }
        public String Description {get; set;}
        public Double Price {get; set;}
        public IEnumerable<DeliveryInformation> orderStaus {get; set;}
        public IEnumerable<OrderProduct> orderProducts {get; set;}  
    }
}