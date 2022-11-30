using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Hubtel.eCommerce.Cart.Store.Params
{
    public class UserInput
    {
        [Required,MaxLength(10),MinLength(10)]
        public string Phone { get; set; }

        [Required]
        public string Username { get; set; }


        [Required, MaxLength(150)]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
