using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _24hplusdotnetcore.Models;
using _24hplusdotnetcore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace _24hplusdotnetcore.Controllers
{
    [ApiController]
    [Authorize]
    public class ProductCategoryController : ControllerBase
    {
        private readonly ILogger<ProductCategoryController> _logger;
        private readonly ProductCategoryServices _productCategoryServices;
        public ProductCategoryController(ILogger<ProductCategoryController> logger, ProductCategoryServices productCategoryServices)
        {
            _logger = logger;
            _productCategoryServices = productCategoryServices;
        }
        [HttpGet]
        [Route("api/productcategories")]
        public ActionResult<List<ProductCategory>> Get()
        {
            var lstProductCategory = new List<ProductCategory>();
            try
            {
                lstProductCategory = _productCategoryServices.GetProductCategories();
                return Ok(lstProductCategory);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseMessage { status = "ERROR " + StatusCodes.Status500InternalServerError, message = ex.Message });
            }
        }
        [HttpGet]
        [Route("api/productcategory/{MaLoaiSanPham}")]
        public ActionResult<ProductCategory> Get(string MaLoaiSanPham)
        {
            var objProductCategory = new ProductCategory();
            try
            {
                objProductCategory = _productCategoryServices.GetProductCategory(MaLoaiSanPham);
                return Ok(objProductCategory);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseMessage { status = "ERROR " + StatusCodes.Status500InternalServerError, message = ex.Message });
            }
        }
        [HttpPost]
        [Route("api/productcategory")]
        public ActionResult<ProductCategory> Create(ProductCategory productCategory)
        {
            try
            {
                _productCategoryServices.Create(productCategory);
                return Ok(productCategory);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseMessage { status = "ERROR " + StatusCodes.Status500InternalServerError, message = ex.Message });
            }
        }
    }
}