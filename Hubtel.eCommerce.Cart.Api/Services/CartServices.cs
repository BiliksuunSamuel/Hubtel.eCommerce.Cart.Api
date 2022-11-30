using Hubtel.eCommerce.Cart.Store.Models;
using Hubtel.eCommerce.Cart.Store.Params;
using Hubtel.eCommerce.Cart.Store.Store;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hubtel.eCommerce.Cart.Api.Services
{
    public sealed class CartServices:ICartServices
    {
        private readonly StoreContext store;

        public CartServices(StoreContext _store)
        {
            store = _store;
        }

        public async Task<ResponseModel> Add(CartInput info)
        {
            try
            {
                ProductModel product = await store.Products.FirstOrDefaultAsync<ProductModel>(p => p.Id == info.ItemId);
                if (product == null||product.Status<=0)
                {
                    return new ResponseModel()
                    {
                        Data = null,
                        Status = StatusCodes.Status400BadRequest,
                        Message = "Item Unavailable"
                    };
                }
               
                CartModel cartItem=await store.Cart.FirstOrDefaultAsync<CartModel>(c => (c.ItemId == info.ItemId&&c.UserId==info.UserId));
                if (cartItem == null)
                {
                    CartModel newCartItem = new CartModel()
                    {
                        ItemName = info.ItemName,
                        ItemId = info.ItemId,
                        UserId = info.UserId,
                        UnitPrice = info.UnitPrice,
                        Quantity = 1,
                        DateAdded = DateTime.Now,
                    };
                    product.Quantity = product.Quantity - 1;
                    product.Status = product.Quantity <= 0 ? 0 : 1;
                    var itemInfo = await store.Cart.AddAsync(newCartItem);
                    await store.SaveChangesAsync();
                    return new ResponseModel()
                    {
                        Data= newCartItem,
                        Status = StatusCodes.Status200OK,
                        Message="Item Added Successfully"
                    };
                }
                else
                {
                    cartItem.Quantity = cartItem.Quantity + 1;
                    product.Quantity = product.Quantity - 1;
                    product.Status = product.Quantity <= 0 ? 0 : 1;
                    await store.SaveChangesAsync();
                    return new ResponseModel()
                    {
                        Data = cartItem,
                        Status = StatusCodes.Status200OK,
                        Message = "Item Added"
                    };
                }
                
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ResponseModel> Delete(CartInput info)
        {
            try
            {
                CartModel itemInfo=await store.Cart.FirstOrDefaultAsync<CartModel>(c=>(c.ItemId == info.ItemId && c.UserId == info.UserId));
                if (itemInfo == null)
                {
                    return new ResponseModel()
                    {
                        Data=null,
                        Status=StatusCodes.Status404NotFound,
                        Message="Operation Failed,Item Not Found"
                    };
                }
                ProductModel product = await store.Products.FirstOrDefaultAsync<ProductModel>(p => p.Id == info.ItemId);
                product.Quantity = product.Quantity + itemInfo.Quantity;
                product.Status = product.Status == 0 ? 1 : 0;
                store.Cart.Remove(itemInfo);
                await store.SaveChangesAsync();
                return new ResponseModel()
                {
                    Data = itemInfo,
                    Message = "Item Remove Successfully",
                    Status = StatusCodes.Status200OK
                };
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ResponseModel> FindOne(CartInput info)
        {
            try
            {
                CartModel item=await store.Cart.FirstOrDefaultAsync<CartModel>(c=>(c.ItemId == info.ItemId && c.UserId == info.UserId));
                return new ResponseModel()
                {
                    Data=item,
                    Status=item==null?StatusCodes.Status404NotFound:StatusCodes.Status200OK,
                    Message=item==null?"Item Not Found!":"Ok"
                };
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ResponseModel> Get( UserModel user)
        {
            try
            {
                List<CartModel>data=await store.Cart.Include(c=>c.User).Where<CartModel>(c=>c.UserId==user.UserId).ToListAsync<CartModel>();
                return new ResponseModel()
                {
                    Data=data,
                    Message="Ok",
                    Status = StatusCodes.Status200OK
                };
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
