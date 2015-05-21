using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace salescampaignschedule.web.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<SalesCampaign> SalesCampaign { get; set; }
        public DbSet<SalesCampaignSchedule> SalesCampaignSchedule { get; set; }
        public DbSet<SalesCampaignScheduleContact> SalesCampaignScheduleContact { get; set; }

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
            // Create default SalesCampaigns.
            var salesCampaigns = new List<SalesCampaign>
                            {
                                new SalesCampaign {SalesCampaignId=1, SalesCampaignName = "Sales Campaign 1"},
                                new SalesCampaign {SalesCampaignId=2, SalesCampaignName = "Sales Campaign 2"}
                            };

            salesCampaigns.ForEach(c => context.SalesCampaign.Add(c));

            context.SaveChanges();

            // Create default SalesCampaignSchedule.
            var salesCampaignSchedules = new List<SalesCampaignSchedule>
                            {
                                new SalesCampaignSchedule {SalesCampaignScheduleId=1, SalesCampaignScheduleName = "Sales Campaign 1 Schedule", ScheduleDate=DateTime.Now, ScheduleTimeZone="GMT+05:30) India Standard Time (Asia/Colombo)", SalesCampaignId=1},
                                new SalesCampaignSchedule {SalesCampaignScheduleId=2, SalesCampaignScheduleName = "Sales Campaign 1 Schedule", ScheduleDate=DateTime.Now, ScheduleTimeZone="(GMT+06:00) Bangladesh Time (Asia/Dhaka)", SalesCampaignId=1},
                                new SalesCampaignSchedule {SalesCampaignScheduleId=3, SalesCampaignScheduleName = "Sales Campaign 2 Schedule", ScheduleDate=DateTime.Now, ScheduleTimeZone="(GMT+04:00) Armenia Time (Asia/Yerevan)", SalesCampaignId=2}
                            };

            salesCampaignSchedules.ForEach(c => context.SalesCampaignSchedule.Add(c));

            context.SaveChanges();

            // Create default SalesCampaignScheduleContact.
            var salesCampaignScheduleContacts = new List<SalesCampaignScheduleContact>
                            {
                                new SalesCampaignScheduleContact {SalesCampaignScheduleContactId=1, ContactName = "Rasel outlook", ContactEmail="rasel.ibaax@outlook.com", SalesCampaignScheduleId = 1},
                                new SalesCampaignScheduleContact {SalesCampaignScheduleContactId=2, ContactName = "Rasel gmail", ContactEmail="rasel.ibaax@gmail.com", SalesCampaignScheduleId = 1},
                                new SalesCampaignScheduleContact {SalesCampaignScheduleContactId=3, ContactName = "Rasel yahoo", ContactEmail="rasel.ibaax@yahoo.com", SalesCampaignScheduleId = 1},
                                new SalesCampaignScheduleContact {SalesCampaignScheduleContactId=4, ContactName = "Rasel outlook", ContactEmail="rasel.ibaax@outlook.com", SalesCampaignScheduleId = 2},
                                new SalesCampaignScheduleContact {SalesCampaignScheduleContactId=5, ContactName = "Rasel gmail", ContactEmail="rasel.ibaax@gmail.com", SalesCampaignScheduleId = 2},
                                new SalesCampaignScheduleContact {SalesCampaignScheduleContactId=6, ContactName = "Rasel yahoo", ContactEmail="rasel.ibaax@yahoo.com", SalesCampaignScheduleId = 2},
                                new SalesCampaignScheduleContact {SalesCampaignScheduleContactId=7, ContactName = "Rasel outlook", ContactEmail="rasel.ibaax@outlook.com", SalesCampaignScheduleId = 3},
                                new SalesCampaignScheduleContact {SalesCampaignScheduleContactId=8, ContactName = "Rasel gmail", ContactEmail="rasel.ibaax@gmail.com", SalesCampaignScheduleId = 3},
                                new SalesCampaignScheduleContact {SalesCampaignScheduleContactId=9, ContactName = "Rasel yahoo", ContactEmail="rasel.ibaax@yahoo.com", SalesCampaignScheduleId = 3},
                            };

            salesCampaignScheduleContacts.ForEach(c => context.SalesCampaignScheduleContact.Add(c));

            context.SaveChanges();

        }
    }

    #endregion
}