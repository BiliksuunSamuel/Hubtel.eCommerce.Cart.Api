using Hubtel.eCommerce.Cart.Store.Models;
using Hubtel.eCommerce.Cart.Store.Params;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hubtel.eCommerce.Cart.Api.Services
{
    public interface ICartServices
    {
        public Task<ResponseModel> Add(CartInput info);
        public Task<ResponseModel> Get(UserModel userInfo);

        public Task<ResponseModel> FindOne(CartInput info);

        public Task<ResponseModel> Delete(CartInput info);
       
    }
}