using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain;
using WebStore.Domain.DTO;
using WebStore.Interfaces;
using WebStore.Interfaces.Services;

namespace WebStore.ServiceHosting.Controllers
{
    [Route(WebAPI.Products)]
    [ApiController]
    public class ProductsApiController : ControllerBase, IProductData
    {
        private readonly IProductData _productData;

        public ProductsApiController(IProductData productData) => _productData = productData;

        [HttpGet("brands")]
        public IEnumerable<BrandDTO> GetBrands() => _productData.GetBrands();

        [HttpGet("{id}")]
        public ProductDTO GetProductById(int id) => _productData.GetProductById(id);

        [HttpPost]
        public IEnumerable<ProductDTO> GetProducts(ProductFilter productFilter = null) => _productData.GetProducts(productFilter);

        [HttpGet("sections")]
        public IEnumerable<SectionDTO> GetSections() => _productData.GetSections();
    }
}
