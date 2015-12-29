using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace lab.aspxapps.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        [DisplayName("Category Name")]
        [Required(ErrorMessage = "Category Name is required")]
        [MaxLength(200)]
        public string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}