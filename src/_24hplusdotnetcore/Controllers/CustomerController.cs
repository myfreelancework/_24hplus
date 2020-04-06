﻿using System;
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
                return Ok(new ResponseContext
                {
                    code = (int)Common.ResponseCode.SUCCESS,
                    message = Common.Message.SUCCESS,
                    data = lstCustomers
                });
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
        [Route("api/customer/getcustomerbyusername/{username}")]
        public ActionResult<List<Customer>> GetCustomerByUserName(string username)
        {
            try
            {
                var lstCustomers = new List<Customer>();
                lstCustomers = _customerServices.GetCustomerByUserName(username);
                return Ok(new ResponseContext
                {
                    code = (int)Common.ResponseCode.SUCCESS,
                    message = Common.Message.SUCCESS,
                    data = lstCustomers
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
        public ActionResult<Customer> Create(Customer customer)
        {
            try
            {
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
        public ActionResult<ResponseMessage> Update(Customer customer)
        {
            try
            {
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
        public ActionResult<ResponseMessage> Delete(string MaKH)
        {
            try
            {
                long deleteCount = _customerServices.DeleteCustomer(MaKH);
                if (deleteCount >= 0)
                {
                    return Ok(new ResponseContext
                    {
                        code = (int)Common.ResponseCode.SUCCESS,
                        message = Common.Message.SUCCESS,
                        data = new ResponseMessage
                        {
                            status = "Delete Successful",
                            message = "MaKH: "+MaKH+" has been deleted"
                        }
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
    }
}