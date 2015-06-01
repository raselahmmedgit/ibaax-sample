using System.Configuration;
using System.IO;
using System.Reflection;
using Amazon;
using Amazon.SimpleEmail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using Amazon.SimpleEmail.Model;

namespace salescampaignschedule.web.Helpers
{
    public class AWSEmailSevice
    {

        //create smtp client instance...
        SmtpClient smtpClient = new SmtpClient();

        //for sent mail notification...
        bool _isMailSent = false;

        //Attached file path...
        public string AttachedFile = string.Empty;

        //HTML Template used in mail ...
        public string Template = string.Empty;

        //hold the final template data list of users...
        public string _finalTemplate = string.Empty;

        //Template replacements varibales dictionary....
        public Dictionary<string, string> Replacements = new Dictionary<string, string>();


        public bool SendMail(MailMessage mailMessage)
        {
            try
            {

                if (mailMessage != null)
                {
                    //code for fixed things
                    //from address...
                    mailMessage.From = new MailAddress("from@gmail.com");

                    //set priority high
                    mailMessage.Priority = System.Net.Mail.MailPriority.High;

                    //Allow html true..
                    mailMessage.IsBodyHtml = true;

                    //Set attachment data..
                    if (!string.IsNullOrEmpty(AttachedFile))
                    {
                        //clear old attachment..
                        mailMessage.Attachments.Clear();

                        Attachment atchFile = new Attachment(AttachedFile);
                        mailMessage.Attachments.Add(atchFile);
                    }

                    //Read email template data ...
                    if (!string.IsNullOrEmpty(Template))
                        _finalTemplate = File.ReadAllText(Template);

                    //check replacements ...
                    if (Replacements.Count > 0)
                    {
                        //exception attached template..
                        if (string.IsNullOrEmpty(_finalTemplate))
                        {
                            throw new Exception("Set Template field (i.e. file path) while using replacement field");
                        }

                        foreach (var item in Replacements)
                        {
                            //Replace Required Variables...
                            _finalTemplate = _finalTemplate.Replace("<%" + item.Key.ToString() + "%>", item.Value.ToString());
                        }
                    }

                    //Set template...
                    mailMessage.Body = _finalTemplate;


                    //Send Email Using AWS SES...
                    var message = mailMessage;
                    var stream = FromMailMessageToMemoryStream(message);
                    using (AmazonSimpleEmailServiceClient client = new AmazonSimpleEmailServiceClient(
                               ConfigurationManager.AppSettings["AWSAccessKey"].ToString(),
                               ConfigurationManager.AppSettings["AWSSecretKey"].ToString(),
                               RegionEndpoint.USWest2))
                    {
                        var sendRequest = new SendRawEmailRequest { RawMessage = new RawMessage { Data = stream } };
                        var response = client.SendRawEmail(sendRequest);

                        //return true ...
                        _isMailSent = true;

                    }
                }
                else
                {
                    _isMailSent = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return _isMailSent;
        }

        private MemoryStream FromMailMessageToMemoryStream(MailMessage message)
        {
            Assembly assembly = typeof(SmtpClient).Assembly;

            Type mailWriterType = assembly.GetType("System.Net.Mail.MailWriter");

            MemoryStream stream = new MemoryStream();

            ConstructorInfo mailWriterContructor =
               mailWriterType.GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, null, new[] { typeof(Stream) }, null);
            object mailWriter = mailWriterContructor.Invoke(new object[] { stream });

            MethodInfo sendMethod =
               typeof(MailMessage).GetMethod("Send", BindingFlags.Instance | BindingFlags.NonPublic);

            if (sendMethod.GetParameters().Length == 3)
            {
                sendMethod.Invoke(message, BindingFlags.Instance | BindingFlags.NonPublic, null, new[] { mailWriter, true, true }, null); // .NET 4.x
            }
            else
            {
                sendMethod.Invoke(message, BindingFlags.Instance | BindingFlags.NonPublic, null, new[] { mailWriter, true }, null); // .NET < 4.0 
            }

            MethodInfo closeMethod =
               mailWriter.GetType().GetMethod("Close", BindingFlags.Instance | BindingFlags.NonPublic);
            closeMethod.Invoke(mailWriter, BindingFlags.Instance | BindingFlags.NonPublic, null, new object[] { }, null);

            return stream;
        }
    }
}