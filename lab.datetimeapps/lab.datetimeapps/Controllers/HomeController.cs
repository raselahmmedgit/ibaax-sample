using lab.datetimeapps.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace lab.datetimeapps.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CategoryList()
        {
            var categories = new List<Category>
                            {
                                new Category { CategoryId=1, Name = "Fruit", CreateDate = DateTime.Now},
                                new Category {CategoryId=2, Name = "Cloth", CreateDate = DateTime.Now},
                                new Category {CategoryId=3, Name = "Car", CreateDate = DateTime.Now},
                                new Category {CategoryId=4, Name = "Book", CreateDate = DateTime.Now},
                                new Category {CategoryId=5, Name = "Shoe", CreateDate = DateTime.Now},
                                new Category {CategoryId=6, Name = "Wiper", CreateDate = DateTime.Now},
                                new Category {CategoryId=7, Name = "Laptop", CreateDate = DateTime.Now},
                                new Category {CategoryId=8, Name = "Desktop", CreateDate = DateTime.Now},
                                new Category {CategoryId=9, Name = "Notebook", CreateDate = DateTime.Now},
                                new Category {CategoryId=10, Name = "AC", CreateDate = DateTime.Now}

                            };

            return View(categories.ToList());
        }

        public ActionResult PDFByRazorPDF()
        {
            var categories = new List<Category>
                            {
                                new Category { CategoryId=1, Name = "Fruit", CreateDate = DateTime.Now},
                                new Category {CategoryId=2, Name = "Cloth", CreateDate = DateTime.Now},
                                new Category {CategoryId=3, Name = "Car", CreateDate = DateTime.Now},
                                new Category {CategoryId=4, Name = "Book", CreateDate = DateTime.Now},
                                new Category {CategoryId=5, Name = "Shoe", CreateDate = DateTime.Now},
                                new Category {CategoryId=6, Name = "Wiper", CreateDate = DateTime.Now},
                                new Category {CategoryId=7, Name = "Laptop", CreateDate = DateTime.Now},
                                new Category {CategoryId=8, Name = "Desktop", CreateDate = DateTime.Now},
                                new Category {CategoryId=9, Name = "Notebook", CreateDate = DateTime.Now},
                                new Category {CategoryId=10, Name = "AC", CreateDate = DateTime.Now}

                            };
            //return View(categories.ToList());
            return new RazorPDF.PdfResult(categories.ToList(), "CategoryList");
        }

        public ActionResult PDFByRotativa()
        {
            var categories = new List<Category>
                            {
                                new Category { CategoryId=1, Name = "Fruit", CreateDate = DateTime.Now},
                                new Category {CategoryId=2, Name = "Cloth", CreateDate = DateTime.Now},
                                new Category {CategoryId=3, Name = "Car", CreateDate = DateTime.Now},
                                new Category {CategoryId=4, Name = "Book", CreateDate = DateTime.Now},
                                new Category {CategoryId=5, Name = "Shoe", CreateDate = DateTime.Now},
                                new Category {CategoryId=6, Name = "Wiper", CreateDate = DateTime.Now},
                                new Category {CategoryId=7, Name = "Laptop", CreateDate = DateTime.Now},
                                new Category {CategoryId=8, Name = "Desktop", CreateDate = DateTime.Now},
                                new Category {CategoryId=9, Name = "Notebook", CreateDate = DateTime.Now},
                                new Category {CategoryId=10, Name = "AC", CreateDate = DateTime.Now}

                            };
            return new Rotativa.ViewAsPdf("CategoryList", categories.ToList()) { FileName = "CategoryList.pdf" };
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}