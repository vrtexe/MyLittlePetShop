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
        public int Id { get; set; }
        public int UserId { get; set; }
        public List<ShoppingItem> Products { get; set; }
    }
}