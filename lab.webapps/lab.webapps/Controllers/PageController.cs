using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using lab.webapps.Models;

namespace lab.webapps.Controllers
{
    public class PageController : BaseController
    {
        AppDbContext _db = new AppDbContext();
        // GET: Page
        [HttpGet]
        public ActionResult Index(string pageName)
        {
            if (Session["AppConstant"] != null)
            {
                
                if (Request.Url != null)
                {
                    string domainUrl = @"http://www.rebaax.com";
                    //string domainUrl = @"http://www.rebaxcode.com";
                    //string domainUrl = Request.Url.Scheme + "://" + Request.Url.Authority;

                    if (String.IsNullOrEmpty(pageName))
                    {
                        var domain = _db.WebSiteDomain.FirstOrDefault(x => x.Url == domainUrl);

                        if (domain != null)
                        {
                            var pageList = _db.WebSitePage.ToList().Where(x => x.WebSiteDomainId == domain.WebSiteDomainId).ToList();
                            var defaultPage = pageList.FirstOrDefault(x => x.IsDefault);
                            if (defaultPage != null) pageName = defaultPage.Name;

                            string strPageList = String.Empty;

                            foreach (var page in pageList)
                            {
                                strPageList += page.Name + " ";
                            }

                            string strPageName = String.Empty;

                            var firstOrDefault = pageList.FirstOrDefault(x => x.Name == pageName);
                            if (firstOrDefault != null)
                            {
                                strPageName = firstOrDefault.Name;
                            }

                            ViewBag.DomainName = "This is " + domain.Name;
                            ViewBag.DomainUrl = "My domain is " + domain.Url;
                            ViewBag.PageName = "My current page is " + strPageName;
                            ViewBag.PageList = "My pages are " + strPageList;
                        }
                        else
                        {
                            return RedirectToAction("NotFound");
                        }
                    }
                    else
                    {
                    
                        var domain = _db.WebSiteDomain.FirstOrDefault(x => x.Url == domainUrl);

                        if (domain != null)
                        {
                            var pageList = _db.WebSitePage.ToList().Where(x => x.WebSiteDomainId == domain.WebSiteDomainId).ToList();

                            string strPageList = String.Empty;

                            foreach (var page in pageList)
                            {
                                strPageList += page.Name + " ";
                            }

                            string strPageName = String.Empty;

                            var firstOrDefault = pageList.FirstOrDefault(x => x.Name == pageName);
                            if (firstOrDefault != null)
                            {
                                strPageName = firstOrDefault.Name;
                            }

                            ViewBag.DomainName = "This is " + domain.Name;
                            ViewBag.DomainUrl = "My domain is " + domain.Url;
                            ViewBag.PageName = "My current page is " + strPageName;
                            ViewBag.PageList = "My pages are " + strPageList;
                        }
                        else
                        {
                            return RedirectToAction("NotFound");
                        }
                    }
                }
            }
            else
            {
                return RedirectToAction("NotFound");
            }

            return View();
        }

        //public ActionResult Data(string pageName, int id)
        public ActionResult Data(int id)
        {
            string pageName = string.Empty;
            var loggedUser = (User)Session["User"];

            if (Session["AppConstant"] != null)
            {

                if (Request.Url != null)
                {
                    string domainUrl = @"http://www.rebaax.com";
                    //string domainUrl = @"http://www.rebaxcode.com";
                    //string domainUrl = Request.Url.Scheme + "://" + Request.Url.Authority;

                    if (String.IsNullOrEmpty(pageName))
                    {
                        var domain = _db.WebSiteDomain.FirstOrDefault(x => x.Url == domainUrl);

                        if (domain != null)
                        {
                            ViewBag.DomainName = "This is " + domain.Name;
                            ViewBag.DomainUrl = "My domain is " + domain.Url;
                            ViewBag.PropertyName = "My Propert " + id;
                            ViewBag.PropertyRate = "My Property Rate " + (id + 1000);
                        }
                        else
                        {
                            return RedirectToAction("NotFound");
                        }
                    }
                    else
                    {

                        var domain = _db.WebSiteDomain.FirstOrDefault(x => x.Url == domainUrl);

                        if (domain != null)
                        {
                            ViewBag.DomainName = "This is " + domain.Name;
                            ViewBag.DomainUrl = "My domain is " + domain.Url;
                            ViewBag.PropertyName = "My Propert " + id;
                            ViewBag.PropertyRate = "My Property Rate " + (id + 1000);
                        }
                        else
                        {
                            return RedirectToAction("NotFound");
                        }
                    }
                }
            }
            else
            {
                return RedirectToAction("NotFound");
            }

            return View();
        }

