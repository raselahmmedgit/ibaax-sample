using lab.webapps.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace lab.webapps
{
    public static class HtmlHelperExtension
    {
        #region Menu Domain Wise
        public static IHtmlString RenderMenu(this HtmlHelper htmlHelper)
        {
            string strContent = String.Empty;
            StringBuilder stringBuilder = new StringBuilder();
            HttpContext context = HttpContext.Current;
            var appConstant = (AppConstant)context.Session["AppConstant"];
            AppDbContext db = new AppDbContext();
            if (context != null)
            {
                //string domainUrl = context.Request.Url.Scheme + System.Uri.SchemeDelimiter + context.Request.Url.Host + (context.Request.Url.IsDefaultPort ? "" : ":" + context.Request.Url.Port);

                var domain = db.WebSiteDomain.FirstOrDefault(x => x.Url == appConstant.DomainUrl);

                var pageList = db.WebSitePage.ToList().Where(x => domain != null && x.WebSiteDomainId == domain.WebSiteDomainId).ToList();

                foreach (var page in pageList)
                {
                    stringBuilder.Append(@"<li><a href='/" + page.Url + "'>" + page.Name + "</a></li>");
                }

                strContent = stringBuilder.ToString();
            }

            return MvcHtmlString.Create(strContent);
        }

        #endregion
    }
}