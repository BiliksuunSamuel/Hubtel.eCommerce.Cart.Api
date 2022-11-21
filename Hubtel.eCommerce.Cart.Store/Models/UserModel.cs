using System;
using System.ComponentModel.DataAnnotations;

namespace Hubtel.eCommerce.Cart.Store.Models
{
    public class UserModel
    {
        [Key]
        public int Id { get; set; }

        [Required,MaxLength(10)]
        public string Username { get; set; }

        [Required]
        public int Role { get; set; }

        [Required,MaxLength(150)]

        public string Email { get; set; }
    }
}
