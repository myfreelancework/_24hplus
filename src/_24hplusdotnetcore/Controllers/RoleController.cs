﻿using System.Collections.Generic;
using _24hplusdotnetcore.Models;
using _24hplusdotnetcore.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace _24hplusdotnetcore.Controllers
{
    [ApiController]
    public class RoleController : ControllerBase
    {
        private ILogger<RoleController> _logger;
        private readonly RoleServices _roleService;
        public RoleController(RoleServices roleServices, ILogger<RoleController> logger)
        {
            _roleService = roleServices;
            _logger = logger;
        }
        [HttpGet]
        [Route("api/roles")]
        public ActionResult<List<Roles>> Get()
        {
            try
            {
                var lstRole = _roleService.Get();
                return Ok(lstRole);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseMessage{status ="ERROR", message = ex.Message});
            }

        }
        [HttpGet]
        [Route("api/role/{roleName}")]
        public ActionResult<Roles> GetRoleByName(string roleName)
        {
            try
            {
                var objRole = _roleService.GetRoleByName(roleName);
                return Ok(objRole);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseMessage{status ="ERROR", message = ex.Message});
            }
        }
        [HttpPost]
        [Route("api/role")]
        public ActionResult<Roles> Create(Roles role)
        {
            try
            {
                var newRole = _roleService.Create(role);
                return Ok(newRole);
            }
            catch (System.Exception ex)
            {
                 _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseMessage{status ="ERROR", message = ex.Message});
            }
        }
    }
}