using System;
using System.Collections.Generic;
using System.Configuration;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Net;
using log4net;
using System.Net.Http;
using EasyHttp.Http;
using MailChimp;
using MailChimp.Types;
using System.IO;

namespace lab.emailverify.Helpers
{
    public static class MandrillHelper
    {
        public static MVList<Mandrill.Messages.SendResult> SendEmail()
        {
            var mandrillApi = new MandrillApi(SiteConfigurationReader.MandrillApiKey);

            MailChimp.Types.Mandrill.UserInfo info = mandrillApi.UserInfo();

            var recipients = new List<Mandrill.Messages.Recipient>();

            var name = string.Format("{0} {1}", "Rasel", "Ahmmed");

            recipients.Add(new Mandrill.Messages.Recipient("raselahmmed@gmail.com", name));
            //recipients.Add(new Mandrill.Messages.Recipient("rasel.bappi@gmail.com", "Rasel Bappi"));

            var message = new Mandrill.Messages.Message()
            {
                To = recipients.ToArray(),
                FromEmail = "rasel@ibaax.com",
                Subject = "Mandrill: Test Email Subject",
                Html = "<div>Dear Sir,<br/> This is email body.<br/> Happy Programing</div>",
                Text = "This is test."
            };

            GetFileType("raselahmmed.jpg");

            GetFileType("raselahmmed.pdf");

            GetFileType("raselahmmed.rar");

            MVList<Mandrill.Messages.SendResult> result;
            //result = api.Send(message);
            result = mandrillApi.Send(message);
            return result;
        }

        public static MVList<Mandrill.Messages.SendResult> SendEmailWithImage()
        {
            var api = new MandrillApi(SiteConfigurationReader.MandrillApiKey);

            MailChimp.Types.Mandrill.UserInfo info = api.UserInfo();

            var recipients = new List<Mandrill.Messages.Recipient>();

            var name = string.Format("{0} {1}", "Rasel", "Ahmmed");

            recipients.Add(new Mandrill.Messages.Recipient("raselahmmed@gmail.com", name));
            //recipients.Add(new Mandrill.Messages.Recipient("rasel.bappi@gmail.com", "Rasel Bappi"));

            var file = File.ReadAllBytes(@"C:\Users\user\Documents\GitHub\ibaax-sample\lab.emailverify\lab.emailverify\Download\raselahmmed.jpg");
            var base64 = Convert.ToBase64String(file, 0, file.Length);

            MCNull<bool> mcNull = new MCNull<bool>(true);

            //var images = new[]
            //{
            //    new Mandrill.Messages.Attachment("image/jpg", "raselahmmed.jpg", mcNull, Convert.ToBase64String(imageBytes))
            //};

            var images = new[]
            {
                new Mandrill.Messages.Attachment("image/jpg", "raselahmmed", new MCNull<bool>(true), base64)
            };
            
            var message = new Mandrill.Messages.Message()
            {
                To = recipients.ToArray(),
                FromEmail = "rasel@ibaax.com",
                Subject = "Mandrill: Test Email Subject",
                Html = "<div>Dear Sir,<br/> This is email body.<br/><img src=\"cid:raselahmmed\"/> Happy Programing</div>",
                Text = "This is test.",
                Images = images
            };
            MVList<Mandrill.Messages.SendResult> result;
            //result = api.Send(message);
            result = api.Send(message);
            return result;
        }

        public static string GetFileType(string documentFileName)
        {
            char[] arrDocumentFileName = documentFileName.ToCharArray();
            Array.Reverse(arrDocumentFileName);

            string strReverseDocumentFileName = new string(arrDocumentFileName);

            var strReverseDocumentExtension = Convert.ToString(strReverseDocumentFileName.Split('.')[0]);

            char[] arrDocumentExtension = strReverseDocumentExtension.ToCharArray();
            Array.Reverse(arrDocumentExtension);

            string documentExtension = new string(arrDocumentExtension);

            string result = string.Empty;

            if (documentExtension == "pdf")
            {
                result = "application/" + documentExtension;
            }
            if (documentExtension == "xls")
            {
                result = "application/" + documentExtension;
            }
            if (documentExtension == "xlsx")
            {
                result = "application/" + documentExtension;
            }
            if (documentExtension == "doc")
            {
                result = "application/" + documentExtension;
            }
            if (documentExtension == "docx")
            {
                result = "application/" + documentExtension;
            }
            if (documentExtension == "ppt")
            {
                result = "application/" + documentExtension;
            }
            if (documentExtension == "pptx")
            {
                result = "application/" + documentExtension;
            }
            if (documentExtension == "jpg" || documentExtension == "png" || documentExtension == "jpeg" || documentExtension == "png")
            {
                result = "image/" + documentExtension;
            }
            if (documentExtension == "txt")
            {
                result = "application/" + documentExtension;
            }
            if (documentExtension == "rar" || documentExtension == "zip")
            {
                result = "application/" + documentExtension;
            }

            return result;

        }

