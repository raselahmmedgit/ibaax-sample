using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace lab.consoleapps
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start");

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
