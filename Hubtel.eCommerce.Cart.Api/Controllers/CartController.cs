using Hubtel.eCommerce.Cart.Api.Services;
using Hubtel.eCommerce.Cart.Store.Models;
using Hubtel.eCommerce.Cart.Store.Params;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;

namespace Hubtel.eCommerce.Cart.Api.Controllers
{
    [Route("api/cart")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly CartServices cartServices;
        private readonly AuthServices _authServices;
        private readonly UserServices _userServices;
        public CartController(CartServices cs,AuthServices authServices,UserServices userServices)
        {
            cartServices = cs;
            _authServices = authServices;
            _userServices= userServices; 
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            UserModel user = _authServices.ValidateToken(HttpContext);
            user = await _userServices.GetUserByEmail(user.Email);
            if (user == null)
            {
                Response.StatusCode = StatusCodes.Status403Forbidden;
                ResponseModel response = new ResponseModel()
                {
                    Message = "Session Expired,Please Login Again",
                    Data = null,
                    Status = StatusCodes.Status403Forbidden
                };
                return StatusCode(response.Status,response);
            }
            ResponseModel data=await cartServices.Get(user);
            Response.StatusCode = data.Status;
            return new JsonResult(data);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Add(CartInput info)
        {
            try
            {
                UserModel user = _authServices.ValidateToken(HttpContext);
                user = await _userServices.GetUserByPhone(user.Phone);
                if(user== null)
                {
                    ResponseModel notFoundData = new ResponseModel()
                    {
                        Data = null,
                        Status = StatusCodes.Status404NotFound,
                        Message = "Account Not Found",
                    };
                    return StatusCode(StatusCodes.Status404NotFound, notFoundData);
                }
                info.UserId = user.UserId;
                ResponseModel data =await cartServices.Add(info);
                return StatusCode(StatusCodes.Status200OK, data);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
               
            }
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> Delete(CartInput item)
        {
            try
            {
                
              ResponseModel data =await cartServices.Delete(item);
                return StatusCode(data.Status, data);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("id")]
        [Authorize]
        public async Task<IActionResult> Find(int id)
        {
            try
            {
                UserModel user = _authServices.ValidateToken(HttpContext);
                user= await _userServices.GetUserByEmail(user.Email);
                if (user == null)
                {
                    Response.StatusCode = StatusCodes.Status403Forbidden;
                    ResponseModel faileData = new ResponseModel()
                    {
                        Message = "Session Expired, Please Login Again",
                        Data = null,
                        Status = StatusCodes.Status403Forbidden
                    };
                    return StatusCode(StatusCodes.Status403Forbidden,faileData);
                }
                ResponseModel data =await cartServices.FindOne(new CartInput()
                {
                    ItemId=id,
                    UserId=user.UserId,
                });
                return StatusCode(data.Status, data);
            }
            catch (Exception ex)
            {

                Response.StatusCode = StatusCodes.Status500InternalServerError;
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}


//##- Yes! You can reply directly to this email and Amen will get it. Just make sure it’s above this line so we can read it right. -##

//Hi Samuel,

//Find below you interview assignment.



//Design an API to be used to unify the e-commerce cart experience for users:
//1.Provide an endpoint to Add items to cart, with specified quantity
//- Adding similar items (same item ID) should increase the quantity - POST
//2. Provide an endpoint to remove an item from cart - DELETE verb
//3. Provide an endpoint list all cart items (with filters => phoneNumbers, time, quantity, item - GET
//4. Provide endpoint to get single item - GET

//Cart model:
//Item ID
//Item name
//Quantity
//Unit price
//Solution Name: Hubtel.eCommerce.Cart
//Project Name: Hubtel.eCommerce.Cart.Api
//Framework: .NET Core 3.1
//Storage: In - memory or RDMS or NOSql You may demonstrate your finished work using
//POSTman or Swagger.



//Extra point will be awarded for authentication 