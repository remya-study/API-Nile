using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Nile;
using Model.Nile.Interface;
using Model.Nile.ViewModel;
using System.Security.Cryptography;
using System.Text;

namespace API_Nile.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LoginController : ControllerBase
    {

        private readonly IUserService _userService;
        public LoginController(IUserService userService) { 
        _userService = userService;
        }
        [HttpPost("")]
        public ActionResult Login(LoginRequest loginRequest)
        {
            var isValidUser = _userService.ValidateUser(loginRequest.UserName,loginRequest.Password);
            return Ok(new { iva = isValidUser });
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public ActionResult Register(RegisterUser registerUser)
        {
            //TODO : Add rate limiting check 
            if (registerUser == null)
            {
                return BadRequest(ModelState);
            }

            var userIdExists = _userService.CheckIfUserIdExists(registerUser.UserName);
            if (userIdExists) 
            {
                return Ok(new RegisterUserResponse { status="Error", message = $" UserId {registerUser.UserName} already in use" });
            }
            var result= _userService.CreateUser(registerUser);

            return Ok(new RegisterUserResponse { status = "success", message = "",data= result });

        }
    }

}
