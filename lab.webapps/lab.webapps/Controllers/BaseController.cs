using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using lab.webapps.Models;

namespace lab.webapps.Controllers
{
    public class BaseController : Controller
    {
        AppDbContext _db = new AppDbContext();
        public string _baseUrl { get; set; }
        public string _serverPath { get; set; }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                if (Request.Url != null)
                {
                    string domainUrl = @"http://www.rebaax.com";
                    _baseUrl = @"http://www.rebaax.com";

                    //string domainUrl = @"http://www.rebaxcode.com";
                    //_baseUrl = @"http://www.rebaxcode.com";

                    //string domainUrl = Request.Url.Scheme + System.Uri.SchemeDelimiter + Request.Url.Host + (Request.Url.IsDefaultPort ? "" : ":" + Request.Url.Port);
                    //_baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority;

                    _serverPath = System.Web.HttpContext.Current.Server.MapPath("~/");

                    var domain = _db.WebSiteDomain.FirstOrDefault(x => x.Url == domainUrl);

                    if (domain != null)
                    {
                        AppConstant appConstant = new AppConstant() { BaseUrl = _baseUrl, ServerPath = _serverPath, DomainName = domain.Name, DomainUrl = domain.Url, DomainId = domain.WebSiteDomainId };

                        Session["AppConstant"] = appConstant;
                    }
                }

                base.OnActionExecuting(filterContext);
            }
            catch (Exception ex)
            {
                throw new Exception("OnActionExecuting Error", ex);
            }
            
        }
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            try
            {
                object objCurrentControllerName;
                this.RouteData.Values.TryGetValue("controller", out objCurrentControllerName);
                object objCurrentActionName;
                this.RouteData.Values.TryGetValue("action", out objCurrentActionName);
                string currentAreaName = string.Empty;
                if (this.RouteData.DataTokens.ContainsKey("area"))
                {
                    currentAreaName = this.RouteData.DataTokens["area"].ToString();
                }

                if (objCurrentActionName != null)
                {
                    string currentActionName = objCurrentActionName.ToString();
                    if (objCurrentControllerName != null)
                    {
                        string currentControllerName = objCurrentControllerName.ToString();

                        ViewBag.CurrentActionName = currentActionName;
                        ViewBag.CurrentControllerName = currentControllerName;
                    }
                }
                ViewBag.CurrentAreaName = currentAreaName;
                base.OnActionExecuted(filterContext);
            }
            catch (Exception ex)
            {
                throw new Exception("OnActionExecuted Error", ex);
            }
            
        }
        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            try
            {
                string cultureName = null;

                // Attempt to read the culture cookie from Request
                HttpCookie cultureCookie = Request.Cookies["_culture"];
                if (cultureCookie != null)
                {
                    cultureName = cultureCookie.Value;
                }
                else
                {
                    cultureCookie = Request.Cookies["ibaax.culture"];
                    if (cultureCookie != null)
                    {
                        cultureName = cultureCookie.Value;
                    }
                    else
                    {
                        cultureName = Request.UserLanguages != null && Request.UserLanguages.Length > 0
                            ? Request.UserLanguages[0]
                            : null; // obtain it from HTTP header AcceptLanguages   
                    }
                }

                // Validate culture name
                cultureName = CultureHelper.GetImplementedCulture(cultureName); // This is safe

                // Modify current thread's cultures            
                Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(cultureName);
                Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;

                return base.BeginExecuteCore(callback, state);
            }
            catch (Exception ex)
            {
                throw new Exception("BeginExecuteCore Error", ex);
            }
            
        }

        protected override void HandleUnknownAction(string actionName)
        {
            base.HandleUnknownAction(actionName);
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            // Let other exceptions just go unhandled
            if (filterContext.Exception is InvalidOperationException)
            {
                // Default view is "error"
            }
        }
    }
}