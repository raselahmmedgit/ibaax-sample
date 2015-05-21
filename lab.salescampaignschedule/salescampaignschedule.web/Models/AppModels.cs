using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace salescampaignschedule.web.Models
{
    public class BaseModel
    {
        [NotMapped]
        public virtual int TempId { get; set; }

        [NotMapped]
        public virtual string KendoWindow { get; set; }

        [NotMapped]
        public virtual string ActionLink { get; set; }

        [NotMapped]
        public virtual bool HasCreate { get; set; }

        [NotMapped]
        public virtual bool HasUpdate { get; set; }

        [NotMapped]
        public virtual bool HasDelete { get; set; }
    }

    public class SalesCampaign //: BaseModel
    {
        [Key]
        public int SalesCampaignId { get; set; }
        [DisplayName("Sales Campaign Name")]
        [Required(ErrorMessage = "Sales Campaign Name is required")]
        [MaxLength(200)]
        public string SalesCampaignName { get; set; }
    }

    public class SalesCampaignSchedule //: BaseModel
    {
        [Key]
        public int SalesCampaignScheduleId { get; set; }
        [DisplayName("Sales Campaign Schedule Name")]
        [Required(ErrorMessage = "Sales Campaign Schedule Name is required")]
        [MaxLength(200)]
        public string SalesCampaignScheduleName { get; set; }

        [DisplayName("Schedule Date")]
        public DateTime ScheduleDate { get; set; }

        [DisplayName("Schedule Time Zone")]
        public string ScheduleTimeZone { get; set; }

        [Required(ErrorMessage = "Select one category.")]
        public int SalesCampaignId { get; set; }
        [ForeignKey("SalesCampaignId")]
        public virtual SalesCampaign SalesCampaign { get; set; }

    }

    public class SalesCampaignScheduleContact //: BaseModel
    {
        [Key]
        public int SalesCampaignScheduleContactId { get; set; }

        [DisplayName("Contact Name")]
        [Required(ErrorMessage = "Contact Name is required.")]
        [MaxLength(200)]
        public string ContactName { get; set; }

        [DisplayName("Contact Email")]
        [Required(ErrorMessage = "Contact Email is required.")]
        public string ContactEmail { get; set; }

        [Required(ErrorMessage = "Select one category.")]
        public int SalesCampaignScheduleId { get; set; }
        [ForeignKey("SalesCampaignScheduleId")]
        public virtual SalesCampaignSchedule SalesCampaignSchedule { get; set; }
    }
}