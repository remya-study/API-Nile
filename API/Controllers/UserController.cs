using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Nile.Interface;

namespace API_Nile.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {

        private readonly IUserService _userService;
        public UserController(IUserService userService) { 
        _userService = userService;
        }
        [HttpGet("All")]
        public ActionResult Users()
        {
            var users = _userService.GetUsers();
            return Ok(users);
        }
        [HttpGet("")]
        public ActionResult UserProfile(string login)
        {
            var userProfile= _userService.GetUserProfile(login);
            return Ok(userProfile);
        }


    }
}
