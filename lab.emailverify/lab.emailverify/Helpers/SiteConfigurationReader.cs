using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace lab.emailverify.Helpers
{
    public class SiteConfigurationReader
    {
        public static String CobisiLicenseKey
        {
            get
            {
                return ConfigurationManager.AppSettings.Get("CobisiLicenseKey").ToString();
            }
        }

        public static String MandrillApiKey
        {
            get
            {
                return ConfigurationManager.AppSettings.Get("MandrillApiKey").ToString();
            }
        }
    }
}