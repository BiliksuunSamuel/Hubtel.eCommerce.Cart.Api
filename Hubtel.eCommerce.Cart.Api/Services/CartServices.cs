using Hubtel.eCommerce.Cart.Store.Models;
using Hubtel.eCommerce.Cart.Store.Params;
using Hubtel.eCommerce.Cart.Store.Store;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hubtel.eCommerce.Cart.Api.Services
{
    public sealed class CartServices:ICartServices
    {
        private readonly StoreContext store;

        public CartServices(StoreContext _store)
        {
            store = _store;
        }

        public List<CartModel> GetItems()
        {
            return store.Cart.ToList<CartModel>();
        }

        public List<CartModel> AddItem(CartModel info)
        {
            try
            {
                CartModel item = store.Cart.FirstOrDefault<CartModel>(c => c.ItemID == info.ItemID);
                if (item != null)
                {
                    item.Quantity = item.Quantity + 1;
                    store.SaveChanges();
                }
                else
                {
                    info.Quantity = info.Quantity == 0 ? 1 : info.Quantity;
                    store.Cart.Add(info);
                    store.SaveChanges();
                }
                return store.Cart.ToList<CartModel>();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<CartModel> RemoveItem(RemoveItem item)
        {
            try
            {
                CartModel cartItem = store.Cart.FirstOrDefault<CartModel>(c => c.ItemID == item.ItemId);
                if (cartItem != null)
                {
                    store.Cart.Remove(cartItem);
                    store.SaveChanges();
                    return store.Cart.ToList<CartModel>();
                }
                else
                {
                    throw new Exception($"No Item Found For Id={item.ItemId}");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public CartModel FindItem(int id)
        {
            try
            {
                CartModel cartItem = store.Cart.FirstOrDefault<CartModel>(c => c.ItemID == id);
                if (cartItem != null)
                {
                    return cartItem;
                }
                else
                {
                    throw new Exception($"No Item Found For Id={id}");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
