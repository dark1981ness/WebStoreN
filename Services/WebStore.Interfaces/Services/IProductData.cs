using System.Collections.Generic;
using WebStore.Domain.Entities;
using WebStore.Domain;

namespace WebStore.Interfaces.Services
{
    public interface IProductData
    {
        IEnumerable<Section> GetSections();
        IEnumerable<Brand> GetBrands();

        IEnumerable<Product> GetProducts(ProductFilter productFilter = null);

        Product GetProductById(int id);
    }
}
