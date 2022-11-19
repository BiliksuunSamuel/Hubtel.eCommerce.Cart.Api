using Microsoft.AspNetCore.Http;

namespace Hubtel.eCommerce.Cart.Api.Services
{
    public interface IAuthService
    {
        public string GenerateToken();
        public dynamic ValidateToken(HttpContext context);
    }
}
