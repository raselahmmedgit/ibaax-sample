using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using ExporterObjects;
using System.IO;

namespace lab.exportfile.Helpers
{
    public class ExportFileViewModel
    {
        public MemoryStream FileStream { get; set; }
        public string ContentType { get; set; }
        public string FileName { get; set; }
    }

    public static class ExportFileHelper
    {
        /// <summary>
        /// Exporter : Export To CSV,Excel2003XML,Excel2007,HTML,PDFtextSharpXML,Word2003XML,Word2007,XML 
        /// </summary>
        /// <typeparam name="T">Any Type Object</typeparam>
        /// <param name="objectList">Object Collection</param>
        /// <param name="exportToFormat">File Formate Like CSV,Excel2003XML,Excel2007,HTML,PDFtextSharpXML,Word2003XML,Word2007,XML</param>
        /// <returns></returns>
        public static string ExportByExporter<T>(this List<T> objectList, ExportToFormat exportToFormat)
        {
            try
            {
                List<T> list = objectList.ToList();
                ExportList<T> exp = new ExportList<T>();
                exp.PathTemplateFolder = System.Web.HttpContext.Current.Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["ExportFilePathTemplateFolder"].ToString());

                string filePathExport = System.Web.HttpContext.Current.Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["ExportFilePathFolder"].ToString() + "/file" + ExportBase.GetFileExtension((exportToFormat)));

                exp.ExportTo(list, (exportToFormat), filePathExport);

                return filePathExport;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static ExportFileViewModel ExportToCSV<T>(this List<T> objectList, string fileName)
        {
            try
            {
                List<T> list = objectList.ToList();
                //Export To CSV 
                MemoryStream output = new MemoryStream();

                StreamWriter writer = new StreamWriter(output, Encoding.UTF8);

                writer.Write("ProductId,");
                writer.Write("ProductName,");
                writer.Write("ProductPrice,");
                writer.Write("CategoryId,");
                writer.Write("CategoryName");
                writer.WriteLine();

                //foreach (var item in list)
                //{
                //    writer.Write(product.ProductId);
                //    writer.Write(",");
                //    writer.Write("\"");
                //    writer.Write(product.Name);
                //    writer.Write("\"");
                //    writer.Write(",");
                //    writer.Write("\"");
                //    writer.Write(product.Price);
                //    writer.Write("\"");
                //    writer.Write(",");
                //    writer.Write(product.Category != null ? product.CategoryId : 0);
                //    writer.Write(",");
                //    writer.Write("\"");
                //    if (product.Category != null) writer.Write(product.Category != null ? product.Category.Name : string.Empty);
                //    writer.Write("\"");
                //    writer.WriteLine();
                //}
                writer.Flush();
                output.Position = 0;
                



                var exportFileViewModel = 
                    new ExportFileViewModel(){
                                    FileStream = output,
                                    ContentType = "text/comma-separated-values",
                                    FileName = fileName
                                };

                return exportFileViewModel;
            }
            catch (Exception)
            {
                throw;
            }
        }
        
    }
}