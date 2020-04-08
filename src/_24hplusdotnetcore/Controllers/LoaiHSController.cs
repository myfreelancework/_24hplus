using System.Collections.Generic;
using _24hplusdotnetcore.Models;
using _24hplusdotnetcore.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace _24hplusdotnetcore.Controllers
{
    [ApiController]
    public class LoaiHSController: ControllerBase
    {
        private readonly ILogger<LoaiHSController> _logger;
        private readonly LoaiHoSoServices _loaiHSServices;
        public LoaiHSController(ILogger<LoaiHSController> logger, LoaiHoSoServices loaiHS)
        {
            _loaiHSServices = loaiHS;
            _logger = logger;
        }
        [HttpGet]
        [Route("api/loaihoso")]
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
                var lstLoaiHS = new List<LoaiHoSo>();
                lstLoaiHS = _loaiHSServices.GetList();
                return Ok(new ResponseContext{
                    code = (int)Common.ResponseCode.SUCCESS,
                    message = Common.Message.SUCCESS,
                    data = lstLoaiHS
                });
            }
            catch (System.Exception ex)
            {
               _logger.LogError(ex, ex.Message);
               return StatusCode(StatusCodes.Status500InternalServerError, new ResponseMessage{
                   status = "ERROR",
                   message = ex.Message
               });
            }
        }
        [HttpGet]
        [Route("api/loaihoso/{MaLoaiHS}")]
        public ActionResult<ResponseContext> GetLoaiHSByMaLoaiHS( string MaLoaiHS)
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
                var objLoaiHS = new LoaiHoSo();
                objLoaiHS = _loaiHSServices.GetLoaiHobyMaHS(MaLoaiHS);
                return Ok(new ResponseContext{
                    code = (int)Common.ResponseCode.SUCCESS,
                    message = Common.Message.SUCCESS,
                    data = objLoaiHS
                });
            }
            catch (System.Exception ex)
            {
               _logger.LogError(ex, ex.Message);
               return StatusCode(StatusCodes.Status500InternalServerError, new ResponseMessage{
                   status = "ERROR",
                   message = ex.Message
               });
            }
        }
    }
}