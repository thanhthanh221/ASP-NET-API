using API.Dto.OrderDtos;
using Domain_Layer.Entities.Identity;
using Domain_Layer.Entities.Order;
using Domain_Layer.Models.OrderAggregate;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Extension.Mapper 
{
    public static class OrderMapper 
    {
        public static GetOrderDto ToGetOrderDto(this Order order)
        {
            return new GetOrderDto {
                userName = null,
                UserId = order.BuyerId,
                Price = order.Price,
                Description = order.Description,
                OrderDate = order.OrderDate,
                Address = order.address,
                deliveryInformation = order.deliveryInformation,
                ProductOrder = order.orderProducts.Select(p => p.ToOrderProductDto()).ToList()
            };

        }
        public static GetOrderProductDto ToOrderProductDto(this OrderProduct orderProduct) 
        {
            return new GetOrderProductDto {
                        ProductId = orderProduct.ProductId,
                        productName = null,
                        amonut = orderProduct.amonut,
                        productPrice = 0
                    };
        }
        public static Order CreateOrderProductMapper(this CreateUpdateOrderDto createUpdateOrder)
        {
            return new Order {
                Id = Guid.NewGuid(),
                BuyerId = createUpdateOrder.BuyerId,
                Description = createUpdateOrder.Description,
                deliveryInformation = new List<DeliveryInformation> {
                    new DeliveryInformation (DateTimeOffset.UtcNow, "Tạo đơn hàng thành công")
                },
                address = createUpdateOrder.Address,
                orderProducts = createUpdateOrder.ProductOrder.ToList<OrderProduct>(),
                Price = 0,
                OrderDate = DateTimeOffset.UtcNow
            };
            
        }
    } 
}