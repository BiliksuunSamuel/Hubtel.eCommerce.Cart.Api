using Hubtel.eCommerce.Cart.Store.Models;
using Hubtel.eCommerce.Cart.Store.Params;
using Hubtel.eCommerce.Cart.Store.Store;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Hubtel.eCommerce.Cart.Api.Services
{
    public class UserServices:IUserServices
    {
        private readonly StoreContext _store;
        private readonly AuthServices _authServices;
        public UserServices(StoreContext store, AuthServices authServices)
        {
            _store = store;
            _authServices = authServices;
        }
        public async  Task<ResponseModel> AddUser(UserInput info)
        {
            try
            {
                UserModel phoneInfo = await _store.Users.FirstOrDefaultAsync<UserModel>(u => u.Phone == info.Phone);
                if (phoneInfo != null)
                {
                    return new ResponseModel() { Data = null, Status = StatusCodes.Status409Conflict, Message = "Phone Number Already Exist" };
                }
                UserModel emailInfo = await _store.Users.FirstOrDefaultAsync<UserModel>(u => u.Email == info.Email);
                if (emailInfo != null)
                {
                    return new ResponseModel() { Data = null, Message = "Email Address Is Already Registered", Status = StatusCodes.Status409Conflict };
                }
                UserModel newUser = new UserModel()
                {
                    Username = info.Username,
                    Phone = info.Phone,
                    Email = info.Email,
                    DateCreated = DateTime.Now,
                };
                


                var User = await _store.Users.AddAsync(newUser);
                
                await _store.SaveChangesAsync();

                UserModel userInfo = await _store.Users.FirstOrDefaultAsync<UserModel>(u => u.Email == info.Email);
                AuthModel authInfo = new AuthModel()
                {
                    Password = _authServices.HashPassword(info.Password),
                    UserId=userInfo.UserId,
                };
                await _store.Auths.AddAsync(authInfo);
                await _store.SaveChangesAsync();
                return new ResponseModel() { Data =userInfo, Message = "Account Created Successfully", Status = StatusCodes.Status200OK };
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<UserModel> GetUserByEmail(string email)
        {
            try
            {
                UserModel user = await _store.Users.FirstOrDefaultAsync<UserModel>(u => u.Email == email);
                return user;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ResponseModel> LoginUser(LoginParams info)
        {
            try
            {
                UserModel user = await _store.Users.FirstOrDefaultAsync<UserModel>(u => (u.Phone == info.Username || u.Email == info.Username));
                if (user != null)
                 
                {
                    AuthModel authInfo = await _store.Auths.FirstOrDefaultAsync<AuthModel>(au => au.UserId == user.UserId);
                    if(authInfo != null && _authServices.VerifyPassword(info.Password,authInfo.Password)) {
                       
                        return new ResponseModel() { Data = user, Message = null, Status = StatusCodes.Status200OK };
                    }
                    
                }
                
                return new ResponseModel() { Data = null, Message = "Incorrect Username or Password", Status = StatusCodes.Status404NotFound };
                
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ResponseModel> UpdatePassword(UserModel info,PasswordInput passwordInfo)
        {
            try
            {
                AuthModel auth = await _store.Auths.FirstOrDefaultAsync<AuthModel>(a => a.UserId == info.UserId);
                if(auth != null )
                {
                    auth.Password = _authServices.HashPassword(passwordInfo.Password);
                    await _store.SaveChangesAsync();
                    return new ResponseModel()
                    {
                        Data = info,
                        Message = "Ok",
                        Status = StatusCodes.Status200OK
                    };
                }
                return new ResponseModel()
                {
                    Data = null,
                    Message = "Account Not Found",
                    Status = StatusCodes.Status400BadRequest
                };
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ResponseModel> UpdateUserInfo(UserModel info)
        {
            try
            {
                UserModel user = await _store.Users.FirstOrDefaultAsync<UserModel>(u => u.UserId == info.UserId);
                if(user != null)
                {
                    user.Username = info.Username;
                    user.Email = info.Email;
                    user.Phone = info.Phone;
                    await _store.SaveChangesAsync();
                    return new ResponseModel() { Data = user, Message = "Ok", Status = StatusCodes.Status200OK };
                }
                return new ResponseModel()
                {
                    Data = null,
                    Message = "Operation Failed, User Not Found",
                    Status = StatusCodes.Status404NotFound

                };
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
