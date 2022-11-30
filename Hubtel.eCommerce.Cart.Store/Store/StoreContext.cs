using Hubtel.eCommerce.Cart.Store.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Hubtel.eCommerce.Cart.Store.Store
{
    public sealed class StoreContext:DbContext
    {
        private readonly IConfiguration _configuration;



        public DbSet<CartModel>  Cart { get; set; }
        public DbSet<ProductModel>  Products { get; set; }
        public DbSet<UserModel>  Users { get; set; }
        public DbSet<AuthModel>  Auths { get; set; }



        //
        public StoreContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Connection());
        }

        public string Connection(string key = "default")
        {
            return _configuration.GetConnectionString(key);
        }
    }
}
