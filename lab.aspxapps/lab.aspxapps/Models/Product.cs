using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace lab.aspxapps.Models
{
    public class Product
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
}