using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Google.GData.Apps;
using Google.GData.Contacts;
using Google.GData.Client;
using Google.GData.Extensions;
using lab.emailimport.Models;
using Google.Contacts;

namespace lab.emailimport.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GMail()
        {
            try
            {
                //string appName = "API Project";
                ////string appName = "api-project-508512642401";
                //string userName = "rasel.ibaax@gmail.com";
                //string password = "rasel123";

                string appName = "My App";
                string userName = "rasel.tester@gmail.com";
                string password = "@@tr9891410";

                RequestSettings requestSettings = new RequestSettings(appName, userName, password);
                requestSettings.AutoPaging = true;
                ContactsRequest contactsRequest = new ContactsRequest(requestSettings);
                Feed<Contact> contactList = contactsRequest.GetContacts();

                List<GmailContact> gmailContactList = new List<GmailContact>();

                foreach (Contact contact in contactList.Entries)
                {
                    foreach (EMail email in contact.Emails)
                    {
                        GmailContact gmailContact = new GmailContact()
                        {
                            EmailId = email.Address.ToString()
                        };

                        gmailContactList.Add(gmailContact);
                    }
                }

                gmailContactList.GroupBy(x => x.EmailId);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return View("Index");
        }
        public ActionResult Hotmail()
        {
            return View("Index");
        }
        public ActionResult Yahoo()
        {
            return View("Index");
        }
        public ActionResult LinkedIn()
        {
            return View("Index");
        }
        public ActionResult Facebook()
        {
            return View("Index");
        }
    }
}