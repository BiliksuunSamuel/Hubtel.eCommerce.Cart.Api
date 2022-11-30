using Hubtel.eCommerce.Cart.Store.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace Hubtel.eCommerce.Cart.Api.Services
{
    public interface IAuthService
    {
        public string GenerateToken(UserModel user);
        public UserModel ValidateToken(HttpContext context);

        public string HashPassword(string password);

        public Boolean VerifyPassword(string password, string hPassword);
    }
}
