using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.DAL.Context;
using WebStore.Domain.Entities.Identity;
using WebStore.Domain.Entities.Orders;
using WebStore.Domain.ViewModels;
using WebStore.Interfaces.Services;

namespace WebStore.Infrastructure.Services.InSql
{
    public class SqlOrderService : IOrderService
    {
        private readonly WebStoreDB _db;
        private readonly UserManager<User> _userManager;

        public SqlOrderService(WebStoreDB db, UserManager<User> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public async Task<Order> CreateOrder(string userName, CartViewModel cartViewModel, OrderViewModel orderViewModel)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user is null)
                throw new InvalidOperationException($"Пользователь с именем {userName} в БД отсутствует");

            await using var transaction = await _db.Database.BeginTransactionAsync();

            var order = new Order
            {
                Name = orderViewModel.Name,
                Address = orderViewModel.Address,
                Phone = orderViewModel.Phone,
                User = user
            };

            var product_ids = cartViewModel.Items.Select(item => item.product.Id).ToArray();

            var cart_products = await _db.Products
                .Where(p => product_ids.Contains(p.Id))
                .ToArrayAsync();

            order.Items = cartViewModel.Items.Join(
                cart_products,
                cart_item => cart_item.product.Id,
                product => product.Id,
                (cart_item, product) => new OrderItem
                {
                    Order = order,
                    Product = product,
                    Price = product.Price,
                    Quantity = cart_item.quantity
                }).ToArray();

            await _db.Orders.AddAsync(order);

            await _db.SaveChangesAsync();

            await transaction.CommitAsync();

            return order;
        }

        public async Task<Order> GetOrderById(int id) => await _db.Orders
            .Include(order => order.User)
            .Include(order => order.Items)
            .FirstOrDefaultAsync(order => order.Id == id);

        public async Task<IEnumerable<Order>> GetUserOrder(string userName) => await _db.Orders
            .Include(order => order.User)
            .Include(order => order.Items)
            .Where(order => order.User.UserName == userName)
            .ToArrayAsync();
    }
}
