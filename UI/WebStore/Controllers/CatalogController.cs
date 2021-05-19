using System;
using System.Collections.Generic;
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
        private const string _catalogPageSize = "CatalogPageSize";

        public CatalogController(IProductData productData, IConfiguration configuration)
        {
            _productData = productData;
            _configuration = configuration;
        }

        public IActionResult Index(int? brandId, int? sectionId, int page = 1, int? pageSize = null)
        {
            var page_size = pageSize
                ?? (int.TryParse(_configuration[_catalogPageSize], out var value) ? value : null);

            var filters = new ProductFilter
            {
                BrandId = brandId,
                SectionId = sectionId,
                Page = page,
                PageSize = page_size
            };

            var (products, total_count) = _productData.GetProducts(filters);

            return View(new CatalogViewModel
            {
                SectionId = sectionId,
                BrandId = brandId,
                Products = products
                .OrderBy(p => p.Order)
                .FromDTO()
                .ToView(),
                PageViewModel = new PageViewModel
                {
                    Page = page,
                    PageSize = page_size ?? 0,
                    TotalItems = total_count
                }
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

        #region WebAPI

        public IActionResult GetFeaturesItems(int? brandId, int? sectionId, int page = 1, int? pageSize = null) =>
            PartialView("Partial/_FeaturesItems", GetProducts(brandId, sectionId, page, pageSize));

        private IEnumerable<ProductViewModel> GetProducts(int? brandId, int? sectionId, int page, int? pageSize) =>
            _productData.GetProducts(new ProductFilter
            {
                SectionId = sectionId,
                BrandId = brandId,
                Page = page,
                PageSize = pageSize ?? (int.TryParse(_configuration[_catalogPageSize], out var size) ? size : null)
            })
               .Products.OrderBy(p => p.Order)
               .FromDTO()
               .ToView();

        #endregion
    }
}
