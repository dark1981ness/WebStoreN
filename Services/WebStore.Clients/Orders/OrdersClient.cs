using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebStore.Clients.Base;
using WebStore.Domain.DTO;
using WebStore.Interfaces;
using WebStore.Interfaces.Services;

namespace WebStore.Clients.Orders
{
    public class OrdersClient : BaseClient, IOrderService
    {
        public OrdersClient(IConfiguration configuration) : base(configuration, WebAPI.Orders) { }

        public async Task<OrderDTO> CreateOrder(string userName, CreateOrderModel orderModel)
        {
            var response = await PostAsync($"{Address}/{userName}", orderModel);
            return await response.Content.ReadAsAsync<OrderDTO>();
        }

        public async Task<OrderDTO> GetOrderById(int id) => await GetAsync<OrderDTO>($"{Address}/{id}");
       

        public async Task<IEnumerable<OrderDTO>> GetUserOrder(string userName) =>
            await GetAsync<IEnumerable<OrderDTO>>($"{Address}/user/{userName}");


    }
}
