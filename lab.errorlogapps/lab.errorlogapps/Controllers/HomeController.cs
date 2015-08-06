﻿using System;
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
            HttpBrowserCapabilitiesBase browser = Request.Browser;
            string browserInfo = "Browser Capabilities\n"
                + "Type = " + browser.Type + "\n"
                + "Name = " + browser.Browser + "\n"
                + "Version = " + browser.Version + "\n"
                + "Major Version = " + browser.MajorVersion + "\n"
                + "Minor Version = " + browser.MinorVersion + "\n"
                + "Platform = " + browser.Platform + "\n"
                + "Is Beta = " + browser.Beta + "\n"
                + "Is Crawler = " + browser.Crawler + "\n"
                + "Is AOL = " + browser.AOL + "\n"
                + "Is Win16 = " + browser.Win16 + "\n"
                + "Is Win32 = " + browser.Win32 + "\n"
                + "Supports Frames = " + browser.Frames + "\n"
                + "Supports Tables = " + browser.Tables + "\n"
                + "Supports Cookies = " + browser.Cookies + "\n"
                + "Supports VBScript = " + browser.VBScript + "\n"
                + "Supports JavaScript = " +
                    browser.EcmaScriptVersion.ToString() + "\n"
                + "Supports Java Applets = " + browser.JavaApplets + "\n"
                + "Supports ActiveX Controls = " + browser.ActiveXControls
                      + "\n"
                + "Supports JavaScript Version = " +
                    browser["JavaScriptVersion"] + "\n";



            //var categoryList = _db.Category.ToList();
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

    }
}