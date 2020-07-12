using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyLittlePetShop.Models
{
    public class ShoppingItem
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        [Display(Name = "Product")]
        public string Name { get; set; }
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        public ShoppingCategory Category { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int Quantity { get; set; }

    }
}