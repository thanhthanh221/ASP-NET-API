using Domain_Layer.Entities.Order;
using System.Collections.Generic;
using System;
using Domain_Layer.Entities.Product;

namespace Domain_Layer.Models.OrderAggregate
{
    public class OrderFullEntity
    {
        public OrderFullEntity(
            Order order, 
            Address address, 
            IEnumerable<DeliveryInformation> deliveryInformations, 
            IEnumerable<OrderProduct> orderProduct)
        {
            this.order = order;
            this.address = address;
            this.deliveryInformations = deliveryInformations;
            this.orderProduct = orderProduct;
        }

        public Order order {get; set;}
        public Address address {get; set;}
        public IEnumerable<DeliveryInformation> deliveryInformations{get; set;}
        public IEnumerable<OrderProduct> orderProduct {get; set;}
    }
}