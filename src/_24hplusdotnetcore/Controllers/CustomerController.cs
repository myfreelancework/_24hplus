using System;
using System.Collections.Generic;
using _24hplusdotnetcore.Models;
using _24hplusdotnetcore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace _24hplusdotnetcore.Controllers
{
    [ApiController]
    
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly CustomerServices _customerServices;

        public CustomerController(ILogger<CustomerController> logger, CustomerServices customerServices)
        {
            _logger = logger;
            _customerServices = customerServices;
        }
        
        [HttpGet]
        [Route("api/customers")]
        public ActionResult<ResponseContext> GetCustomerList([FromQuery] string username,[FromQuery] DateTime? datefrom, [FromQuery] DateTime? dateto,[FromQuery] string status, [FromQuery] int? pagenumber, [FromQuery] int? pagesize)
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
                var lstCustomers = new List<Customer>();
                lstCustomers = _customerServices.GetList(username, datefrom, dateto, status, pagenumber,pagesize) ;
                var datasizeInfo = _customerServices.CustomerPagesize(lstCustomers);
                return Ok(new CustomerDataResponse
                {
                    code = (int)Common.ResponseCode.SUCCESS,
                    message = Common.Message.SUCCESS,
                    data = lstCustomers,
                    pagenumber = pagenumber.HasValue? (int)pagenumber : 1,
                    totalrecord = datasizeInfo[0],
                    totalpage = datasizeInfo[1]
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseMessage {status = "ERROR", message = ex.Message});
            }
        }
        [HttpGet]
        [Route("api/customer/{Id}")]
        public ActionResult<ResponseContext> GetCustomer(string Id)
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
                var objCustomer = new Customer();
                objCustomer = _customerServices.GetCustomer(Id);
                return Ok(new ResponseContext
                {
                    code = (int)Common.ResponseCode.SUCCESS,
                    message = Common.Message.SUCCESS,
                    data = objCustomer
                });
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseMessage { status = "ERROR", message = ex.Message });
            }
        }
        [HttpGet]
        [Route("api/customer/getcustomerbyusername")]
        public ActionResult<ResponseContext> GetCustomerByUserName([FromQuery]string username, [FromQuery] int? pagenumber) 
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
                var lstCustomers = new List<Customer>();
                lstCustomers = _customerServices.GetCustomerByUserName(username,pagenumber);
                return Ok(new ResponseContext
                {
                    code = (int)Common.ResponseCode.SUCCESS,
                    message = Common.Message.SUCCESS,
                    data = lstCustomers,
                    Paging = _customerServices.CustomerPagesize(lstCustomers)[1]
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseMessage { status = "ERROR", message = ex.Message });
            }
        }

        [HttpPost]
        [Route("api/customer")]
        public ActionResult<ResponseContext> Create(Customer customer)
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
                var newCustomer = new Customer();
                newCustomer =  _customerServices.CreateCustomer(customer);
                return Ok(new ResponseContext
                {
                    code = (int)Common.ResponseCode.SUCCESS,
                    message = Common.Message.SUCCESS,
                    data = newCustomer
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseMessage { status = "ERROR", message = ex.Message });
            }
        }
        [HttpPost]
        [Route("api/customer/update")]
        public ActionResult<ResponseContext> Update(Customer customer)
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
                long updateCount = _customerServices.UpdateCustomer(customer);
                if (updateCount >=0)
                {
                    return Ok(new ResponseContext
                    {
                        code = (int)Common.ResponseCode.SUCCESS,
                        message = Common.Message.SUCCESS,
                        data = customer
                    });
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new ResponseMessage { status = "ERROR", message = "Cannot update customer" });
                }
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseMessage { status = "ERROR", message = ex.Message });
            }
        }
        [HttpPost]
        [Route("api/customer/delete")]
        public ActionResult<ResponseContext> Delete([FromBody] string[] IdArray)
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
                long deleteCount = _customerServices.DeleteCustomer(IdArray);
                if (deleteCount >= 0)
                {
                    return Ok(new ResponseContext
                    {
                        code = (int)Common.ResponseCode.SUCCESS,
                        message = Common.Message.SUCCESS,
                        data = JsonConvert.SerializeObject(""+ deleteCount + " have been deleted")
                    });
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new ResponseMessage { status = "ERROR", message = "Cannot delete customer" });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseMessage { status = "ERROR", message = ex.Message });
            }
        }
        [HttpGet]
        [Route("api/customer/countstatus")]
        public ActionResult<ResponseContext> CustomerSatusCount([FromQuery]string username, [FromQuery]string greentype)
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
                var statusCount = _customerServices.GetStatusCount(username, greentype);
                return Ok(new ResponseContext
                {
                    code = (int)Common.ResponseCode.SUCCESS,
                    message = Common.Message.SUCCESS,
                    data = statusCount
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseMessage { status = "ERROR", message = ex.Message });
            }
        }
    }
}