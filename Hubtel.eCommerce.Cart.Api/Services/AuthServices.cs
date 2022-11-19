using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace Hubtel.eCommerce.Cart.Api.Services
{
    public class AuthServices : IAuthService
    {
        private readonly IConfiguration configuration;


        public AuthServices(IConfiguration _config)
        {
            configuration = _config;
        }
        public string GenerateToken()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,""),
                new Claim(ClaimTypes.Email,""),
                new Claim(ClaimTypes.Role,""),
            };
            var token = new JwtSecurityToken(configuration["Jwt:Issuer"], configuration["Jwt:Audience"], claims, expires: DateTime.Now.AddHours(8), signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public dynamic ValidateToken(HttpContext context)
        {
            try
            {
                ClaimsIdentity identity = context.User.Identity as ClaimsIdentity;
                if (identity !=null)
                {
                    var claim = identity.Claims;
                    var role = claim.FirstOrDefault(o => o.Type == ClaimTypes.Role)?.Value;

                    var user = new
                    {
                        Name = claim.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value,
                        Role = role !=null? int.Parse(role!) : 0,
                        Email = claim.FirstOrDefault(o => o.Type == ClaimTypes.Email)?.Value,
                    };
                    return user;
                }
                throw new Exception("Unauthorized");
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
