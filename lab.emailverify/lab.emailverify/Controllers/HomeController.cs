using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using lab.emailverify.Helpers;
using lab.emailverify.Models;

namespace lab.emailverify.Controllers
{
    public class HomeController : Controller
    {
        AppDbContext _db = new AppDbContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Cobisi()
        {
            return View();
        }

        //[HttpGet]
        //public ActionResult VerificationEmailAddressAjax(string emailAddress)
        //{
        //    EmailVerification emailVerification = null;

        //    if (!string.IsNullOrWhiteSpace(emailAddress))
        //    {
        //    emailVerification = _db.EmailVerification.FirstOrDefault(item => item.EmailAddress == emailAddress) ?? new EmailVerification() { EmailAddress = emailAddress };
                
        //                if (emailVerification != null && emailVerification.IsVerified == true)
        //                {
        //                    var emailVerificationViewModel = CobisiHelper.GetEmailVerificationReport(emailVerification.EmailAddress);

        //                    emailVerificationViewModel.EmailVerificationId =
        //                        emailVerification.EmailVerificationId;

        //                    return PartialView("_VerificationReport", emailVerificationViewModel);
        //                }

        //            }
        //            else
        //            {
        //                emailVerification.EmailAddress = emailAddress;
        //            }
                

        //        return PartialView("_Verification", emailVerification);
        //}

        [HttpPost]
        public ActionResult VerifyEmailAddressAjax(string emailAddress)
        {

            //var emailVerificationViewModel = CobisiHelper.GetEmailVerificationReport(primaryEmailAddress);
            var emailVerificationViewModel = CobisiHelper.GetEmailVerificationReportForAll(emailAddress);

            if (emailVerificationViewModel.IsPositive)
            {
                emailVerificationViewModel.SuccessMessage = "Email address verified successfully.";
            }
            else
            {
                emailVerificationViewModel.ErrorMessage = "Email address not verified reason: <b>" + emailVerificationViewModel.LastStatus.ToString() + "</b>";
            }

            if (emailVerificationViewModel.ErrorMessage != null && !String.IsNullOrEmpty(emailVerificationViewModel.ErrorMessage))
            {
                return Json(new { success = false, data = emailVerificationViewModel.ErrorMessage });
            }
            else
            {
                return Json(new { success = true, data = emailVerificationViewModel.ErrorMessage, successData = this.RenderPartialViewToString("_VerificationReport", emailVerificationViewModel) });
            }

        }

    }
}