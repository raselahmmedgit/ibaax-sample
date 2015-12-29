using Cobisi.EmailVerify;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
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

            SendMail("Hello", "This is test");
            //SendMailByHotMail("Hello", "This is test");

            var date = DateTime.Today;
            var currentDate = DateTime.Today.ToString("yyyy-MM-dd");

            //DateTime startOfWeek = DateTime.Today.AddDays(-1 * (int)(DateTime.Today.DayOfWeek));

            DateTime startOfWeek = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek);
            DateTime endOfWeek = startOfWeek.AddDays(7);
            DateTime endOfLastWeek = startOfWeek.AddDays(-1);

            var lastDayOfMonth = DateTime.DaysInMonth(date.Year, date.Month);

            DateTime firstOfThisMonth = new DateTime(date.Year, date.Month, 1);
            DateTime firstOfNextMonth = new DateTime(date.Year, date.Month, 1).AddMonths(1);
            DateTime lastOfThisMonth = firstOfNextMonth.AddDays(-1);

            DatetimeMatch();
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

        public static void CobisiEmailVerify()
        {
            var settings = new VerificationSettings
            {
                AllowDomainLiterals = false,
                AllowComments = true,
                AllowQuotedStrings = true
            };

            // The component will use just the provided DNS server for its lookups

            settings.DnsServers.Clear();
            settings.DnsServers.Add(IPAddress.Parse("8.8.8.8"));

            // Pass the configured settings to the verification engine

            var result = Engine.Run("john@example.com",
                                    VerificationLevel.Mailbox,
                                    settings).Result;

            VerificationEngine engine = new VerificationEngine();
            engine.VerificationLevelCompleted += (sender, e) =>
            {
                switch (e.Level.Name.ToUpper())
                {
                    case "SYNTAX":
                        if (e.Result.Status == VerificationStatus.Success) emailVerificationInfo.VerificationLevel = 1;
                        break;

                    case "DEADOMAIN":
                        if (e.Result.Status == VerificationStatus.Success) emailVerificationInfo.VerificationLevel = 2;
                        break;

                    case "DNS":
                        if (e.Result.Status == VerificationStatus.Success) emailVerificationInfo.VerificationLevel = 3;
                        break;

                    case "SMTP":
                        if (e.Result.Status == VerificationStatus.Success) emailVerificationInfo.VerificationLevel = 4;
                        break;

                    case "MAILBOX":
                        if (e.Result.Status == VerificationStatus.Success) emailVerificationInfo.VerificationLevel = 5;
                        break;

                    case "CATCHALL":
                        if (e.Result.Status == VerificationStatus.Success) emailVerificationInfo.VerificationLevel = 6;
                        break;
                }
            };
            var result = engine.Run(PrimaryEmailAddress, Level);
            emailVerificationInfo.IsAjax = true;
            emailVerificationInfo.IsPositive = result.Result.LastStatus == VerificationStatus.Success;
            emailVerificationInfo.IsVerified = emailVerificationInfo.VerificationLevel >= 5;
            emailVerificationInfo.LastStatus = result.Result.LastStatus.ToString();

            // ...
        }

        public static void SendMail(string subject, string body)
        {
            try
            {
                //var fromAddress = new MailAddress("rasel.tester@gmail.com", "Rasel Tester");
                //var toAddress = new MailAddress("rasel.ibaax@outlook.com", "Rasel iBaax");
                //const string fromPassword = "@@987654";

                //var smtp = new SmtpClient
                //{
                //    Host = "smtp.gmail.com",
                //    Port = 587,
                //    EnableSsl = true,
                //    DeliveryMethod = SmtpDeliveryMethod.Network,
                //    UseDefaultCredentials = false,
                //    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                //};
                //using (var message = new MailMessage(fromAddress, toAddress)
                //{
                //    Subject = subject,
                //    Body = body
                //})
                //{
                //    smtp.Send(message);
                //}

                string smtpAddress = "smtp.gmail.com";
                int portNumber = 587;
                bool enableSSL = true;

                string emailFrom = "rasel.tester@gmail.com";
                string password = "@@987654";
                string emailTo = "rasel.ibaax@outlook.com";

                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(emailFrom);
                    mail.To.Add(emailTo);
                    mail.Subject = subject;
                    mail.Body = body;
                    mail.IsBodyHtml = true;
                    // Can set to false, if you are sending pure text.

                    using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                    {
                        smtp.Credentials = new NetworkCredential(emailFrom, password);
                        smtp.EnableSsl = enableSSL;
                        smtp.Send(mail);
                    }
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void SendMailByHotMail(string subject, string body)
        {
            try
            {
                //var fromAddress = new MailAddress("rasel.tester@outlook.com", "Rasel Tester");
                //var toAddress = new MailAddress("rasel.ibaax@outlook.com", "Rasel iBaax");
                //const string fromPassword = "@@987654";

                //var smtp = new SmtpClient
                //{
                //    Host = "smtp.live.com",
                //    Port = 587,
                //    EnableSsl = true,
                //    DeliveryMethod = SmtpDeliveryMethod.Network,
                //    UseDefaultCredentials = false,
                //    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                //};
                //using (var message = new MailMessage(fromAddress, toAddress)
                //{
                //    Subject = subject,
                //    Body = body
                //})
                //{
                //    smtp.Send(message);
                //}

                OpaqueMail.Net.MailMessage message = new OpaqueMail.Net.MailMessage("rasel.tester@outlook.com", "rasel.ibaax@outlook.com");
                message.Subject = subject;
                message.Body = body;
                var client = new OpaqueMail.Net.SmtpClient
                {
                    Host = "smtp.live.com",
                    Port = 587,
                    Credentials = new System.Net.NetworkCredential("rasel.tester@outlook.com", "@@987654"),
                    EnableSsl = true
                };
                client.Send(message);

            }
            catch (Exception)
            {
                throw;
            }
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

        private static void DatetimeMatch()
        {
            DateTime dateTime1 = new DateTime(2015, 07, 25);
            DateTime dateTime2 = DateTime.Now;

            if (dateTime1 == dateTime2)
            {
                
            }

            if (dateTime1.Date.Equals(dateTime2.Date))
            {
                // the dates are equal
                Console.WriteLine("Date 1 :" + dateTime1.Date + " Date 2 :" + dateTime2.Date);
            }
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
