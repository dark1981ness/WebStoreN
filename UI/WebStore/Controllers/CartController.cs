using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.DTO;
using WebStore.Domain.ViewModels;
using WebStore.Interfaces.Services;

namespace WebStore.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartServices _cartServices;

        public CartController(ICartServices cartServices) => _cartServices = cartServices;

        public IActionResult Index() => View(new CartOrderViewModel { Cart = _cartServices.GetViewModel() });

        public IActionResult Add(int id)
        {
            _cartServices.Add(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Remove(int id)
        {
            _cartServices.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Decrement(int id)
        {
            _cartServices.Decrement(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Clear()
        {
            _cartServices.Clear();
            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        public async Task<IActionResult> CheckOut(OrderViewModel orderModel, [FromServices] IOrderService orderService)
        {
            if (!ModelState.IsValid)
            {
                return View(nameof(Index), new CartOrderViewModel
                {
                    Cart = _cartServices.GetViewModel(),
                    Order = orderModel
                });
            }

            //var order = await orderService.CreateOrder(
            //    User.Identity!.Name,
            //    _cartServices.GetViewModel(),
            //    orderModel
            //    );

            var order_model = new CreateOrderModel
            {
                Order = orderModel,
                Items = _cartServices.GetViewModel().Items.Select(item => new OrderItemDTO
                {
                    Price=item.product.Price,
                    Quantity=item.quantity,
                }).ToList()
            };

            var order = await orderService.CreateOrder(User.Identity!.Name, order_model);

            _cartServices.Clear();

            return RedirectToAction(nameof(OrderConfirmed), new { order.Id});
        }

        public IActionResult OrderConfirmed(int id)
        {
            ViewBag.OrderId = id;
            return View();
        }
    }
}
