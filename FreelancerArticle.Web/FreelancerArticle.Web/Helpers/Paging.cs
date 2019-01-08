using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace FreelancerArticle.Web.Helpers
{
    public static class Paging
    {
        public static MvcHtmlString PagingNavigator(this HtmlHelper helper, int pageNum, int itemsCount, int pageSize)
        {
            StringBuilder sb = new StringBuilder();
            if (pageNum > 0)
                sb.Append(helper.ActionLink("<-----", "Index", new { pageNum = pageNum - 1 }));
            else
                sb.Append(HttpUtility.HtmlEncode("<-----"));
            sb.Append("&emsp;&emsp;");
            int pagesCount = (int)Math.Ceiling((double)itemsCount / pageSize);
            if (pageNum < pagesCount - 1)
                sb.Append(helper.ActionLink("----->", "Index", new { pageNum = pageNum + 1 }));
            else
                sb.Append(HttpUtility.HtmlEncode("----->"));
            return MvcHtmlString.Create(sb.ToString());
        }
    }
}