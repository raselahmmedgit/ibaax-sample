using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace lab.webapps.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            //if (Request.Url != null)
            //{
            //    string domain = Request.Url.Scheme + System.Uri.SchemeDelimiter + Request.Url.Host + (Request.Url.IsDefaultPort ? "" : ":" + Request.Url.Port);
            //}

            return View();
        }

        public ActionResult Error()
        {
            return View("_Error");
        }

    }
}