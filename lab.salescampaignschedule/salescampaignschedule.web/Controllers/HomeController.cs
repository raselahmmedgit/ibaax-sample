using System.Net.Mail;
using salescampaignschedule.web.Helpers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace salescampaignschedule.web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SendEmail()
        {
            try
            {
                string aWSAccessKey = ConfigurationManager.AppSettings["AWSAccessKey"];
                string aWSSecretKey = ConfigurationManager.AppSettings["AWSSecretKey"];
                AmazonSESWrapper amazonSESWrapper = new AmazonSESWrapper(aWSAccessKey, aWSSecretKey);
                List<string> to = new List<string>() { "raselahmmed@hotmail.com", "raselahmmed@gmail.com" };
                List<string> cc = new List<string>() { "rasel.bappi@gmail.com" };
                List<string> bcc = new List<string>() { "rasel.bappi@hotmail.com" };
                string senderEmailAddress = "rasel.ibaax@outlook.com";
                string replyToEmailAddress = "rasel.ibaax@gmail.com";
                string subject = "What is your subject?";
                string body = "This is my body...";
                AmazonSentEmailResult result = amazonSESWrapper.SendEmail(to, cc, bcc, senderEmailAddress, replyToEmailAddress, subject, body);

            }
            catch (Exception ex)
            {
                LoggerHelper.Error(ex);
            }

            
            return View();
        }

        public ActionResult SendEmailNew()
        {
            try
            {
                string aWSAccessKey = ConfigurationManager.AppSettings["AWSAccessKey"];
                string aWSSecretKey = ConfigurationManager.AppSettings["AWSSecretKey"];
                //Create instance for send email...
                AWSEmailSevice emailContaint = new AWSEmailSevice();
                MailMessage emailStuff = new MailMessage();

                //email subject..
                emailStuff.Subject = "Your Email subject";

                //region  Optional email stuff

                //Templates to be used in email / Add your Html template path ..
                emailContaint.Template = @"\Templates\MyUserNotification.html";

                //add file attachment / add your file ...
                emailContaint.AttachedFile = @"\ExcelReport\report.pdf";
                
                //Note :In case of template 
                //if youe want to replace variables in run time 
                //just add replacements like <%FirstName%>  ,  <%OrderNo%> , in HTML Template 


                //if you are using some varibales in template then add 
                // Hold first name..
                var FirstName = "User First Name";

                //  Hold email..
                var OrderNo = 1236;
                
                //firstname replacement..
                emailContaint.Replacements.Add("FirstName", FirstName.ToString());
                emailContaint.Replacements.Add("OrderNo", OrderNo.ToString());

                // endregion option email stuff

            }
            catch (Exception ex)
            {
                LoggerHelper.Error(ex);
            }


            return View();
        }

    }
}