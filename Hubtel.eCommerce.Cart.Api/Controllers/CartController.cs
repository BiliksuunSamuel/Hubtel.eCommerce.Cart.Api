using Hubtel.eCommerce.Cart.Api.Services;
using Hubtel.eCommerce.Cart.Store.Models;
using Hubtel.eCommerce.Cart.Store.Params;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Hubtel.eCommerce.Cart.Api.Controllers
{
    [Route("api/cart")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly CartServices cartServices;
        public CartController(CartServices cs)
        {
            cartServices = cs;
        }

        [HttpGet]
        
        public JsonResult Get()
        {
            return new JsonResult(cartServices.GetItems());
        }

        [HttpPost]
        [Authorize]
        public JsonResult Add(CartModel info)
        {
            try
            {
                List<CartModel> items = cartServices.AddItem(info);
                return new JsonResult(items);
            }
            catch (Exception ex)
            {
                Response.StatusCode = StatusCodes.Status500InternalServerError;
                return new JsonResult(ex.Message);
               
            }
        }

        [HttpDelete]
        [Authorize]
        public JsonResult Delete(RemoveItem item)
        {
            try
            {

                List<CartModel> items = cartServices.RemoveItem(item);
                return new JsonResult(items);
            }
            catch (Exception ex)
            {

                Response.StatusCode = StatusCodes.Status500InternalServerError;
                return new JsonResult(ex.Message);
            }
        }

        [HttpGet("id")]
        [Authorize]
        public JsonResult Find(int id)
        {
            try
            {

                CartModel item = cartServices.FindItem(id);
                return new JsonResult(item);
            }
            catch (Exception ex)
            {

                Response.StatusCode = StatusCodes.Status500InternalServerError;
                return new JsonResult(ex.Message);
            }
        }
    }
}
