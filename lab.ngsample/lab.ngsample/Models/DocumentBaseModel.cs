using System;
using System.ComponentModel.DataAnnotations;

namespace lab.ngsample.Models
{
    public class DocumentBaseModel : EntityBaseModel
    {
        [Display(Name = "Document Title")]
        [StringLength(128)]
        public virtual String Name { get; set; }

        [Display(Name = "About The Document")]
        [StringLength(512)]
        public virtual String Description { get; set; }

        [Display(Name = "Upload File")]
        public virtual Byte[] Document { get; set; }

        [Display(Name = "File Name With Extension")]
        [StringLength(256)]
        public virtual String DocumentFileName { get; set; }

        [Display(Name = "File Size")]
        public virtual Int64? DocumentFileSize { get; set; }

        [Display(Name = "Upload Date")]
        [DataType(DataType.Date)]
        public override DateTime CreateDate { get; set; }

    }
}