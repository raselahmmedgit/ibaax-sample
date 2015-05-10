using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace lab.webapps.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> User { get; set; }

        #region For Web Site

        public DbSet<WebSiteDomain> WebSiteDomain { get; set; }
        public DbSet<WebSiteInfo> WebSiteInfo { get; set; }
        public DbSet<WebSitePage> WebSitePage { get; set; }
        public DbSet<WebSiteLayout> WebSiteLayout { get; set; }
        public DbSet<WebSiteWidget> WebSiteWidget { get; set; }
        public DbSet<WebSiteTheme> WebSiteTheme { get; set; }

        #endregion

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<ContactJobTiteRatingStaffingRate>()
            //        .Map(e => e.ToTable("ContactJobTiteRatingStaffingRate"));
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }

    }

    #region Initial data

    // Change the base class as follows if you want to drop and create the database during development:
    //public class DbInitializer : DropCreateDatabaseAlways<AppDbContext>
    //public class DbInitializer : CreateDatabaseIfNotExists<AppDbContext>
    public class DbInitializer : DropCreateDatabaseIfModelChanges<AppDbContext>
    {
        protected override void Seed(AppDbContext context)
        {
            // Create default categories.
            var users = new List<User>
                            {
                                new User { UserId=1, UserName = "Rasel", Password="123456"},
                                new User {UserId=2, UserName = "Sohel", Password="123456"},
                                new User {UserId=3, UserName = "Bappi", Password="123456"}
                            };

            users.ForEach(c => context.User.Add(c));

            context.SaveChanges();

        }
    }

    #endregion
}