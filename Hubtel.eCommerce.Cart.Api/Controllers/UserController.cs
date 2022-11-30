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
        public async Task<IActionResult> Register(UserInput info)
        {
            try
            {
                ResponseModel data = await _userServies.AddUser(info);

                if (data.Status == StatusCodes.Status200OK)
                {
                    var res = new  { Data = data.Data, Message = data.Message, access_token = _authServices.GenerateToken(data.Data) };
                    return StatusCode(StatusCodes.Status200OK, res);

                }
                else
                {
                    return StatusCode(data.Status, data);
                }

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

            
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginParams info)
        {
            try
            {
                ResponseModel data = await _userServies.LoginUser(info);
                if (data.Status == StatusCodes.Status200OK)
                {
                    var res=new { Data = data.Data, Message = data.Message, access_token = _authServices.GenerateToken(data.Data) };
                    return StatusCode(data.Status, res);

                }
                return StatusCode(data.Status, data);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut()]
        [Authorize]
        public async Task<IActionResult> UpdateUserInfo(UserModel info)
        {
            try
            {
                ResponseModel data = await _userServies.UpdateUserInfo(info);
                return StatusCode(data.Status, data);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPatch]
        [Authorize]
        public async Task<IActionResult> UpdatePassword(PasswordInput info)
        {
            try
            {
                UserModel userInfo = _authServices.ValidateToken(HttpContext);
                userInfo = await _userServies.GetUserByEmail(userInfo.Email);
                if (userInfo == null)
                {

                    return StatusCode(StatusCodes.Status403Forbidden,"Session Expired, Please Login Again");
                }
                ResponseModel data = await _userServies.UpdatePassword(userInfo, info);
                return new JsonResult(data.Status,data);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
