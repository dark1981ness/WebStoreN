using System;
using System.Collections.Generic;

namespace WebStore.Domain.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public decimal Price { get; set; }
    }

    public class CatalogViewModel
    {
        public IEnumerable<ProductViewModel> Products { get; set; }

        public int? SectionId { get; set; }

        public int? BrandId { get; set; }

        public PageViewModel PageViewModel { get; set; }
    }

    public class PageViewModel
    {
        public int Page { get; set; }

        public int PageSize { get; set; }

        public int TotalItems { get; set; }

        public int TotalPages => PageSize == 0 ? 0 : (int)Math.Ceiling((double)TotalItems / PageSize);
    }
}
