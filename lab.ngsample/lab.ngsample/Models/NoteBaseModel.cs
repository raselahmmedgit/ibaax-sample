using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations.Schema;

namespace lab.ngsample.Models
{
    public class NoteBaseModel : EntityBaseModel
    {
        [Display(Name = "Written By")]
        [Required]
        public override Int32 CreatedByUserId { get; set; }

        [Display(Name = "Written On")]
        [DataType(DataType.DateTime)]
        public override DateTime CreateDate { get; set; }

        [Required(ErrorMessage = "Notes is required")]
        [Display(Name = "Notes")]
        [StringLength(512)]
        [DataType(DataType.Html)]
        [AllowHtml]
        public virtual String Notes { get; set; }

        #region NotMapped
        [NotMapped]
        public string PhotoFileName { get; set; }
        [NotMapped]
        public int Id { get; set; }
        [NotMapped]
        public int NoteId { get; set; }
        [NotMapped]
        public int ParentModelId { get; set; }
        [NotMapped]
        public string ModelName { get; set; }
        [NotMapped]
        public double DaysBefore { get; set; }
        [NotMapped]
        public string Difference { get; set; }
        #endregion
        
    }
}