using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;

namespace lab.exportfile.Helpers
{
    public static class ExportExcelFileHelper
    {
        public static void ExportToExcelFile<T>(this List<T> objectList, string fileName)
        {
            string excelFormat = GetToExcelFormatValue(objectList);
            ExportToExcelFile(excelFormat, fileName);
        }

        public static ExportFileViewModel ExportToExcelFileByFileStream<T>(this List<T> objectList, string fileName)
        {
            ExportFileViewModel exportFileViewModel = new ExportFileViewModel();
            string excelFormat = GetToExcelFormatValue(objectList);
            exportFileViewModel = ExportToExcelFileByFileStream(excelFormat, fileName);
            return exportFileViewModel;
        }

        private static ExportFileViewModel ExportToExcelFileByFileStream(string excelFormat, string fileName)
        {
            ExportFileViewModel exportFileViewModel = new ExportFileViewModel();
            MemoryStream memoryStream = new MemoryStream();
            StreamWriter streamWriter = new StreamWriter(memoryStream, Encoding.UTF8);
            streamWriter.Write(excelFormat);
            string fileNameWithExtension = fileName + ".xls";
            exportFileViewModel.FileStream = memoryStream;
            exportFileViewModel.ContentType = "application/vnd.ms-excel";
            exportFileViewModel.FileName = fileNameWithExtension;
            return exportFileViewModel;
        }

        private static void ExportToExcelFile(string excelFormat, string fileName)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            string fileNameWithExtension = fileName + ".xls";
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=\"" + fileNameWithExtension + "\"");
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.Write(excelFormat);
            HttpContext.Current.Response.End();
        }

        private static string GetToExcelFormatValue<T>(this List<T> objectList)
        {
            var sb = new StringBuilder();

            //Get the properties for type T for the headers
            PropertyInfo[] propInfos = typeof(T).GetProperties();
            for (int i = 0; i <= propInfos.Length - 1; i++)
            {
                sb.Append(propInfos[i].Name);

                if (i < propInfos.Length - 1)
                {
                    sb.Append("\t");
                }
            }

            sb.AppendLine();

            //Loop through the collection, then the properties and add the values
            for (int i = 0; i <= objectList.Count - 1; i++)
            {
                T item = objectList[i];
                for (int j = 0; j <= propInfos.Length - 1; j++)
                {
                    object o = item.GetType().GetProperty(propInfos[j].Name).GetValue(item, null);
                    if (o != null)
                    {
                        string value = o.ToString();

                        //Check if the value contans a comma and place it in quotes if so
                        if (value.Contains(","))
                        {
                            value = string.Concat("\"", value, "\"");
                        }

                        //Replace any \r or \n special characters from a new line with a space
                        if (value.Contains("\r"))
                        {
                            //value = value.Replace("\r", " ");
                            value = string.Concat("\"", value, "\"");
                        }
                        if (value.Contains("\n"))
                        {
                            //value = value.Replace("\n", " ");
                            value = string.Concat("\"", value, "\"");
                        }
                        if (value.Contains("\t"))
                        {
                            //value = value.Replace("\t", " ");
                            value = string.Concat("\"", value, "\"");
                        }

                        sb.Append(value);
                    }

                    if (j < propInfos.Length - 1)
                    {
                        sb.Append("\t");
                    }
                }

                sb.AppendLine();
            }

            return sb.ToString();
        }
    }
}