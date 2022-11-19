using Hubtel.eCommerce.Cart.Store.Models;
using Hubtel.eCommerce.Cart.Store.Params;
using Hubtel.eCommerce.Cart.Store.Store;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace Hubtel.eCommerce.Cart.Api.Controllers
{
    [Route("api/cart")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly StoreContext store;

        public CartController(StoreContext s)
        {
            store = s;
        }

        [HttpGet("get")]
        
        public JsonResult get()
        {
            return new JsonResult(store.Cart.ToList<CartModel>());
        }

        [HttpPost("add")]
        //[Authorize]
        public JsonResult add(CartModel info)
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
                return new JsonResult(store.Cart.ToList<CartModel>());
            }
            catch (Exception ex)
            {
                Response.StatusCode = StatusCodes.Status500InternalServerError;
                return new JsonResult(ex.Message);
               
            }
        }

        [HttpDelete("remove")]
        [Authorize]
        public JsonResult delete(RemoveItem item)
        {
            try
            {
                CartModel cartItem = store.Cart.FirstOrDefault<CartModel>(c => c.ItemID == item.ItemId);
                if (cartItem != null)
                {
                    store.Cart.Remove(cartItem);
                    store.SaveChanges();
                    return new JsonResult(store.Cart.ToList<CartModel>());
                }
                Response.StatusCode = StatusCodes.Status404NotFound;

                return new JsonResult($"No Item Found For Id={item.ItemId}");
            }
            catch (Exception ex)
            {

                Response.StatusCode = StatusCodes.Status500InternalServerError;
                return new JsonResult(ex.Message);
            }
        }

        [HttpGet("find")]
        [Authorize]
        public JsonResult find(int id)
        {
            try
            {
                CartModel cartItem = store.Cart.FirstOrDefault<CartModel>(c => c.ItemID == id);
                if (cartItem != null)
                {
                    return new JsonResult(cartItem);
                }
                Response.StatusCode = StatusCodes.Status404NotFound;
                return new JsonResult($"No Item Found For Id={id}");
            }
            catch (Exception ex)
            {

                Response.StatusCode = StatusCodes.Status500InternalServerError;
                return new JsonResult(ex.Message);
            }
        }
    }
}
