using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace lab.webapps.Models
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

    public class AppConstant
    {
        public string BaseUrl { get; set; }
        public string ServerPath { get; set; }
        public string DomainName { get; set; }
        public string DomainUrl { get; set; }
        public int DomainId { get; set; }
    }
}