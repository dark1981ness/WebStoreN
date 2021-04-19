using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Domain.DTO;
using WebStore.Domain.Entities.Orders;

namespace WebStore.Services.Mapping
{
    public static class OrderMapper
    {
        public static OrderItemDTO ToDTO(this OrderItem item) => item is null
            ? null
            : new OrderItemDTO
            {
                Id=item.Id,
                Price=item.Price,
                Quantity=item.Quantity,
            };

        public static OrderItem FromDTO(this OrderItemDTO item) => item is null
            ? null
            : new OrderItem
            {
                Id = item.Id,
                Price = item.Price,
                Quantity = item.Quantity,
            };

        public static OrderDTO ToDTO(this Order order) => order is null
            ? null
            : new OrderDTO
            {
                Id=order.Id,
                Name=order.Name,
                Address=order.Address,
                Phone=order.Phone,
                Date=order.Date,
                Items=order.Items.Select(ToDTO)
            };

        public static Order FromDTO(this OrderDTO order) => order is null
            ? null
            : new Order
            {
                Id = order.Id,
                Name = order.Name,
                Address = order.Address,
                Phone = order.Phone,
                Date = order.Date,
                Items = order.Items.Select(FromDTO).ToList()
            };
    }
}
