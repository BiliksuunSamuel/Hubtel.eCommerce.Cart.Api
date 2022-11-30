using Hubtel.eCommerce.Cart.Store.Models;
using Hubtel.eCommerce.Cart.Store.Params;
using Hubtel.eCommerce.Cart.Store.Store;
using System.Threading.Tasks;

namespace Hubtel.eCommerce.Cart.Api.Services
{
    public interface IProductServices
    {

        public Task<ResponseModel> Add(ProductInput info);

        public Task<ResponseModel> Get();

        public Task<ResponseModel> Update(ProductModel info);

        public Task<ResponseModel>Delete(int id);

        public Task<ResponseModel> FindById(int id);
       
    }
}
