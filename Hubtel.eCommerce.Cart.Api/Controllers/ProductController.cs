using Hubtel.eCommerce.Cart.Api.Services;
using Hubtel.eCommerce.Cart.Store.Models;
using Hubtel.eCommerce.Cart.Store.Params;
using Hubtel.eCommerce.Cart.Store.Store;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Hubtel.eCommerce.Cart.Api.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductServices _productServices;

        public ProductController(ProductServices productServices)
        {
            _productServices= productServices;
        }

        [HttpGet]
        public async Task<JsonResult> Get()
        {
            try
            {
                ResponseModel results = await _productServices.Get();
                Response.StatusCode = results.Status;
                return new JsonResult(results);
            }
            catch (Exception ex)
            {

                Response.StatusCode = StatusCodes.Status500InternalServerError;
                return new JsonResult(ex.Message);
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<JsonResult>Add(ProductInput info)
        {
            try
            {
                ResponseModel res=await _productServices.Add(info);
                Response.StatusCode = res.Status;
                return new JsonResult(res);
            }
            catch (Exception ex)
            {

                Response.StatusCode = StatusCodes.Status500InternalServerError;
                return new JsonResult(ex.Message);
            }
        }

        [HttpDelete]
        [Authorize]
        public async Task<JsonResult>Delete(int id)
        {
            try
            {

                ResponseModel res=await _productServices.Delete(id);
                Response.StatusCode = res.Status;
                return new JsonResult(res);
            }
            catch (Exception ex)
            {

                Response.StatusCode = StatusCodes.Status500InternalServerError;
                return new JsonResult(ex.Message);
            }
        }

        [HttpPut]
        [Authorize]
        public async Task<JsonResult> Update(ProductModel info)
        {
            try
            {
                ResponseModel data=await _productServices.Update(info);
                Response.StatusCode = data.Status;
                return new JsonResult(data);
            }
            catch (Exception ex)
            {

                Response.StatusCode = StatusCodes.Status500InternalServerError;
                return new JsonResult(ex.Message);
            }
        }

        [HttpGet("id")]
        public async Task<JsonResult>FindOne(int id)
        {
            try
            {
                ResponseModel data=await _productServices.FindById(id);
                Response.StatusCode = data.Status;
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
