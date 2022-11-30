using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Hubtel.eCommerce.Cart.Store.Models
{
    public class ProductModel
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public string ItemName { get; set; }

        [Required]
        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public int Status { get; set; }

        public DateTime DateAdded { get; set; }
    }
}
