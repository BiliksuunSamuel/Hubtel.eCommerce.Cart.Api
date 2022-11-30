using Hubtel.eCommerce.Cart.Store.Models;
using Hubtel.eCommerce.Cart.Store.Params;
using System.Threading.Tasks;

namespace Hubtel.eCommerce.Cart.Api.Services
{
    public interface IUserServices
    {

        public Task<ResponseModel> AddUser(UserInput info);

        public Task<ResponseModel> LoginUser(LoginParams info);

        public Task<UserModel> GetUserByEmail(string email);
        public Task<UserModel> GetUserByPhone(string phone);

        public Task<ResponseModel> UpdateUserInfo(UserModel info);

        public Task<ResponseModel> UpdatePassword(UserModel info,PasswordInput passwordInfo);

    }
}