        public static MVList<Mandrill.Messages.SendResult> SendEmailWithAttach()
        {
            var api = new MandrillApi(SiteConfigurationReader.MandrillApiKey);

            MailChimp.Types.Mandrill.UserInfo info = api.UserInfo();

            var recipients = new List<Mandrill.Messages.Recipient>();

            var name = string.Format("{0} {1}", "Rasel", "Ahmmed");

            recipients.Add(new Mandrill.Messages.Recipient("raselahmmed@gmail.com", "Rasel Ahmmed"));
            //recipients.Add(new Mandrill.Messages.Recipient("rasel.bappi@gmail.com", "Rasel Bappi"));

            var imageBytes = File.ReadAllBytes(@"C:\Users\user\Documents\GitHub\ibaax-sample\lab.emailverify\lab.emailverify\Download\raselahmmed.jpg");
            var pdfBytes = File.ReadAllBytes(@"C:\Users\user\Documents\GitHub\ibaax-sample\lab.emailverify\lab.emailverify\Download\raselahmmed.pdf");

            MCNull<bool> mcNull = new MCNull<bool>(true);

            //Mandrill.Messages.Attachment imgAttach = new Mandrill.Messages.Attachment("image/jpg", "raselahmmed.jpg", mcNull, Convert.ToBase64String(imageBytes));
            //Mandrill.Messages.Attachment pdfAttach = new Mandrill.Messages.Attachment("application/pdf", "raselahmmed.pdf", mcNull, Convert.ToBase64String(pdfBytes));

            var attachments = new[]
                              {
                                  new Mandrill.Messages.Attachment("image/jpg", "raselahmmed.jpg", mcNull, Convert.ToBase64String(imageBytes)),
                                  new Mandrill.Messages.Attachment("application/pdf", "raselahmmed.pdf", mcNull, Convert.ToBase64String(pdfBytes))
                              };

            var message = new Mandrill.Messages.Message()
            {
                To = recipients.ToArray(),
                FromEmail = "rasel@ibaax.com",
                Subject = "Mandrill: Test Email Subject",
                Html = "<div>Dear Sir,<br/> This is email body.<br/> Happy Programing</div>",
                Text = "This is test.",
                PreserveRecipients = true,
                Attachments = attachments
            };

            MVList<Mandrill.Messages.SendResult> result;
            //result = api.Send(message);
            result = api.Send(message);
            return result;
        }

        public static MVList<Mandrill.Messages.SendResult> SendEmail(string emailAddress, string firstName, string lastName)
        {
            var api = new MandrillApi(SiteConfigurationReader.MandrillApiKey);
            var recipients = new List<Mandrill.Messages.Recipient>();
            var name = string.Format("{0} {1}", firstName, lastName);
            recipients.Add(new Mandrill.Messages.Recipient(emailAddress, name));
            var message = new Mandrill.Messages.Message()
            {
                To = recipients.ToArray(),
                FromEmail = "rasel@ibaax.com",
                Subject = "Mandrill: Test Email Subject",
                Html = "<div>Dear " + firstName + " " + lastName + ",<br/> This is email body.<br/> Happy Programing</div>"
            };
            MVList<Mandrill.Messages.SendResult> result;
            result = api.Send(message);
            return result;
        }
    }



    #region Sample Code


    //enum MandrillError
    //{
    //    OK,
    //    WebException,
    //    HttpNotOk,
    //    Invalid,
    //    Rejected,
    //    Unknown
    //}

    //public class Mandrill
    //{
    //    static string MandrillBaseUrl = ConfigurationManager.AppSettings["MandrillBaseUrl"];
    //    static Guid MandrillKey = new Guid(ConfigurationManager.AppSettings["MandrillKey"]);

    //    public static bool SendActivationEMail(BLL.TrialSignup ts, out string errorMsg)
    //    {
    //        string activationLink =
    //            HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + "/Register/Activation.aspx?id=" + ts.Id;

    //        //send-template(string key, string template_name, array template_content, struct message) 
    //        dynamic sendParams = new ExpandoObject();
    //        sendParams.key = MandrillKey;
    //        sendParams.template_name = "Secret Project Trial Activation";

    //        sendParams.template_content = new List<dynamic>();

    //        sendParams.message = new ExpandoObject();
    //        sendParams.message.subject = "Here's your Secret Project activation email";
    //        sendParams.message.from_email = "info@SecretProject.com";
    //        sendParams.message.from_name = "Secret Project";

    //        sendParams.message.to = new List<dynamic>();
    //        sendParams.message.to.Add(new ExpandoObject());
    //        sendParams.message.to[0].email = ts.EMail;
    //        sendParams.message.to[0].name = ts.Name;

    //        sendParams.message.track_opens = true;
    //        //sendParams.message.track_clicks = true;

    //        sendParams.message.global_merge_vars = new List<dynamic>();
    //        sendParams.message.global_merge_vars.Add(new ExpandoObject());
    //        sendParams.message.global_merge_vars[0].name = "NAME";
    //        sendParams.message.global_merge_vars[0].content = ts.Name;

