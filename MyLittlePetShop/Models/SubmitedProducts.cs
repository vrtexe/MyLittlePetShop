using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyLittlePetShop.Models
{
    public class SubmitedProducts
    {
        [Key]
        public string UserId { get; set; }
        public virtual List<ShoppingItem> Products { get; set; }
        public SubmitedProducts()
        {
            Products = new List<ShoppingItem>();
        }
    }
}