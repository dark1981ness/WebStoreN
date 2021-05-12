using System.Collections.Generic;
using WebStore.Domain.Entities;
using WebStore.Domain;
using WebStore.Domain.DTO;

namespace WebStore.Interfaces.Services
{
    public interface IProductData
    {
        IEnumerable<SectionDTO> GetSections();

        SectionDTO GetSectionById(int id);

        IEnumerable<BrandDTO> GetBrands();

        BrandDTO GetBrandById(int id);

        PageProductsDTO GetProducts(ProductFilter productFilter = null);

        ProductDTO GetProductById(int id);
    }
}
