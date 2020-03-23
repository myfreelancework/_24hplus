using _24hplusdotnetcore.Models;
using _24hplusdotnetcore.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
                    return StatusCode(StatusCodes.Status500InternalServerError ,new ResponseMessage{status = "ERROR", message = ex.Message});
            }
        }
    }
}