using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using lab.exportfile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace lab.exportfile.Controllers
{
    public class GridController : Controller
    {
        AppDbContext _db = new AppDbContext();

        // GET: Grid
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult EditingInline_Read([DataSourceRequest] DataSourceRequest request)
        {
            var categoryList = _db.Category.ToList();
            return Json(categoryList.ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingInline_Create([DataSourceRequest] DataSourceRequest request, Category category)
        {
            if (category != null && ModelState.IsValid)
            {
                _db.Category.Add(category);
                _db.SaveChanges();
            }

            return Json(new[] { category }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingInline_Update([DataSourceRequest] DataSourceRequest request, Category category)
        {
            if (category != null && ModelState.IsValid)
            {
                _db.Entry(category).State = System.Data.Entity.EntityState.Modified;
                _db.SaveChanges();
            }

            return Json(new[] { category }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingInline_Destroy([DataSourceRequest] DataSourceRequest request, Category category)
        {
            if (category != null)
            {
                _db.Category.Remove(category);
                _db.SaveChanges();
            }

            return Json(new[] { category }.ToDataSourceResult(request, ModelState));
        }
    }
}