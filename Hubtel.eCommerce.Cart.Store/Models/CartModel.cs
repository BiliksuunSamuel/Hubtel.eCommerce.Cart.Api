using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hubtel.eCommerce.Cart.Store.Models
{
    public class CartModel
    {




        [Key]
        public int Id { get; set; }

        [Required,ForeignKey("UserId")]
        public int UserId { get; set; }

        public UserModel User { get; set; }

        [Required]
        public int ItemId { get; set; }

        [Required,MaxLength(100)]
        public string ItemName { get; set; }

        [Required,DefaultValue(1)]
        public int Quantity { get; set; }

        [Required,DefaultValue(1)]
        public decimal UnitPrice { get; set; }

        public DateTime DateAdded { get; set; }
    }
}
