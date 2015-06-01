using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace salescampaignschedule.web.Helpers
{
    public static class LoggerHelper
    {
        readonly static log4net.ILog _logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static void Error(Exception exception)
        {
            _logger.Error(exception.Message);
        }
    }
}