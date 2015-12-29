using lab.ngsample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace lab.ngsample.Controllers
{
    public class ErrorController : BaseController
    {
        #region Global Variable Declaration

        #endregion Global Variable Declaration

        #region Constructor

        #endregion Constructor

        #region Action/Method

        public ActionResult Index()
        {
            var errorModel = new BaseModel();
            if (errorModel.ErrorMessage != null)
            {
                errorModel.ErrorMessage = "We are facing some problem while processing the current request. Please try again later.";
            }
            return View(errorModel);
        }

        public ActionResult Error500()
        {
            return View();
        }

        public ActionResult Error404()
        {
            return View();
        }

        public ActionResult Error403()
        {
            return View();
        }

        #endregion
    }
}