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

    public class Category : BaseModel
    {
        [Key]
        public int CategoryId { get; set; }
        [DisplayName("Category Name")]
        [Required(ErrorMessage = "Category Name is required")]
        [MaxLength(200)]
        public string Name { get; set; }
    }

    public class Product : BaseModel
    {
        [Key]
        public int ProductId { get; set; }
        [DisplayName("Product Name")]
        [Required(ErrorMessage = "Product Name is required.")]
        [MaxLength(200)]
        public string Name { get; set; }
        [DisplayName("Product Price")]
        [Required(ErrorMessage = "Product Price is required.")]
        public float Price { get; set; }

        [Required(ErrorMessage = "Select one category.")]
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]

        public virtual Category Category { get; set; }
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