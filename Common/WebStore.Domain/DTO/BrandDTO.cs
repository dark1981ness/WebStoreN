using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebStore.Domain.DTO
{
    public class BrandDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Order { get; set; }
    }

    public class SectionDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Order { get; set; }

        public int? ParentId { get; set; }
    }

    public class ProductDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Order { get; set; }

        public decimal Price { get; set; }

        public string ImageUrl { get; set; }

        public BrandDTO Brand { get; set; }

        public SectionDTO Section { get; set; }
    }
}
