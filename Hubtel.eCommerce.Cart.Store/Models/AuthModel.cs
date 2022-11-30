using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Hubtel.eCommerce.Cart.Store.Models
{
    public class AuthModel
    {
        [Key]
        public int Id { get; set; }

        [Required,MaxLength(255)]
        public string Password { get; set; }

        public int UserId { get; set; }
    }
}
