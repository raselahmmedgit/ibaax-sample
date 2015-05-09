using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using lab.webapps.Models;

namespace lab.webapps
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            InitializeAndSeedDb();
        }
        protected void Application_BeginRequest(Object sender, EventArgs e)
        {
            //HttpApplication httpApplication = (HttpApplication)sender;
            //HttpContext context = httpApplication.Context;

            //if (context != null)
            //{
            //    if (context.Session == null)
            //    {
            //        //if (context.Session == null && context.Session["AppConstant"] == null)
            //        //{
            //        //}

            //        string domainUrl = @"http://www.rasel.com";
            //        string baseUrl = @"http://www.rasel.com";
            //        //string domainUrl = @"http://www.hasib.com";
            //        //string baseUrl = @"http://www.hasib.com";

            //        //string domainUrl = context.Request.Url.Scheme + System.Uri.SchemeDelimiter + context.Request.Url.Host + (context.Request.Url.IsDefaultPort ? "" : ":" + context.Request.Url.Port);
            //        //string baseUrl = context.Request.Url.Scheme + "://" + context.Request.Url.Authority;
                    
            //        string serverPath = System.Web.HttpContext.Current.Server.MapPath("~/");
            //        AppDbContext db = new AppDbContext();
            //        var domain = db.WebSiteDomain.FirstOrDefault(x => x.Url == domainUrl);

            //        if (domain != null)
            //        {
            //            AppConstant appConstant = new AppConstant() { BaseUrl = baseUrl, ServerPath = serverPath, DomainName = domain.Name, DomainUrl = domain.Url, DomainId = domain.WebSiteDomainId };

            //            HttpContext.Current.Session["AppConstant"] = appConstant;
            //        }
            //    }
            //}
        }
        protected void Application_EndRequest(Object sender, EventArgs e)
        {
        }
        protected void Application_Error(Object sender, EventArgs e)
        {
        }
        protected void Application_End()
        {
        }

        #region Method
        private static void InitializeAndSeedDb()
        {
            try
            {
                // Initializes and seeds the database.
                Database.SetInitializer(new DBInitializer());

                using (var context = new AppDbContext())
                {
                    if (!context.Database.Exists())
                    {
                        context.Database.Initialize(force: true);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        #endregion

    }

}
