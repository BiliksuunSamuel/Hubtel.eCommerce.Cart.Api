using Hubtel.eCommerce.Cart.Store.Models;
using Hubtel.eCommerce.Cart.Store.Params;
using System.Collections.Generic;

namespace Hubtel.eCommerce.Cart.Api.Services
{
    public interface ICartServices
    {
        public List<CartModel> GetItems();
        public List<CartModel> AddItem(CartModel info);
        public List<CartModel> RemoveItem(RemoveItem item);
        public CartModel FindItem(int id);
    }
}