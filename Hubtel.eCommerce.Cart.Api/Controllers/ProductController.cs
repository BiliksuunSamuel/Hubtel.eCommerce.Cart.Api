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
        public async Task<IActionResult> Get()
        {
            try
            {
                ResponseModel results = await _productServices.Get();
                return StatusCode(results.Status, results);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult>Add(ProductInput info)
        {
            try
            {
                ResponseModel res=await _productServices.Add(info);
                return StatusCode(res.Status, res);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult>Delete(int id)
        {
            try
            {

                ResponseModel res=await _productServices.Delete(id);

                return StatusCode(res.Status, res);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Update(ProductModel info)
        {
            try
            {
                ResponseModel data=await _productServices.Update(info);
                
                return StatusCode((int)data.Status, data);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("id")]
        public async Task<IActionResult>FindOne(int id)
        {
            try
            {
                ResponseModel data=await _productServices.FindById(id);
                
                return StatusCode(data.Status, data);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
