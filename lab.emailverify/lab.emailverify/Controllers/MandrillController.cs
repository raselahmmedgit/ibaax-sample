using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using lab.emailverify.Helpers;
using lab.emailverify.ViewModels;

namespace lab.emailverify.Controllers
{
    public class MandrillController : Controller
    {
        // GET: Mandrill
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SendEmailAddress()
        {
            var mandrilResultViewModel = new MandrilResultViewModel();
            var result = MandrillHelper.SendEmail();
            //var result2 = MandrillHelper.SendEmailWithImage();
            //var result = MandrillHelper.SendEmailWithAttach();

            if (result.Count > 0)
            {
                mandrilResultViewModel.SuccessMessage = "Email send successfully.";
            }
            else
            {
                mandrilResultViewModel.ErrorMessage = "Email do not send successfully.";
            }

            if (mandrilResultViewModel.ErrorMessage != null && !String.IsNullOrEmpty(mandrilResultViewModel.ErrorMessage))
            {
                return Json(new { success = false, data = mandrilResultViewModel.ErrorMessage });
            }
            else
            {
                return Json(new { success = true, data = mandrilResultViewModel.SuccessMessage });
            }

        }

        [HttpPost]
        public ActionResult SendEmailAddressAjax(string emailAddress)
        {
            var mandrilResultViewModel = new MandrilResultViewModel();
            var result = MandrillHelper.SendEmail(emailAddress, "Rasel", "Ahmmed");

            if (result.Count > 0)
            {
                mandrilResultViewModel.SuccessMessage = "Email send successfully.";
            }
            else
            {
                mandrilResultViewModel.ErrorMessage = "Email do not send successfully.";
            }

            if (mandrilResultViewModel.ErrorMessage != null && !String.IsNullOrEmpty(mandrilResultViewModel.ErrorMessage))
            {
                return Json(new { success = false, data = mandrilResultViewModel.ErrorMessage });
            }
            else
            {
                return Json(new { success = true, data = mandrilResultViewModel.SuccessMessage, successData = this.RenderPartialViewToString("_VerificationReport", mandrilResultViewModel) });
            }

        }
    }
}