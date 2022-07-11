using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain_Layer.Entities.Order;

namespace API.Extension 
{
    public static class OrderLinq 
    {
        // public static IEnumerable<OrderProduct>  OrderProductInOrder(
        //     IEnumerable<OrderProduct> orderProducts,
        //     Guid OrderId)
        // {
        //     IEnumerable<OrderProduct> fullProductInOrder = from a in orderProducts
        //                                                     where a.OrderId.Equals(OrderId)
        //                                                     select (a == null) ? null: a;
        //     return fullProductInOrder;
        // }
        // public static IEnumerable<Order> fullOrders(
        //     IEnumerable<Order> orders,
        //     IEnumerable<OrderProduct> orderProducts
        // )
        // {
        //     IEnumerable<Order> orderfull = from a in orders
        //                                 join b in orderProducts on a.Id equals b.OrderId into t
        //                                 from b in t.DefaultIfEmpty()
        //                                 select a;

        //     return null;
        // }
    }
}