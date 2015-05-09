using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading;
using System.Web.Mvc;

namespace lab.webapps
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class InitializeAuthorizAttribute : ActionFilterAttribute
    {
        //private static SimpleMembershipInitializer _initializer;
        //private static object _initializerLock = new object();
        //private static bool _isInitialized;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //// Ensure ASP.NET Simple Membership is initialized only once per app start
            //LazyInitializer.EnsureInitialized(ref _initializer, ref _isInitialized, ref _initializerLock);

            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                string strIsAjaxRequest = "It is Ajax Request.";

                filterContext.Result = new JsonResult
                {
                    Data = new
                    {
                        // put whatever data you want which will be sent
                        // to the client
                        message = "Sorry, you are not logged user."
                    },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            else
            {
                string strIsRequest = "It is Request.";
            }
        }

        private class SimpleMembershipInitializer
        {
            public SimpleMembershipInitializer()
            {
                
            }
        }

    }
}