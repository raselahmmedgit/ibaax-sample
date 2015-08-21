using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ExporterObjects;
using lab.exportfile.Attributes;
using lab.exportfile.Helpers;
using lab.exportfile.Models;

namespace lab.exportfile.Controllers
{
    public class HomeController : BaseController
    {
        AppDbContext _db = new AppDbContext();

        public ActionResult Index()
        {
            return View();
        }

        //[DeleteFile]
        public ActionResult GetExporterFile()
        {
            var _productList = _db.Product.ToList();

            string filePathExport = ExportFileHelper.ExportByExporter(_productList, ExportToFormat.CSV);

            Session["ExportFilePath"] = filePathExport;

            return this.File(filePathExport, "application/octet-stream", System.IO.Path.GetFileName(filePathExport));
        }

        public ActionResult GetMyFile()
        {
            var _productList = _db.Product.ToList();

            string filePathExport = ExportFileHelper.ExportByExporter(_productList, ExportToFormat.CSV);

            Session["ExportFilePath"] = filePathExport;

            return this.File(filePathExport, "application/octet-stream", System.IO.Path.GetFileName(filePathExport));
        }

        [HttpGet]
        public void GetCSVFile()
        {
            var _productList = _db.Product.ToList();

            string fileName = "Product List";

            ExportCsvFileHelper.ExportToCSVFile(_productList, fileName);
        }

        [HttpGet]
        public void GetExcelFile()
        {
            var _productList = _db.Product.ToList();

            string fileName = "Product List";

            ExportExcelFileHelper.ExportToExcelFile(_productList, fileName);
        }

        //PDF
        protected FileContentResult ViewPdf(string pageTitle, string viewName, object model)
        {
            // Render the view html to a string.
            string htmlText = HtmlViewRenderer.RenderViewToString(this, viewName, model);

            // Let the html be rendered into a PDF document through iTextSharp.
            byte[] buffer = standardPdfRenderer.Render(htmlText, pageTitle);

            // Return the PDF as a binary stream to the client.
            return File(buffer, "application/pdf", "file.pdf");
        }


        #region Stream

        [HttpPost]
        public ActionResult GetCSVFile(int id)
        {
            var _productList = _db.Product.ToList();

            string fileName = "Product List";

            ExportFileViewModel exportFileViewModel = ExportCsvFileHelper.ExportToCSVFileByFileStream(_productList, fileName);

            File(exportFileViewModel.FileStream,   //The binary data of the XLS file
              exportFileViewModel.ContentType, //MIME type of Excel files
              exportFileViewModel.FileName);     //Suggested file name in the "Save as" dialog which will be displayed to the end user

            return Json(new { msg = "Export to CSV successfully.", status = "success" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetExcelFile(int id)
        {
            var _productList = _db.Product.ToList();

            string fileName = "Product List";

            ExportFileViewModel exportFileViewModel = ExportExcelFileHelper.ExportToExcelFileByFileStream(_productList, fileName);

            File(exportFileViewModel.FileStream.ToArray(),   //The binary data of the XLS file
              exportFileViewModel.ContentType, //MIME type of Excel files
              exportFileViewModel.FileName);     //Suggested file name in the "Save as" dialog which will be displayed to the end user

            return Json(new { msg = "Export to EXCEL successfully.", status = "success" }, JsonRequestBehavior.AllowGet);


        }


        #endregion

        #region Ajax

        [HttpPost]
        public ActionResult GetCSVFileAjax(int id)
        {
            var _productList = _db.Product.ToList();

            string sessionName = "ProductListCSV";

            Session[sessionName] = _productList;

            string strUrl = "/Home/ExportCSVFile?sessionName=" + sessionName;

            return Json(new { success = true, downloadurl = strUrl, message = "Export to CSV successfully.", status = "success" }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public void ExportCSVFile(string sessionName)
        {
            try
            {
                if (Session[sessionName] != null)
                {
                    var productList = (List<Product>)Session[sessionName];

                    string fileName = "Product List";

                    ExportCsvFileHelper.ExportToCSVFile(productList, fileName);

                }

            }
            catch (Exception)
            {
                throw;
            }

        }

        #endregion
    }
}