        public ActionResult GetProperty(int id)
        {
            string pageName = string.Empty;
            var loggedUser = (User)Session["User"];

            if (Session["AppConstant"] != null)
            {

                if (Request.Url != null)
                {
                    string domainUrl = @"http://www.rebaax.com";
                    //string domainUrl = @"http://www.rebaxcode.com";
                    //string domainUrl = Request.Url.Scheme + "://" + Request.Url.Authority;

                    if (String.IsNullOrEmpty(pageName))
                    {
                        var domain = _db.WebSiteDomain.FirstOrDefault(x => x.Url == domainUrl);

                        if (domain != null)
                        {
                            ViewBag.DomainName = "This is " + domain.Name;
                            ViewBag.DomainUrl = "My domain is " + domain.Url;
                            ViewBag.PropertyName = "My Property " + id;
                            ViewBag.PropertyRate = "My Property Rate " + (id + 1000);
                        }
                        else
                        {
                            return RedirectToAction("NotFound");
                        }
                    }
                    else
                    {

                        var domain = _db.WebSiteDomain.FirstOrDefault(x => x.Url == domainUrl);

                        if (domain != null)
                        {
                            ViewBag.DomainName = "This is " + domain.Name;
                            ViewBag.DomainUrl = "My domain is " + domain.Url;
                            ViewBag.PropertyName = "My Property " + id;
                            ViewBag.PropertyRate = "My Property Rate " + (id + 1000);
                        }
                        else
                        {
                            return RedirectToAction("NotFound");
                        }
                    }
                }
            }
            else
            {
                return RedirectToAction("NotFound");
            }

            return View("Property");
        }

        public ActionResult GetAgent(int id)
        {
            string pageName = string.Empty;
            var loggedUser = (User)Session["User"];

            if (Session["AppConstant"] != null)
            {

                if (Request.Url != null)
                {
                    string domainUrl = @"http://www.rebaax.com";
                    //string domainUrl = @"http://www.rebaxcode.com";
                    //string domainUrl = Request.Url.Scheme + "://" + Request.Url.Authority;

                    if (String.IsNullOrEmpty(pageName))
                    {
                        var domain = _db.WebSiteDomain.FirstOrDefault(x => x.Url == domainUrl);

                        if (domain != null)
                        {
                            ViewBag.DomainName = "This is " + domain.Name;
                            ViewBag.DomainUrl = "My domain is " + domain.Url;
                            ViewBag.AgentName = "My Agent id " + id;
                        }
                        else
                        {
                            return RedirectToAction("NotFound");
                        }
                    }
                    else
                    {

                        var domain = _db.WebSiteDomain.FirstOrDefault(x => x.Url == domainUrl);

                        if (domain != null)
                        {
                            ViewBag.DomainName = "This is " + domain.Name;
                            ViewBag.DomainUrl = "My domain is " + domain.Url;
                            ViewBag.AgentName = "My Agent id " + id;
                        }
                        else
                        {
                            return RedirectToAction("NotFound");
                        }
                    }
                }
            }
            else
            {
                return RedirectToAction("NotFound");
            }

            return View("Agent");
        }

        public ActionResult NotFound()
        {
            return View();
        }
    }
}