    //        sendParams.message.global_merge_vars.Add(new ExpandoObject());
    //        sendParams.message.global_merge_vars[1].name = "LINK";
    //        sendParams.message.global_merge_vars[1].content = activationLink;

    //        errorMsg = string.Empty;

    //        MandrillError merr = SendMessage(sendParams);

    //        switch (merr)
    //        {
    //            case MandrillError.OK:
    //                return true;
    //            case MandrillError.WebException:
    //            case MandrillError.HttpNotOk:
    //                errorMsg = "There was an issue sending your activation e-mail. Please try again later or call us directly.";
    //                break;
    //            case MandrillError.Invalid:
    //                errorMsg = "Your email address appears to be invalid. Please try again with a valid address, or call us directly.";
    //                break;
    //            case MandrillError.Rejected:
    //                errorMsg = "Your activation email was rejected. Please try again with a valid address, or call us directly.";
    //                break;
    //            case MandrillError.Unknown:
    //                errorMsg = "There was an unknown problem sending your activation email. Please try again, or call us directly.";
    //                break;
    //        }
    //        return false;
    //    }

    //    public static bool SendSalesNotification(BLL.TrialSignup ts)
    //    {
    //        dynamic sendParams = new ExpandoObject();
    //        sendParams.key = MandrillKey;
    //        sendParams.template_name = "Secret Project Trial Sales Notification";

    //        sendParams.template_content = new List<dynamic>();

    //        sendParams.message = new ExpandoObject();
    //        sendParams.message.subject = "Secret Project Trial Account Notification";
    //        sendParams.message.from_email = "info@SecretProject.com";
    //        sendParams.message.from_name = "Secret Project";

    //        sendParams.message.to = new List<dynamic>();
    //        sendParams.message.to.Add(new ExpandoObject());
    //        sendParams.message.to[0].email = ConfigurationManager.AppSettings["SalesEmail"];
    //        sendParams.message.to[0].name = "Secret Project Sales";

    //        //sendParams.message.track_opens = true;
    //        //sendParams.message.track_clicks = true;

    //        sendParams.message.global_merge_vars = new List<dynamic>();
    //        sendParams.message.global_merge_vars.Add(new ExpandoObject());
    //        sendParams.message.global_merge_vars[0].name = "NAME";
    //        sendParams.message.global_merge_vars[0].content = ts.Name;

    //        sendParams.message.global_merge_vars.Add(new ExpandoObject());
    //        sendParams.message.global_merge_vars[1].name = "COMPANY";
    //        sendParams.message.global_merge_vars[1].content = ts.CompanyName;

    //        sendParams.message.global_merge_vars.Add(new ExpandoObject());
    //        sendParams.message.global_merge_vars[2].name = "EMAIL";
    //        sendParams.message.global_merge_vars[2].content = ts.EMail;

    //        MandrillError merr = SendMessage(sendParams);

    //        switch (merr)
    //        {
    //            case MandrillError.OK:
    //                return true;
    //            case MandrillError.WebException:
    //            case MandrillError.HttpNotOk:
    //            case MandrillError.Invalid:
    //            case MandrillError.Rejected:
    //            case MandrillError.Unknown:
    //                break;
    //        }
    //        return false;
    //    }

    //    private static MandrillError SendMessage(dynamic sendParams)
    //    {
    //        ILog _log = log4net.LogManager.GetLogger("Mandrill/SendMessage");

    //        string url = MandrillBaseUrl + "/messages/send-template.json";

    //        var http = new HttpClient
    //        {
    //            Request = { Accept = HttpContentTypes.ApplicationJson }
    //        };

    //        EasyHttp.Http.HttpResponse response;
    //        try
    //        {
    //            response = http.Post(url, sendParams, HttpContentTypes.ApplicationJson);
    //        }
    //        catch (WebException ex)
    //        {
    //            _log.ErrorFormat("Error: WebException - {0}", ex.Message);
    //            return MandrillError.WebException;
    //        }

    //        if (response.StatusCode != HttpStatusCode.OK)
    //        {
    //            _log.InfoFormat("Response = {0} - {1}", response.StatusCode, response.StatusDescription);
    //            _log.Info(response.RawText);
    //            return MandrillError.HttpNotOk;
    //        }

    //        dynamic rv = response.DynamicBody;
    //        _log.InfoFormat("email: {0}, status: {1}", rv[0].email, rv[0].status);

    //        string send_status = rv[0].status;
    //        if (send_status == "sent" || send_status == "queued")
    //            return MandrillError.OK;

    //        // otherwise, it should be "rejected" or "invalid"
    //        if (send_status == "invalid")
    //        {
    //            return MandrillError.Invalid;
    //        }
    //        if (send_status == "rejected")
    //        {
    //            return MandrillError.Rejected;
    //        }

    //        // unexpected...
    //        return MandrillError.Unknown;
    //    }

    //}


    #endregion
}