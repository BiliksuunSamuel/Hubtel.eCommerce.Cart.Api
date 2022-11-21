using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Hubtel.eCommerce.Cart.Store.Models
{
    public class CartModel
    {




        [Key]
        public int ItemID { get; set; }

        [Required,MaxLength(100)]
        public string ItemName { get; set; }

        [Required,DefaultValue(1)]
        public int Quantity { get; set; }

        [Required,DefaultValue(1)]
        public decimal UnitPrice { get; set; }
    }
}
