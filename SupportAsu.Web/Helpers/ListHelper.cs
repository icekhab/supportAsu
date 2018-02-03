using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SupportAsu.Web.Helpers
{
    public static class ListHelper
    {
        public static MvcHtmlString CreateList(this HtmlHelper html, SelectList items)
        {
            TagBuilder ul = new TagBuilder("ul");
            ul.AddCssClass("sa-list");
            foreach (SelectListItem item  in items.Items)
            {
                TagBuilder li = new TagBuilder("li");
                li.MergeAttribute("value", item.Value);
                li.SetInnerText(item.Text);
                if(item.Selected)
                {
                    li.AddCssClass("checked");
                }
                ul.InnerHtml += li.ToString();
            }
            return new MvcHtmlString(ul.ToString());
        }
    }
}