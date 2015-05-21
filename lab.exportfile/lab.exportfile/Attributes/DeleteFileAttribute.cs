using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace lab.exportfile.Attributes
{
    public class DeleteFileAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            // Delete file
            if (HttpContext.Current.Session["ExportFilePath"] != null)
            {
                File.Delete(HttpContext.Current.Session["ExportFilePath"].ToString());
            }
        }
    }
}