using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using WebStore.Domain.ViewModels;

namespace WebStore.TagHelpers
{
    public class Paging : TagHelper
    {
        //private readonly IUrlHelperFactory _urlHelperFactory;

        [ViewContext, HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }

        public PageViewModel PageViewModel { get; set; }

        public string PageAction { get; set; }

        [HtmlAttributeName(DictionaryAttributePrefix ="page-url-")]
        public Dictionary<string, object> PageUrlValues { get; set; } = new(StringComparer.OrdinalIgnoreCase);

        //public Paging(IUrlHelperFactory urlHelperFactory) => _urlHelperFactory = urlHelperFactory;

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var ul = new TagBuilder("ul");
            ul.AddCssClass("pagination");

            //var url_helper = _urlHelperFactory.GetUrlHelper(ViewContext);

            for (var i = 1; i <= PageViewModel.TotalPages; i++)
            {
                ul.InnerHtml.AppendHtml(CreateElement(i));
            }

            output.Content.AppendHtml(ul);
        }

        private TagBuilder CreateElement(int pageNumber)
        {
            var li = new TagBuilder("li");
            var a = new TagBuilder("a");

            if (pageNumber == PageViewModel.Page)
            {
                a.MergeAttribute("data-page", pageNumber.ToString());
                li.AddCssClass("active");
            }
            else
            {
                PageUrlValues["page"] = pageNumber;
                //a.Attributes["href"] = url.Action(PageAction, PageUrlValues);
                a.Attributes["href"] = "#";
                foreach (var (key, value) in PageUrlValues.Where(v => v.Value is { }))
                    a.MergeAttribute($"data-{key}", value.ToString());
            }

            a.InnerHtml.AppendHtml(pageNumber.ToString());
            li.InnerHtml.AppendHtml(a);

            return li;
        }
    }
}
