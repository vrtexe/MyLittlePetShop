using System.ComponentModel.DataAnnotations;

namespace MyLittlePetShop.Models
{
    public class BuyerInfo
    {
        public ApplicationUser User { get; set; }
        public int Quantity { get; set; }
        public BuyerInfo()
        {

        }
    }
}