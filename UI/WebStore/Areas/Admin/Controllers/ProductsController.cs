using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.Entities.Identity;
using WebStore.Interfaces.Services;
using WebStore.Services.Mapping;

namespace WebStore.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize(Roles =Role._administrators)]
    public class ProductsController : Controller
    {
        private readonly IProductData _productData;

        public ProductsController(IProductData productData) => _productData = productData;

        public IActionResult Index() => View(_productData.GetProducts().FromDTO());

        public IActionResult Edit(int id) => _productData.GetProductById(id) is { } product
            ? View(product.FromDTO())
            : NotFound();

        public IActionResult Delete(int id) => _productData.GetProductById(id) is { } product
            ? View(product.FromDTO())
            : NotFound();
    }
}
