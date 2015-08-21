using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using lab.errorlogapps.Models;

namespace lab.errorlogapps.Controllers
{
    public class HomeController : Controller
    {
        readonly log4net.ILog _logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private AppDbContext _db = new AppDbContext();
        public ActionResult Index()
        {
            var categoryList = _db.Category.ToList();
            _logger.Info("Starting GenerateDebugLog Method...");
            _logger.Debug("No input parameters for method.");

            _logger.Info("Starting loop...");
            for (int i = 0; i <= 10; i++)
            {
                _logger.Debug("Current value: " + i);
            }
            _logger.Info("End loop.");

            _logger.Info("End GenerateDebugLog Method.");

            try
            {
                throw new Exception("Exception test!");
            }
            catch (Exception ex)
            {
                _logger.Error("Error! ", ex);
            }

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your apps description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}