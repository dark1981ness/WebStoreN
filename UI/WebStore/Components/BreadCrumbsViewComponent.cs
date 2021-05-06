using Microsoft.AspNetCore.Mvc;
using WebStore.Interfaces.Services;
using WebStore.Services.Mapping;
using WebStore.ViewModels;

namespace WebStore.Components
{
    public class BreadCrumbsViewComponent : ViewComponent
    {
        private readonly IProductData _productData;

        public BreadCrumbsViewComponent(IProductData productData) => _productData = productData;

        public IViewComponentResult Invoke()
        {

            var model = new BreadCrumbsViewModel();

            if (int.TryParse(Request.Query["SectionId"], out var section_id))
            {
                model.Section = _productData.GetSectionById(section_id).FromDTO();
                if (model.Section?.ParentId != null)
                    model.Section.Parent = _productData.GetSectionById((int)model.Section.ParentId).FromDTO();
            }

            if (int.TryParse(Request.Query["BrandId"], out var brand_id))
                model.Brand = _productData.GetBrandById(brand_id).FromDTO();

            if (int.TryParse(ViewContext.RouteData.Values["id"]?.ToString(), out var product_id))
                model.Product = _productData.GetProductById(product_id)?.Name;

            return View(model);
        }
    }
}
