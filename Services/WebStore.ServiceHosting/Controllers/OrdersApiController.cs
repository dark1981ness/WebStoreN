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
    /// <summary>
    /// Упарвление заказами
    /// </summary>
    [Route(WebAPI.Orders)]
    [ApiController]
    public class OrdersApiController : ControllerBase,IOrderService
    {
        private readonly IOrderService _orderService;


        public OrdersApiController(IOrderService orderService) => _orderService = orderService;

        /// <summary>
        /// Создание нового заказа
        /// </summary>
        /// <param name="userName">Имя пользователя заказчика</param>
        /// <param name="orderModel">Информация о заказе</param>
        /// <returns>Тнформация о сформированном заказе</returns>
        [HttpPost("{userName}")]
        public async Task<OrderDTO> CreateOrder(string userName, [FromBody] CreateOrderModel orderModel) =>
            await _orderService.CreateOrder(userName, orderModel);

        /// <summary>
        /// Получение заказа по его идентификатору
        /// </summary>
        /// <param name="id">Идентификатор запрашиваемого заказа</param>
        /// <returns>Информация о заказе</returns>
        [HttpGet("{id}")]
        public async Task<OrderDTO> GetOrderById(int id) => await _orderService.GetOrderById(id);

        /// <summary>
        /// Получение всех заказов указанного пользователя
        /// </summary>
        /// <param name="userName">Имя пользовтеля</param>
        /// <returns>Перечень заказов, сделанных указанным пользователем</returns>
        [HttpGet("user/{userName}")]
        public async Task<IEnumerable<OrderDTO>> GetUserOrder(string userName) =>
            await _orderService.GetUserOrder(userName);
    }
}
