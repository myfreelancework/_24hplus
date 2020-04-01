using _24hplusdotnetcore.Models;
using _24hplusdotnetcore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace _24hplusdotnetcore.Controllers
{
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly AuthServices _authServices;
        public AuthController(AuthServices authServices, ILogger<AuthController> logger)
        {
            _logger = logger;
            _authServices = authServices;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/auth/login")]
        public ActionResult Login(User user)
        {
            try
            {
                var authInfo = new AuthInfo();
                authInfo = _authServices.Login(user);
                if (authInfo != null)
                {
                    return Ok(authInfo);
                }
                else
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, new {message = "username or password is incorrect"});
                }
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseMessage { status = "ERROR", message = ex.Message });
            }

        }
    }
}