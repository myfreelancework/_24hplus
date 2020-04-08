using System;
using System.Collections.Generic;
using _24hplusdotnetcore.Models;
using _24hplusdotnetcore.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace _24hplusdotnetcore.Controllers
{
    [ApiController]
    public class LoaiCVController : ControllerBase
    {
        private readonly ILogger<LoaiCVController> _logger;
        private readonly LoaiCVServices _loaiCVServices;
        public LoaiCVController(ILogger<LoaiCVController> logger, LoaiCVServices loaiCVServices)
        {
            _logger = logger;
            _loaiCVServices = loaiCVServices;
        }
        [HttpGet]
        [Route("api/loaicv")]
        public ActionResult<ResponseContext> GetList()
        {
            try
            {
                if ((bool)HttpContext.Items["isLoggedInOtherDevice"])
                    return Ok(new ResponseContext
                    {
                        code = (int)Common.ResponseCode.IS_LOGGED_IN_ORTHER_DEVICE,
                        message = Common.Message.IS_LOGGED_IN_ORTHER_DEVICE,
                        data = null
                    });
                var lstLoaicv = new List<LoaiHinhCongViec>();
                lstLoaicv = _loaiCVServices.GetList();
                return Ok(new ResponseContext
                {
                    code = (int)Common.ResponseCode.SUCCESS,
                    message = Common.Message.SUCCESS,
                    data = lstLoaicv
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseMessage
                {
                    status = "ERROR",
                    message = ex.Message
                });
            }
        }
        [HttpGet]
        [Route("api/loaicv/{MaLoaiCV}")]
        public ActionResult<ResponseContext> GetLoaiCV(string MaLoaiCV)
        {
            try
            {
                if ((bool)HttpContext.Items["isLoggedInOtherDevice"])
                    return Ok(new ResponseContext
                    {
                        code = (int)Common.ResponseCode.IS_LOGGED_IN_ORTHER_DEVICE,
                        message = Common.Message.IS_LOGGED_IN_ORTHER_DEVICE,
                        data = null
                    });
                var objLoaiCV = new LoaiHinhCongViec();
                objLoaiCV = _loaiCVServices.GetLoaiHinhCongViec(MaLoaiCV);
                return Ok(new ResponseContext
                {
                    code = (int)Common.ResponseCode.SUCCESS,
                    message = Common.Message.SUCCESS,
                    data = objLoaiCV
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseMessage
                {
                    status = "ERROR",
                    message = ex.Message
                });
            }
        }
    }
}