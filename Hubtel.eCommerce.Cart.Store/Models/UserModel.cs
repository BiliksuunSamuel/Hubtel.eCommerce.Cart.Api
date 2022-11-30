using System;
using System.ComponentModel.DataAnnotations;

namespace Hubtel.eCommerce.Cart.Store.Models
{
    public class UserModel
    {
        [Key]
        public int UserId { get; set; }

        [Required,MaxLength(50)]
        public string Username { get; set; }

        [Required,MaxLength(150)]

        public string Email { get; set; }

        [Required,MaxLength(10),MinLength(10)]
        public string Phone { get; set; }

        public DateTime DateCreated { get; set; }

    }
}
