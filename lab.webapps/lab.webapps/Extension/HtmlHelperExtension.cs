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
        #region Application Title Domain Wise
        public static IHtmlString RenderApplicationTitle(this HtmlHelper htmlHelper)
        {
            try
            {
                string strContent = String.Empty;
                StringBuilder stringBuilder = new StringBuilder();
                HttpContext context = HttpContext.Current;
                var appConstant = (AppConstant)context.Session["AppConstant"];
                AppDbContext db = new AppDbContext();
                if (context != null)
                {
                    var appInfo = db.WebSiteInfo.FirstOrDefault(x => x.WebSiteDomainId == appConstant.DomainId);

                    if (appInfo != null)
                        stringBuilder.Append(appInfo.Title);

                    strContent = stringBuilder.ToString();
                }

                return MvcHtmlString.Create(strContent);
            }
            catch (Exception ex)
            {
                throw new Exception("Render Title Error", ex);
            }

        }

        #endregion

        #region Application Name Domain Wise
        public static IHtmlString RenderApplicationName(this HtmlHelper htmlHelper)
        {
            try
            {
                string strContent = String.Empty;
                StringBuilder stringBuilder = new StringBuilder();
                HttpContext context = HttpContext.Current;
                var appConstant = (AppConstant)context.Session["AppConstant"];
                AppDbContext db = new AppDbContext();
                if (context != null)
                {
                    var appInfo = db.WebSiteInfo.FirstOrDefault(x => x.WebSiteDomainId == appConstant.DomainId);

                    if (appInfo != null)
                        stringBuilder.Append(@"<a href='/" + appInfo.Name + "' class='navbar-brand'>" + appInfo.Name + "</a></li>");

                    strContent = stringBuilder.ToString();
                }

                return MvcHtmlString.Create(strContent);
            }
            catch (Exception ex)
            {
                throw new Exception("Render Name Error", ex);
            }

        }

        #endregion

        #region Application Footer Domain Wise
        public static IHtmlString RenderApplicationFooter(this HtmlHelper htmlHelper)
        {
            try
            {
                string strContent = String.Empty;
                StringBuilder stringBuilder = new StringBuilder();
                HttpContext context = HttpContext.Current;
                var appConstant = (AppConstant)context.Session["AppConstant"];
                AppDbContext db = new AppDbContext();
                if (context != null)
                {
                    var appInfo = db.WebSiteInfo.FirstOrDefault(x => x.WebSiteDomainId == appConstant.DomainId);

                    if (appInfo != null)
                        stringBuilder.Append(appInfo.Footer);

                    strContent = stringBuilder.ToString();
                }

                return MvcHtmlString.Create(strContent);
            }
            catch (Exception ex)
            {
                throw new Exception("Render Footer Error", ex);
            }

        }

        #endregion

        #region Application Menu Domain Wise
        public static IHtmlString RenderApplicationMenu(this HtmlHelper htmlHelper)
        {
            try
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
            catch (Exception ex)
            {
                throw new Exception("Render Menu Error", ex);
            }
            
        }

        #endregion
    }
}