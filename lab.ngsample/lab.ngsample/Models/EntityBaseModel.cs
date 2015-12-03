using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lab.ngsample.Models
{
    public class EntityBaseModel : BaseModel
    {
        [Required(ErrorMessage = "Created date is required")]
        [Display(Name = "Created On")]
        [DataType(DataType.Date)]
        public virtual DateTime CreateDate { get; set; }

        [Required(ErrorMessage = "Created by user is required")]
        [Display(Name = "Created By")]
        [ForeignKey("CreatedByUser")]
        public virtual Int32 CreatedByUserId { get; set; }

        [Display(Name = "Created By")]
        public virtual User CreatedByUser { get; set; }

        [Required(ErrorMessage = "Last update date is required")]
        [Display(Name = "Last Updated On")]
        [DataType(DataType.Date)]
        public DateTime LastUpdateDate { get; set; }

        [Required(ErrorMessage = "Last updated by user is required")]
        [Display(Name = "Last Updated By")]
        [ForeignKey("LastUpdatedByUser")]
        public virtual Int32 LastUpdatedByUserId { get; set; }

        [Display(Name = "Last Updated By")]
        public virtual User LastUpdatedByUser { get; set; }

        [Display(Name = "Created By Company")]
        [ForeignKey("CreatedByCompany")]
        public Int32? CreatedByCompanyId { get; set; }

        [Display(Name = "Created By Company")]
        public virtual Company CreatedByCompany { get; set; }

    }
}