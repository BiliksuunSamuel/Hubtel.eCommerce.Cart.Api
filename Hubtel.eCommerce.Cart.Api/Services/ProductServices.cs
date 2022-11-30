using Hubtel.eCommerce.Cart.Store.Models;
using Hubtel.eCommerce.Cart.Store.Params;
using Hubtel.eCommerce.Cart.Store.Store;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hubtel.eCommerce.Cart.Api.Services
{
    public class ProductServices : IProductServices
    {
        private readonly StoreContext _storeContext;

        public ProductServices(StoreContext storeContext)
        {
            _storeContext= storeContext;
        }
        public async Task<ResponseModel> Add(ProductInput info)
        {
            try
            {
                ProductModel item = new ProductModel()
                {
                    ItemName = info.ItemName,
                    Status = 1,
                    DateAdded = DateTime.Now,
                    Price = info.Price,
                    Quantity = info.Quantity,
                };
                var product= await _storeContext.Products.AddAsync(item);
                await _storeContext.SaveChangesAsync();
                return new ResponseModel()
                {
                    Data =item ,
                    Message = "Product Added Successfully",
                    Status = StatusCodes.Status200OK
                };
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ResponseModel> Delete(int id)
        {
            try
            {
                ProductModel product = await _storeContext.Products.FirstOrDefaultAsync<ProductModel>(p => p.Id == id);
                if (product != null)
                {
                    _storeContext.Products.Remove(product);
                    await _storeContext.SaveChangesAsync();
                    return new ResponseModel()
                    {
                        Data = product,
                        Message = "Product Deleted Successfully",
                        Status = StatusCodes.Status200OK
                    };
                }
                return new ResponseModel()
                {
                    Data = null,
                    Message = "Product Not found",
                    Status = StatusCodes.Status404NotFound,
                };
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ResponseModel> FindById(int id)
        {
            try
            {
                ProductModel product = await _storeContext.Products.FirstOrDefaultAsync<ProductModel>(p => p.Id == id);
                return new ResponseModel()
                {
                    Data=product,
                    Status=product!=null?StatusCodes.Status200OK:StatusCodes.Status404NotFound,
                    Message=product!=null?"Ok":$"No Product Found For Id={id}"
                };
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ResponseModel> Get()
        {
            try
            {
                List<ProductModel> data = await _storeContext.Products.ToListAsync<ProductModel>();
                return new ResponseModel()
                {
                    Data=data,
                    Message="Ok",
                    Status=StatusCodes.Status200OK
                };
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ResponseModel> Update(ProductModel info)
        {
            try
            {
                ProductModel product = await _storeContext.Products.FirstOrDefaultAsync<ProductModel>(p => p.Id == info.Id);
                if (product != null)
                {
                    product.ItemName = info.ItemName;
                    product.Quantity = info.Quantity;
                    product.Price = info.Price;
                    product.Status= info.Status;
                    await _storeContext.SaveChangesAsync();
                    return new ResponseModel()
                    {
                        Data=product,
                        Message="Update Successfull",
                        Status=StatusCodes.Status200OK
                    };
                }
                return new ResponseModel()
                {
                    Data=info,
                    Message="Product Not Updated, Invalid Details",
                    Status=StatusCodes.Status400BadRequest
                };
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
