using System.Data.Entity;
using lab.emailverify.Helpers;
using System;
using System.IO;
using lab.emailverify.Models;

namespace lab.emailverify
{
    public class BootStrapper
    {
        public static void Run()
        {
            try
            {
                log4net.Config.XmlConfigurator.Configure(new FileInfo(System.Web.HttpContext.Current.Server.MapPath("~/Web.config")));
                InitializeAndSeedDb();
            }
            catch (Exception ex)
            {
                ExceptionHelper.Manage(ex, true);
            }

        }

        private static void InitializeAndSeedDb()
        {
            try
            {
                // Initializes and seeds the database.
                Database.SetInitializer(new DbInitializer());

                using (var context = new AppDbContext())
                {
                    context.Database.Initialize(force: true);
                }
            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }
}