using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using SportStore.Models.ViewModels;

namespace SportStore.Infrastructure
{
    [HtmlTargetElement("div", Attributes = "page-model")]
    public class PageLinkTagHelper : TagHelper
    {
        private readonly IUrlHelperFactory urlHelperFactory;

        [HtmlAttributeName(DictionaryAttributePrefix = "page-url-")]
        public Dictionary<string, object> PageUrlValues { get; set; } = new();

        public PageLinkTagHelper(IUrlHelperFactory helperFactory) => this.urlHelperFactory =
            helperFactory ??
            throw new ArgumentNullException(nameof(helperFactory), "URL helping factory can't be null");

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }
        public PagingInfo PageModel { get; set; }
        public string PageController { get; set; }
        public string PageAction { get; set; }
        public bool PageClassesEnabled { get; set; }
        public string PageClasses { get; set; }
        public string PageClassesNormal { get; set; }
        public string PageClassesSelected { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);
            var result = new TagBuilder("div");

            for (int index = 1; index <= PageModel.TotalPages; index++)
            {
                TagBuilder tag = new TagBuilder("a");
                PageUrlValues["productPage"] = index;

                tag.Attributes["href"] = urlHelper.Action(action: PageAction,
                    PageUrlValues);

                if (PageClassesEnabled)
                {
                    tag.AddCssClass(PageClasses);
                    tag.AddCssClass(index == PageModel.CurrentPage ? PageClassesSelected : PageClassesNormal);
                }

                tag.InnerHtml.Append($"{index} ");
                result.InnerHtml.AppendHtml(tag);
            }

            output.Content.AppendHtml(result.InnerHtml);
        }
    }
}
