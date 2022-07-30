using System;
using System.Collections.Generic;
using API.Dto.OrderDtos;
using Domain_Layer.Entities.Order;

namespace API.Extension.Build.OrderDtoBuilder
{
    public interface IGetOrderBuilder
    {
        GetOrderDtoBuilder AddUserName(string UserName);
        GetOrderDtoBuilder AddUserId(Guid UserId);
        GetOrderDtoBuilder AddProductOrder(List<GetOrderProductDto> productOrder);
        GetOrderDtoBuilder AddPrice(decimal Price);
        GetOrderDtoBuilder AddDeliveryInformation(List<DeliveryInformation> deliveryInformation);
        GetOrderDtoBuilder AddDescription(string description);
        GetOrderDtoBuilder AddAddress(Address address);
        GetOrderDtoBuilder AddOrderDate(DateTimeOffset OrderDate);
        GetOrderDto Build();

    }
}