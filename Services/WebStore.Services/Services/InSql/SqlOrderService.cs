using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebStore.DAL.Context;
using WebStore.Domain.DTO;
using WebStore.Domain.Entities.Identity;
using WebStore.Domain.Entities.Orders;
using WebStore.Interfaces.Services;
using WebStore.Services.Mapping;

namespace WebStore.Services.Services.InSql
{
    public class SqlOrderService : IOrderService
    {
        private readonly WebStoreDB _db;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<SqlOrderService> _logger;

        public SqlOrderService(WebStoreDB db, UserManager<User> userManager, ILogger<SqlOrderService> logger)
        {
            _db = db;
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<OrderDTO> CreateOrder(string userName, CreateOrderModel orderModel)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user is null)
                throw new InvalidOperationException($"Пользователь с именем {userName} в БД отсутствует");

            _logger.LogInformation($"Оформление нового заказа для {userName}");

            var timer = Stopwatch.StartNew();

            await using var transaction = await _db.Database.BeginTransactionAsync();

            var order = new Order
            {
                Name = orderModel.Order.Name,
                Address = orderModel.Order.Address,
                Phone = orderModel.Order.Phone,
                User = user
            };

            //var product_ids = cartViewModel.Items.Select(item => item.product.Id).ToArray();

            //var cart_products = await _db.Products
            //    .Where(p => product_ids.Contains(p.Id))
            //    .ToArrayAsync();

            //order.Items = cartViewModel.Items.Join(
            //    cart_products,
            //    cart_item => cart_item.product.Id,
            //    product => product.Id,
            //    (cart_item, product) => new OrderItem
            //    {
            //        Order = order,
            //        Product = product,
            //        Price = product.Price,
            //        Quantity = cart_item.quantity
            //    }).ToArray();

            foreach (var item in orderModel.Items)
            {
                var product = await _db.Products.FindAsync(item.ProductId);

                if (product is null) continue;

                var order_item = new OrderItem
                {
                    Order = order,
                    Price = product.Price,
                    Quantity = item.Quantity,
                    Product = product
                };

                order.Items.Add(order_item);
            }

            await _db.Orders.AddAsync(order);

            await _db.SaveChangesAsync();

            await transaction.CommitAsync();

            _logger.LogInformation($"Заказ для {userName} успешно сформирован за {timer.Elapsed} с id:{order.Id} на сумму {order.Items.Sum(i=>i.TotalItemPrice)}");

            return order.ToDTO();
        }

        public async Task<OrderDTO> GetOrderById(int id) => (await _db.Orders
            .Include(order => order.User)
            .Include(order => order.Items)
            .FirstOrDefaultAsync(order => order.Id == id))
            .ToDTO();

        public async Task<IEnumerable<OrderDTO>> GetUserOrder(string userName) => (await _db.Orders
            .Include(order => order.User)
            .Include(order => order.Items)
            .Where(order => order.User.UserName == userName)
            .ToArrayAsync())
            .Select(order=>order.ToDTO());
    }
}
