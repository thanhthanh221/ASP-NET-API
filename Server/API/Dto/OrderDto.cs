using System;
using System.Collections.Generic;
using Domain_Layer.Entities.Order;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BackEnd.Dto
{
    public class GetOrderDto 
    {

        public String userName {get; set;}
        public Guid UserId {get; set;}
        public List<GetOrderProductDto> ProductOrder {get; set;}
        public decimal Price {get; set;}
        public IList<DeliveryInformation> deliveryInformation {get; set;}
        public String Description {get; set;}
        public Address Address {get; set;}
        public DateTimeOffset OrderDate {get; set;}
    }
    public class CreateUpdateOrderDto 
    {
        [Required]
        public Guid BuyerId {get; set;}
        [Required]
        public List<OrderProduct> ProductOrder {get; set;}
        [Required]
        public String Description {get; set;}
        [Required]
        public Address Address {get; set;}
    }
    public class GetOrderProductDto
    {   
        [Required]
        public string productName {get; set;}
        [Required]
        public Guid ProductId {get; set;}
        [Required]
        public decimal productPrice {get; set;}
        [Required]
        [Range(1, int.MaxValue)]
        public int amonut {get; set;}
    }
    public class GetAddressDto
    {
        [Required]
        public String City {get; set;}
        [Required]
        public String District {get; set;}
        [Required]
        public String Commune {get; set;}
        [Required]
        public String Street {get; set;}
    }
}