using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Dto;
using Microsoft.AspNetCore.Http;
using System.Threading;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Domain_Layer.Interfaces;
using Domain_Layer.Entities.Order;
using Domain_Layer.Entities.Identity;
using Domain_Layer.Entities.Product;
using Domain_Layer.Models.OrderAggregate;
using API.Extension;
using API.Extension.Build.OrderDtoBuilder;

namespace API.Controllers
{
    [ApiController]
    [Route("Order")]
    
    public class OrderController : ControllerBase
    {
        private readonly IAsyncRepository<Order> orderRepository;
        private readonly IAsyncRepository<Product> productRepository;
        private readonly UserManager<ApplicationUser> userManager;

        public OrderController(
            IAsyncRepository<Order> orderRepository, 
            IAsyncRepository<Product> productRepository, 
            UserManager<ApplicationUser> userManager)
        {
            this.orderRepository = orderRepository;
            this.productRepository = productRepository;
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetFullOrderAsync()
        {
            List<Order> OrderFull = (await orderRepository.GetAllAsync()).ToList();
            IList<GetOrderDto> getOrderDtos = new List<GetOrderDto>();
            foreach (var item in OrderFull)
            {
                ApplicationUser user = await userManager.FindByIdAsync(item.BuyerId.ToString());
                if(user is null)
                {
                    continue;
                }
                List<GetOrderProductDto> getOrderProducts = new List<GetOrderProductDto>();
                foreach (OrderProduct orderProduct in item.orderProducts)
                {
                    Product product = await productRepository.GetAsync(orderProduct.ProductId);
                    if(product != null)
                    {
                        GetOrderProductDto kq = orderProduct.ToOrderProductDto();
                        kq.productName = product.Name;
                        kq.productPrice = product.Price;
                        getOrderProducts.Add(kq);
                    }      
                }
                getOrderDtos.Add(new GetOrderDtoBuilder()
                    .AddAddress(item.address)
                    .AddProductOrder(getOrderProducts)
                    .AddDeliveryInformation(item.deliveryInformation.ToList())
                    .AddPrice(item.Price)
                    .AddUserId(item.BuyerId)
                    .AddUserName(user.UserName.ToString())
                    .AddOrderDate(item.OrderDate)
                    .AddDescription(item.Description.ToString())
                    .Build());
            }
            return Ok(getOrderDtos);

        }
        [HttpGet("Id")]
        public async Task<IActionResult> GetOrderAsync(Guid Id)
        {
            Order order = await orderRepository.GetAsync(Id);
            if(order is null)
            {
                return NotFound();
            }
            ApplicationUser user = await userManager.FindByIdAsync(order.BuyerId.ToString());
            if(user is null)
            {
                return BadRequest(
                    new {
                        message = "Lỗi Không có người dùng trên"
                    }
                );
            }
            GetOrderDto orderDto = order.ToGetOrderDto();
            orderDto.userName = user.UserName;

            foreach (var item in orderDto.ProductOrder)
            {
                Product product = await productRepository.GetAsync(item.ProductId);
                if(product != null)
                {
                    item.productPrice = product.Price;
                    item.productName = product.Name;
                }      
            }
            orderDto.ProductOrder.RemoveAll(p => p.productPrice == 0);
            return Ok(orderDto);
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrderAsync(CreateUpdateOrderDto createUpdateOrderDto)
        {
            Order order = createUpdateOrderDto.CreateOrderProductMapper();
            ApplicationUser user = await userManager.FindByIdAsync(order.BuyerId.ToString());
            if(user is null)
            {
                return NotFound(
                    new {
                        message = "Không có người dùng đó"
                    }
                );
            }

            foreach (var item in order.orderProducts)
            {
                Product product = await productRepository.GetAsync(item.ProductId);
                if(product != null)
                {
                    order.Price += product.Price*item.amonut;
                }
                
            }
            await orderRepository.CreateAsync(order);
            return CreatedAtAction(nameof(CreateOrderAsync),order);
        }
        [HttpPut("Id")]
        public async Task<IActionResult> UpdateOrderAsync(CreateUpdateOrderDto createUpdateOrderDto)
        {
            return NoContent();
        }
        [HttpDelete("Id")]
        public async Task<IActionResult> DeleteOrderAsync(Guid Id)
        {
            
            return NoContent();
        }
    }
}