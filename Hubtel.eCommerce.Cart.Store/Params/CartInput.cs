using Hubtel.eCommerce.Cart.Store.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Text;

namespace Hubtel.eCommerce.Cart.Store.Params
{
    public class CartInput
    {
        public int UserId { get; set; }

        [Required]
        public int ItemId { get; set; }

        [Required]
        public string ItemName { get; set; }


        [Required]
        public decimal UnitPrice { get; set; }

    }
}
