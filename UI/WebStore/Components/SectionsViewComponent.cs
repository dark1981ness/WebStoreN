using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebStore.Interfaces.Services;
using WebStore.Domain.ViewModels;
using WebStore.ViewModels;

namespace WebStore.Components
{
    //[ViewComponent(Name = "Название")]
    public class SectionsViewComponent : ViewComponent
    {
        private readonly IProductData _productData;

        public SectionsViewComponent(IProductData productData) => _productData = productData;

        public IViewComponentResult Invoke(string sectionId)
        {
            var section_id = int.TryParse(sectionId, out var id) ? id : (int?)null;

            var sections = GetSections(section_id, out var parent_section_id);

            ViewBag.SectionId = section_id;
            ViewData["ParentSectionId"] = parent_section_id;

            return View(new SelectableSectionsViewModel()
            {
                Sections = sections,
                SectionId = section_id,
                ParentSectionId = parent_section_id
            });
        }

        private IEnumerable<SectionViewModel> GetSections(int? sectionId, out int? parentSectionId)
        {
            parentSectionId = null;

            var sections = _productData.GetSections();

            var parent_sections = sections.Where(s => s.ParentId is null);

            var parent_sections_views = parent_sections
                .Select(s => new SectionViewModel
                {
                    Id = s.Id,
                    Name = s.Name,
                    Order = s.Order
                })
                .ToList();
            foreach (var parent_section in parent_sections_views)
            {
                var child = sections.Where(s => s.ParentId == parent_section.Id);
                foreach (var child_section in child)
                {
                    if (child_section.Id == sectionId)
                        parentSectionId = child_section.ParentId;

                    parent_section.ChildSections.Add(new SectionViewModel
                    {
                        Id = child_section.Id,
                        Name = child_section.Name,
                        Order = child_section.Order,
                        Parent = parent_section
                    });
                }
                parent_section.ChildSections.Sort((a, b) => Comparer<int>.Default.Compare(a.Order, b.Order));
            }

            parent_sections_views.Sort((a, b) => Comparer<int>.Default.Compare(a.Order, b.Order));

            return parent_sections_views;
        }
    }
}
