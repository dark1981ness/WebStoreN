using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Data;
using WebStore.Domain;
using WebStore.Domain.Entities;
using WebStore.Infrastructure.Services.Interfaces;

namespace WebStore.Infrastructure.Services.InMemory
{
    public class InMemoryProductData : IProductData
    {
        public IEnumerable<Brand> GetBrands() => TestData.Brands;

        public Product GetProductById(int id) => TestData.Products.FirstOrDefault(p => p.Id == id);

        public IEnumerable<Product> GetProducts(ProductFilter productFilter = null)
        {
            var query = TestData.Products;
            if (productFilter?.SectionId is { } section_id)
                query = query.Where(product => product.SectionId == section_id);
            if(productFilter?.BrandId is { } brand_id)
                query = query.Where(product => product.BrandId == brand_id);
            return query;
        }

        public IEnumerable<Section> GetSections() => TestData.Sections;


    }
}
