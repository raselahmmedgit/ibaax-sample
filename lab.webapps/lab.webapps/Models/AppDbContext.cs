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
            // Create default Users.
            var users = new List<User>
                            {
                                new User {UserId=1, UserName = "Rasel", Password="123456"},
                                new User {UserId=2, UserName = "Sohel", Password="123456"},
                                new User {UserId=3, UserName = "Bappi", Password="123456"}
                            };

            users.ForEach(c => context.User.Add(c));

            context.SaveChanges();

            // Create default WebSiteDomains.
            var webSiteDomains = new List<WebSiteDomain>
                            {
                                new WebSiteDomain {WebSiteDomainId=1, Name = "RE BAAX", Url="http://www.rebaax.com"},
                                new WebSiteDomain {WebSiteDomainId=2, Name = "RE BAAX CODE", Url="http://www.rebaxcode.com"}
                            };

            webSiteDomains.ForEach(c => context.WebSiteDomain.Add(c));

            context.SaveChanges();

            // Create default WebSiteInfos.
            var webSiteInfos = new List<WebSiteInfo>
                            {
                                new WebSiteInfo {WebSiteInfoId=1, Name = "reBaax", Title="reBaax", Footer = "reBaax", WebSiteDomainId = 1},
                                new WebSiteInfo {WebSiteInfoId=2, Name = "reBaaxCode", Title="reBaaxCode", Footer = "reBaaxCode", WebSiteDomainId = 2}
                            };

            webSiteInfos.ForEach(c => context.WebSiteInfo.Add(c));

            context.SaveChanges();

            // Create default WebSitePages.
            var webSitePages = new List<WebSitePage>
                            {
                                new WebSitePage {WebSitePageId=1, Name = "Home", Url="Home", UrlParam = null, IsDefault = true, WebSiteDomainId = 1},
                                new WebSitePage {WebSitePageId=2, Name = "Property", Url="Property", UrlParam = null, IsDefault = false, WebSiteDomainId = 1},
                                new WebSitePage {WebSitePageId=3, Name = "Contact", Url="Contact", UrlParam = null, IsDefault = false, WebSiteDomainId = 1},
                                new WebSitePage {WebSitePageId=4, Name = "Home", Url="Home", UrlParam = null, IsDefault = true, WebSiteDomainId = 2},
                                new WebSitePage {WebSitePageId=5, Name = "Agent", Url="Agent", UrlParam = null, IsDefault = false, WebSiteDomainId = 2},
                                new WebSitePage {WebSitePageId=6, Name = "Developer", Url="Developer", UrlParam = null, IsDefault = false, WebSiteDomainId = 2}
                            };

            webSitePages.ForEach(c => context.WebSitePage.Add(c));

            context.SaveChanges();

        }
    }

    #endregion
}