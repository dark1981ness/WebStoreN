using System.Collections.Generic;
using System.Threading.Tasks;
using WebStore.Domain.DTO;

namespace WebStore.Interfaces.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDTO>> GetUserOrder(string userName);

        Task<OrderDTO> GetOrderById(int id);

        Task<OrderDTO> CreateOrder(string userName, CreateOrderModel orderModel);

    }
}
