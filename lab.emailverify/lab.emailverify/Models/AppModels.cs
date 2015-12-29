using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace lab.emailverify.Models
{
    public enum EmailVerificationLevel
    {
        NotChecked,
        SynctexValidated,
        DEAValidated,
        DNSValidated,
        SMTPValidated,
        MailboxValidated,
        CatchAllValidated
    }

    public class BaseModel
    {
        [NotMapped]
        public virtual string SuccessMessage { get; set; }

        [NotMapped]
        public virtual string ErrorMessage { get; set; }

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

    public class EmailVerification : BaseModel
    {
        [Required]
        [Display(AutoGenerateField = false)]
        [Key]
        public Int32 EmailVerificationId { get; set; }
        [Required]
        public String EmailAddress { get; set; }
        public Boolean IsSynctexValidated { get; set; }
        public Boolean IsRoleAccountValidated { get; set; }
        public Boolean IsIspSpecificSyntaxValidated { get; set; }
        public Boolean IsDEADomainValidated { get; set; }
        public Boolean IsDNSValidated { get; set; }
        public Boolean IsDEAMailExchangerValidated { get; set; }
        public Boolean IsSMTPValidated { get; set; }
        public Boolean IsMailboxValidated { get; set; }
        public Boolean IsCatchAllValidated { get; set; }
        public Boolean IsPositive { get; set; }
        public Boolean IsVerified { get; set; }
        public Int32 VerificationLevel { get; set; }
        public String LastStatus { get; set; }
        public virtual EmailVerificationLevel EmailVerificationLevel { get { return (EmailVerificationLevel)VerificationLevel; } }
    }

}