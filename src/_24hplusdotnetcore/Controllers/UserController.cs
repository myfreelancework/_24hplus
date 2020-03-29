using System.Collections.Generic;
using _24hplusdotnetcore.Models;
using _24hplusdotnetcore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace _24hplusdotnetcore.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserServices _userService;
        public UserController(UserServices userServices)
        {
            _userService = userServices;
        }
        
        [HttpPost]
        [Route("api/user")]        
        public ActionResult<User> Create(User user)
        {
            try
            {
                var newUser = _userService.Create(user);
                return Ok(newUser);
            }
            catch (System.Exception ex)
            {
                Log.Error(ex, ex.Message);
                    return StatusCode(StatusCodes.Status500InternalServerError ,new ResponseMessage{status = "ERROR", message = ex.Message});
            }
        }

        [HttpGet]
        [Authorize(Roles = "1")]
        [Route("api/user")]
        public ActionResult<List<User>> Get()
        {
            try
            {
                var lstUser = _userService.Get();
                return Ok(lstUser);
            }
            catch (System.Exception ex)
            {
                
                 return StatusCode(StatusCodes.Status500InternalServerError ,new ResponseMessage{status = "ERROR", message = ex.Message});
            }
        }

        [HttpGet]
        [Route("api/user/{userName}")]
        public ActionResult<User> Get(string userName)
        {
            try
            {
                var objUser = _userService.Get(userName);
                return Ok(objUser);
            }
            catch (System.Exception ex)
            {
                 return StatusCode(StatusCodes.Status500InternalServerError ,new ResponseMessage{status = "ERROR", message = ex.Message});
            }
            
        }
    }
}