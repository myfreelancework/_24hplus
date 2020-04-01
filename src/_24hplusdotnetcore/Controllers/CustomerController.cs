using System;
using System.Collections.Generic;
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
        public ActionResult<List<Customer>> GetCustomerList()
        {
            try
            {
                var lstCustomers = new List<Customer>();
                lstCustomers = _customerServices.GetList();
                return Ok(lstCustomers);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseMessage {status = "ERROR", message = ex.Message});
            }
        }
        [HttpGet]
        [Route("api/customer/{MaKH}")]
        public ActionResult<Customer> GetCustomer(string MaKH)
        {
            try
            {
                var objCustomer = new Customer();
                objCustomer = _customerServices.GetCustomer(MaKH);
                return Ok(objCustomer);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseMessage { status = "ERROR", message = ex.Message });
            }
        }
        [HttpPost]
        [Route("api/customer")]
        public ActionResult<Customer> Create(Customer customer)
        {
            try
            {
                var newCustomer = new Customer();
                newCustomer =  _customerServices.CreateCustomer(customer);
                return Ok(newCustomer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseMessage { status = "ERROR", message = ex.Message });
            }
        }
        [HttpPost]
        [Route("api/customer/update")]
        public ActionResult<ResponseMessage> Update(Customer customer)
        {
            long updateCount = 0;
            try
            {
                updateCount = updateCount = _customerServices.UpdateCustomer(customer);
                return Ok(new ResponseMessage {status = StatusCodes.Status200OK.ToString(), message = "Modified Customer count: "+updateCount+"" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseMessage { status = "ERROR", message = ex.Message });
            }
        }
        [HttpPost]
        [Route("api/customer/delete")]
        public ActionResult<ResponseMessage> Delete(string MaKH)
        {
            try
            {
                long deleteCount = deleteCount = _customerServices.DeleteCustomer(MaKH);
                return Ok(new ResponseMessage { status = StatusCodes.Status200OK.ToString(), message = "Deleted Customer count: " + deleteCount + "" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseMessage { status = "ERROR", message = ex.Message });
            }
        }
    }
}