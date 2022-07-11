using System;
using System.Collections.Generic;
using BackEnd.Dto;
using Domain_Layer.Entities.Order;

namespace API.Extension.Build.OrderDtoBuilder
{
    public class GetOrderDtoBuilder : IGetOrderBuilder
    {
        public string userName {get; set;}
        public Guid UserId {get; set;}
        public List<GetOrderProductDto> ProductOrder {get; set;}
        public decimal Price {get; set;}
        public IList<DeliveryInformation> deliveryInformation {get; set;}
        public String Description {get; set;}
        public Address Address {get; set;}
        public DateTimeOffset OrderDate {get; set;}

        public GetOrderDtoBuilder AddAddress(Address address)
        {
            this.Address = address;
            return this;
        }

        public GetOrderDtoBuilder AddDeliveryInformation(List<DeliveryInformation> deliveryInformation)
        {
            this.deliveryInformation = deliveryInformation;
            return this;
        }

        public GetOrderDtoBuilder AddDescription(string description)
        {
            this.Description = description;
            return this;
        }

        public GetOrderDtoBuilder AddOrderDate(DateTimeOffset OrderDate)
        {
            this.OrderDate = OrderDate;
            return this;
        }

        public GetOrderDtoBuilder AddPrice(decimal Price)
        {
          this.Price = Price;
          return this;
        }

        public GetOrderDtoBuilder AddProductOrder(List<GetOrderProductDto> productOrder)
        {
            this.ProductOrder = productOrder;
            return this;
            
        }

        public GetOrderDtoBuilder AddUserId(Guid UserId)
        {
            this.UserId = UserId;
            return this;
        }

        public GetOrderDtoBuilder AddUserName(string UserName)
        {
            this.userName = UserName;
            return this;
        }

        public GetOrderDto Build()
        {
            return new GetOrderDto {
                userName = this.userName,
                UserId = UserId,
                Price = Price,
                Description = Description,
                OrderDate = OrderDate,
                Address = Address,
                deliveryInformation = deliveryInformation,
                ProductOrder =  ProductOrder
            };
        }
    }
}