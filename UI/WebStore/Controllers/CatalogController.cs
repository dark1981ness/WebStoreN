using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebStore.Domain;
using WebStore.Domain.ViewModels;
using WebStore.Interfaces.Services;
using WebStore.Services.Mapping;

namespace WebStore.Controllers
{
    public class CatalogController : Controller
    {
        private readonly IProductData _productData;
        private readonly IConfiguration _configuration;

        public CatalogController(IProductData productData, IConfiguration configuration)
        {
            _productData = productData;
            _configuration = configuration;
        }

        public IActionResult Index(int? brandId, int? sectionId, int page = 1, int? pageSize = null)
        {
            var page_size = pageSize
                ?? (int.TryParse(_configuration["CatalogPageSize"], out var value) ? value : null);

            var filters = new ProductFilter
            {
                BrandId = brandId,
                SectionId = sectionId,
                Page = page,
                PageSize = page_size
            };

            var products = _productData.GetProducts(filters);

            return View(new CatalogViewModel
            {
                SectionId = sectionId,
                BrandId = brandId,
                Products = products
                .Products
                .OrderBy(p => p.Order)
                .FromDTO()
                .ToView()
            });
        }

        public IActionResult Details(int id)
        {
            var product = _productData.GetProductById(id);

            if (product is null)
            {
                return NotFound();
            }

            return View(product.FromDTO().ToView());
        }
    }
}
