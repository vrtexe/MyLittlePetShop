using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyLittlePetShop.Models
{
    public class UserToRole
    {
        public string UserEmail { get; set; }
        public int RoleId { get; set; }
    }
}