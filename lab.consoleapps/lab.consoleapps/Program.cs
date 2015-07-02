using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace lab.consoleapps
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start");

            LinqTest();

            var RoleId = Convert.ToInt32(RoleEnum.SuperAdmin);
            var RoleName = RoleEnum.SuperAdmin.ToString();

            Random rnd = new Random();
            int myRandomNo = rnd.Next(1000000, 9999999); // creates a 8 digit random no.

            Random generator = new Random();
            String r = generator.Next(0, 10000000).ToString("D7");

            var temp = Guid.NewGuid().ToString().Replace("-", string.Empty);
            var reg = Regex.Replace(temp, "[a-zA-Z]", string.Empty);
            var barcode = Regex.Replace(temp, "[a-zA-Z]", string.Empty).Substring(0, 7);

            string path = AppDomain.CurrentDomain.BaseDirectory;
            string logPath = Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)).FullName;
            string myDocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            WriteLog("Done");

            testBool tb = new testBool();

            Console.WriteLine(tb.myBool);

            #region MyRegion

            DateTime _sDate = new DateTime(2015, 5, 14);
            DateTime _eDate = new DateTime(2015, 5, 28);

            Console.WriteLine(GetRemainingDays(_sDate, _eDate, new HashSet<DayOfWeek> { DayOfWeek.Sunday, DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday}));

            Console.WriteLine(GetRemainingDays(_sDate, _eDate, new HashSet<DayOfWeek>{}));

            Console.WriteLine(GetTotalDays(_sDate, _eDate));

            //Product _product = new Product();
            Product _product = new Product() { P = new Product() };
            //string name = _product.Name;

            if (IsEmptyEntity(_product))
            {
                Console.WriteLine("if");
            }
            else
            {
                Console.WriteLine("else"); 
            }

            #endregion

            Console.WriteLine("End");
            Console.Read();
        }

        public enum RoleEnum : int
        {
            SuperAdmin = 1,
            Admin = 2,
            HeadTeacher = 3,
            AssistantHeadTeacher = 4,
            Teacher = 5,
            Employee = 6,
            Student = 7,
            Parent = 8,
            Guardian = 9
        }

        private static void WriteLog(string message)
        {
            StreamWriter streamWriter = null;
            string myDocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            //streamWriter = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "Log.txt", true);
            streamWriter = new StreamWriter(myDocumentsPath + @"\Log.txt", true);
            streamWriter.WriteLine(DateTime.Now.ToString() + " : " + message);
            streamWriter.Flush();
            streamWriter.Close();
        }

        private static void LinqTest()
        {
            var categoryList1 = new List<Category>
            {
                new Category{ Id  = 1, Name = "a"},
                new Category{ Id  = 2, Name = "b"},
                new Category{ Id  = 3, Name = "c"},
                new Category{ Id  = 4, Name = "d"},
                new Category{ Id  = 5, Name = "e"}
            };

            var categoryList2 = new List<Category>
            {
                new Category{ Id  = 1, Name = "a"},
                new Category{ Id  = 5, Name = "e"}
            };

            var matchCategoryList = (from c1 in categoryList1
                                     join c2 in categoryList2 on c1.Id equals c2.Id
                                    select c1).ToList();

            var notMatchCategoryList = (from c1 in categoryList1
                                        where !(categoryList2.Any(item => item.Id == c1.Id))
                                       select c1).ToList();
        }

        public class Category
        {
            public int Id;
            public string Name;
        }

        public class testBool
        {
            public Boolean myBool;
        }

        public class Product
        {
            public int Id { get; set; }
            public string Name { get; set; }

            public Product P { get; set; }
        }

        static string GetDefaultValue(PropertyInfo prop)
        {
            var attributes = prop.GetCustomAttributes(typeof(DefaultValueAttribute), true);
            if (attributes.Length > 0)
            {
                var defaultAttr = (DefaultValueAttribute)attributes[0];
                return defaultAttr.Value.ToString();
            }

            // Attribute not found, fall back to default value for the type
            if (prop.PropertyType.IsValueType)
                return Activator.CreateInstance(prop.PropertyType).ToString();
            return null;
        }


        public static bool IsEmptyEntity<T>(T obj)
        {
            foreach (PropertyInfo property in typeof(T).GetProperties())
            {
                if (property.GetValue(obj) != null)
                {
                    if (property.GetValue(obj).ToString() != GetDefaultValue(property))
                        return false;    
                }
            }
            return true;
        }

        public static double GetTotalDays(DateTime startDate, DateTime endDate)
        {
            return (endDate - startDate).TotalDays;
        }

        public static double GetRemainingDays(DateTime startDate, DateTime endDate, ISet<DayOfWeek> includedDays)
        {
            if (includedDays.Any())
            {
                //Get Total day base on includedDays
                return Enumerable.Range(0, Int32.MaxValue)
                    .Select(n => startDate.AddDays(n + 1))
                    .TakeWhile(date => date <= endDate)
                    .Count(date => includedDays.Contains(date.DayOfWeek)); 
            }
            else
            {
                return Enumerable.Range(0, Int32.MaxValue)
                    .Select(n => startDate.AddDays(n + 1))
                    .TakeWhile(date => date <= endDate)
                    .Count();
            }
        }

        public static int GetRemainingDays(DateTime startDate, DateTime endDate, int[] dayOfWeekTags)
        {
            int i = 0;

            for (DateTime day = startDate.AddDays(1); day.Date <= endDate.Date; day = day.AddDays(1))
                i += dayOfWeekTags[(int)day.DayOfWeek];

            return i;
        } 
    }
}
