using Microsoft.AspNetCore.Mvc;
using SimpleMvcSitemap;
using System.Collections.Generic;
using System.Linq;
using WebStore.Interfaces.Services;

namespace WebStore.Controllers.API
{
    public class SiteMap : ControllerBase
    {
        public IActionResult Index([FromServices] IProductData ProductData)
        {
            var nodes = new List<SitemapNode>
            {
                new (Url.Action("Index", "Home")),
                new (Url.Action("SecondAction", "Home")),
                new (Url.Action("Index", "Catalog")),
                new (Url.Action("Index", "WebAPI")),
            };

            nodes.AddRange(ProductData.GetSections().Select(s => new SitemapNode(Url.Action("Index", "Catalog", new { SectionId = s.Id }))));

            foreach (var brand in ProductData.GetBrands())
                nodes.Add(new SitemapNode(Url.Action("Index", "Catalog", new { BrandId = brand.Id })));

            foreach (var product in ProductData.GetProducts().Products)
                nodes.Add(new SitemapNode(Url.Action("Index", "Catalog", new { product.Id })));

            return new SitemapProvider().CreateSitemap(new SitemapModel(nodes));
        }
    }
}
