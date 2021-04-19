using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.DTO;
using WebStore.Interfaces;
using WebStore.Interfaces.Services;

namespace WebStore.ServiceHosting.Controllers
{
    [Route(WebAPI.Orders)]
    [ApiController]
    public class OrdersApiController : ControllerBase,IOrderService
    {
        private readonly IOrderService _orderService;


        public OrdersApiController(IOrderService orderService) => _orderService = orderService;

        [HttpPost("{userName}")]
        public async Task<OrderDTO> CreateOrder(string userName, [FromBody] CreateOrderModel orderModel) =>
            await _orderService.CreateOrder(userName, orderModel);

        [HttpGet("{id}")]
        public async Task<OrderDTO> GetOrderById(int id) => await _orderService.GetOrderById(id);

        [HttpGet("user/{userName}")]
        public async Task<IEnumerable<OrderDTO>> GetUserOrder(string userName) =>
            await _orderService.GetUserOrder(userName);
    }
}
