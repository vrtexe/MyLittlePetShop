using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyLittlePetShop.Models
{
    public class ShoppingCartItems
    {
        [Key]
        [StringLength(128)]
        public string UserID { get; set; }
        public List<int> Quantity { get; set; }
        public virtual List<ShoppingItem> items { get; set; }
        public ShoppingCartItems(string userID)
        {
            items = new List<ShoppingItem>();
            Quantity = new List<int>();
            UserID = userID;
        }
        public ShoppingCartItems()
        {
            Quantity = new List<int>();
            items = new List<ShoppingItem>();
        }
    }
}