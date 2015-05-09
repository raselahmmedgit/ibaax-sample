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
                    string domainUrl = @"http://www.rasel.com";
                    //string domainUrl = @"http://www.hasib.com";
                    //string domainUrl = Request.Url.Scheme + "://" + Request.Url.Authority;

                    if (String.IsNullOrEmpty(pageName))
                    {
                        pageName = "Home";
                        
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

        public ActionResult NotFound()
        {
            return View();
        }
    }
}