using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Interfaces.Services;

namespace WebStore.Components
{
    public class CartViewComponent:ViewComponent
    {
        private readonly ICartServices _cartServices;

        public CartViewComponent(ICartServices cartServices) => _cartServices = cartServices;

        public IViewComponentResult Invoke() => View(_cartServices.GetViewModel());
    }
}
