using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using WebStore.DAL.Context;
using WebStore.Domain;
using WebStore.Domain.DTO;
using WebStore.Domain.Entities;
using WebStore.Interfaces.Services;
using WebStore.Services.Mapping;

namespace WebStore.Services.Services.InSql
{
    public class SqlProductData : IProductData
    {
        private readonly WebStoreDB _db;

        public SqlProductData(WebStoreDB db) => _db = db;

        public WebStoreDB Db { get; }

        public IEnumerable<BrandDTO> GetBrands() => _db.Brands.Include(b => b.Products).ToDTO();

        public IEnumerable<SectionDTO> GetSections() => _db.Sections.Include(s => s.Products).ToDTO();

        public PageProductsDTO GetProducts(ProductFilter productFilter = null)
        {
            IQueryable<Product> query = _db.Products
                .Include(p => p.Section)
                .Include(p => p.Brand);

            if (productFilter?.Ids?.Length > 0)
                query = query.Where(product => productFilter.Ids.Contains(product.Id));
            else
            {
                if (productFilter?.SectionId is { } section_id)
                    query = query.Where(product => product.SectionId == section_id);

                if (productFilter?.BrandId is { } brand_id)
                    query = query.Where(product => product.BrandId == brand_id);
            }

            var total_count = query.Count();

            //if (productFilter?.PageSize > 0)
            //    query = query
            //        .Skip((productFilter.Page - 1) * (int)productFilter.PageSize)
            //        .Take((int)productFilter.PageSize);

            if (productFilter is { PageSize: > 0 and var page_size, Page: > 0 and var page_number })
                query = query
                   .Skip((page_number - 1) * page_size)
                   .Take(page_size);

            return new PageProductsDTO(query.AsEnumerable().ToDTO(), total_count);
        }

        public ProductDTO GetProductById(int id) => _db.Products
            .Include(p => p.Section)
            .Include(p => p.Brand)
            .FirstOrDefault(p => p.Id == id)
            .ToDTO();

        public SectionDTO GetSectionById(int id) => _db.Sections
            .Include(s => s.Products)
            .FirstOrDefault(s=>s.Id==id)
            .ToDTO();

        public BrandDTO GetBrandById(int id) => _db.Brands
            .Include(b => b.Products)
            .FirstOrDefault(b => b.Id == id)
            .ToDTO();
    }
}
