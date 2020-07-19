using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyLittlePetShop.Models
{
    public class ShoppingCategory
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Category")]
        public string Name { get; set; }
        public string Image { get; set; }
        //List<ShoppingItem> items { get; set; }
    }
}