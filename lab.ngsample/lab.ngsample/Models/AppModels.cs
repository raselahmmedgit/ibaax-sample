using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lab.ngsample.Models
{
    public class User : BaseModel
    {
        [Key]
        public int UserId { get; set; }

        [DisplayName("User Name")]
        [Required(ErrorMessage = "User Name is required")]
        [MaxLength(200)]
        public string UserName { get; set; }

        [DisplayName("User Password")]
        [Required(ErrorMessage = "User Password is required")]
        [MaxLength(200)]
        public string Password { get; set; }
    }

    public class Contact : BaseModel
    {
        [Key]
        public int ContactId { get; set; }

        [DisplayName("First Name")]
        [Required(ErrorMessage = "First Name is required")]
        [MaxLength(200)]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        [Required(ErrorMessage = "Last Name is required")]
        [MaxLength(200)]
        public string LastName { get; set; }

        [Display(Name = "Mobile")]
        [StringLength(64)]
        public String Mobile { get; set; }

        [StringLength(10)]
        public String MobileCode { get; set; }

        [Display(Name = "Email Address")]
        [StringLength(128)]
        public String EmailAddress { get; set; }

        [Display(Name = "Address")]
        [StringLength(256)]
        public String Address { get; set; }
    }

    public class ContactDocumentCategory : EntityBaseModel
    {
        [Required]
        [Display(AutoGenerateField = false)]
        [Key]
        public Int32 ContactDocumentCategoryId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "Name")]
        [StringLength(128)]
        public String Name { get; set; }
    }

    public class ContactDocument : DocumentBaseModel
    {
        [Required]
        [Display(AutoGenerateField = false)]
        [Key]
        public Int32 ContactDocumentId { get; set; }

        [ForeignKey("Contact")]
        public Int32? ContactId { get; set; }

        [Display(Name = "Primary Resume")]
        public Boolean IsPrimary { get; set; }

        [Display(Name = "Document Category")]
        [ForeignKey("ContactDocumentCategory")]
        public Int32? ContactDocumentCategoryId { get; set; }

        public string FileType { get; set; }

        public virtual Contact Contact { get; set; }
        public virtual ContactDocumentCategory ContactDocumentCategory { get; set; }

        #region NotMapped
        [NotMapped]
        public string ContactName { get; set; }
        [NotMapped]
        public string ContactDocumentCategoryName { get; set; }
        #endregion Custom Fields
    }

    public class Company : BaseModel
    {
        [Key]
        public int CompanyId { get; set; }

        [DisplayName("First Name")]
        [Required(ErrorMessage = "First Name is required")]
        [MaxLength(200)]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        [Required(ErrorMessage = "Last Name is required")]
        [MaxLength(200)]
        public string LastName { get; set; }

        [Display(Name = "Mobile")]
        [StringLength(64)]
        public String Mobile { get; set; }

        [StringLength(10)]
        public String MobileCode { get; set; }

        [Display(Name = "Email Address")]
        [StringLength(128)]
        public String EmailAddress { get; set; }

        [Display(Name = "Address")]
        [StringLength(256)]
        public String Address { get; set; }
    }

    public class CompanyDocumentCategory : EntityBaseModel
    {
        [Required]
        [Display(AutoGenerateField = false)]
        [Key]
        public Int32 CompanyDocumentCategoryId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "Name")]
        [StringLength(128)]
        public String Name { get; set; }

    }

    public class CompanyDocument : DocumentBaseModel
    {
        [Required]
        [Display(AutoGenerateField = false)]
        [Key]
        public Int32 CompanyDocumentId { get; set; }

        [Required]
        [ForeignKey("Company")]
        public Int32 CompanyID { get; set; }

        [Display(Name = "Document Category")]
        [ForeignKey("CompanyDocumentCategory")]
        public Int32? CompanyDocumentCategoryId { get; set; }

        public virtual Company Company { get; set; }

        public virtual CompanyDocumentCategory CompanyDocumentCategory { get; set; }

        #region NotMapped

        [NotMapped]
        public string CompanyName { get; set; }

        [NotMapped]
        public string CompanyDocumentCategoryName { get; set; }

        #endregion NotMapped
    }
}