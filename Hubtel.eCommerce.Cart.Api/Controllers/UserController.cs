using Hubtel.eCommerce.Cart.Api.Services;
using Hubtel.eCommerce.Cart.Store.Models;
using Hubtel.eCommerce.Cart.Store.Params;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Hubtel.eCommerce.Cart.Api.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserServices _userServies;
        private readonly AuthServices _authServices;
        public UserController(UserServices userServices, AuthServices authServices)
        {
            _userServies= userServices;
            _authServices= authServices;
        }

        [HttpPost("register")]
        public async Task<JsonResult> Register(UserInput info)
        {
            try
            {
                ResponseModel data = await _userServies.AddUser(info);

                Response.StatusCode = data.Status;
                if (data.Status == StatusCodes.Status200OK)
                {
                    return new JsonResult(new { Data = data.Data, Message = data.Message, access_token = _authServices.GenerateToken(data.Data) });

                }
                else
                {
                    return new JsonResult(data);
                }

            }
            catch (Exception ex)
            {
                Response.StatusCode = StatusCodes.Status500InternalServerError;
                return new JsonResult(ex.Message);
            }

            
        }

        [HttpPost("login")]
        public async Task<JsonResult> Login(LoginParams info)
        {
            try
            {
                ResponseModel data = await _userServies.LoginUser(info);
                Response.StatusCode = data.Status;
                if (data.Status == StatusCodes.Status200OK)
                {
                    return new JsonResult(new { Data = data.Data, Message = data.Message, access_token = _authServices.GenerateToken(data.Data) });

                }
                return new JsonResult(data);
            }
            catch (Exception ex)
            {

                Response.StatusCode = StatusCodes.Status500InternalServerError;
                return new JsonResult(ex.Message);
            }
        }

        [HttpPut()]
        [Authorize]
        public async Task<JsonResult> UpdateUserInfo(UserModel info)
        {
            try
            {
                ResponseModel data = await _userServies.UpdateUserInfo(info);
                Response.StatusCode = data.Status;
                return new JsonResult(data);
            }
            catch (Exception ex)
            {

                Response.StatusCode = StatusCodes.Status500InternalServerError;
                return new JsonResult(ex.Message);
            }
        }

        [HttpPatch]
        [Authorize]
        public async Task<JsonResult> UpdatePassword(PasswordInput info)
        {
            try
            {
                UserModel userInfo = _authServices.ValidateToken(HttpContext);
                userInfo = await _userServies.GetUserByEmail(userInfo.Email);
                if (userInfo == null)
                {
                    Response.StatusCode = StatusCodes.Status403Forbidden;
                    return new JsonResult("Session Expired, Please Login Again");
                }
                ResponseModel data = await _userServies.UpdatePassword(userInfo, info);
                Response.StatusCode= data.Status;
                return new JsonResult(data);
            }
            catch (Exception ex)
            {

                Response.StatusCode = StatusCodes.Status500InternalServerError;
                return new JsonResult(ex.Message);
            }
        }
    }
}
