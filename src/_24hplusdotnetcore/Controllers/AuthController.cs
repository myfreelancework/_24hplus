using System.Collections.Generic;
using _24hplusdotnetcore.Models;
using _24hplusdotnetcore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _24hplusdotnetcore.Controllers
{
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthServices _authServices;
        public AuthController(AuthServices authServices)
        {
            _authServices = authServices;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/auth/login")]
        public ActionResult Login(User user)
        {
            try
            {
                string tokenString = string.Empty;
                tokenString = _authServices.Login(user);
                if (!string.IsNullOrWhiteSpace(tokenString))
                {
                    return Ok(new { token = tokenString });
                }
                else
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, new {message = "username or password incorrect"});
                }
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseMessage { status = "ERROR", message = ex.Message });
            }

        }
    }
}