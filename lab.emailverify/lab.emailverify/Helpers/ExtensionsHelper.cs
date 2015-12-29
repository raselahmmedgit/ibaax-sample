using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace lab.emailverify.Helpers
{
    public static class Extensions
    {
        public static string RenderPartialViewToString(this Controller controller, string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
                viewName = controller.ControllerContext.RouteData.GetRequiredString("action");

            controller.ViewData.Model = model;

            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(controller.ControllerContext, viewName);
                var viewContext = new ViewContext(controller.ControllerContext, viewResult.View, controller.ViewData, controller.TempData, sw);
                viewResult.View.Render(viewContext, sw);
                string partialViewContent = sw.GetStringBuilder().ToString();
                return partialViewContent;
            }
        }

        public static string RenderPartialViewToString(this Controller controller, ViewResult viewResult, object model)
        {
            controller.ViewData.Model = model;

            using (var sw = new StringWriter())
            {
                var viewContext = new ViewContext(controller.ControllerContext, viewResult.View, controller.ViewData, controller.TempData, sw);
                viewResult.View.Render(viewContext, sw);
                string partialViewContent = sw.GetStringBuilder().ToString();
                return partialViewContent;
            }
        }

        public static bool HasValue(this int value)
        {
            if (value > 0)
            {
                return true;
            }
            return false;
        }

        public static bool HasValue(this int? value)
        {
            if (value > 0)
            {
                return true;
            }
            return false;
        }

        public static int ToValue(this int value)
        {
            return value;
        }

        public static int ToValue(this int? value)
        {
            if (value.HasValue)
            {
                return value.Value;
            }
            return 0;
        }

        public static int DayDifferenceFromToday(this DateTime inputdate)
        {
            TimeSpan span = DateTime.Now.Subtract(inputdate);
            return (int)span.TotalDays;
        }

       
    }
}