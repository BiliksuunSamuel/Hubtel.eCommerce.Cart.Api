using Hubtel.eCommerce.Cart.Store.Models;
using Microsoft.AspNetCore.Http;

namespace Hubtel.eCommerce.Cart.Api.Services
{
    public interface IAuthService
    {
        public string GenerateToken(UserModel user);
        public UserModel ValidateToken(HttpContext context);
    }
}
