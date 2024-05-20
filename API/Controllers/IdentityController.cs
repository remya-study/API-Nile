using Core_Nile.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Model.Nile.Interface;
using Model.Nile.ViewModel;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using LoginRequest = Model.Nile.ViewModel.LoginRequest;

namespace API_Nile.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController(IConfiguration configuration, IUserService userService) : ControllerBase
    {
        [HttpPost("token")]
        public ActionResult CreateToken(LoginRequest loginRequest)
        {

            bool isUserValid = userService.ValidateUser(loginRequest.UserName, loginRequest.Password);
            if (!isUserValid)
            {
                return Unauthorized("Invalid user");
            }

            var issuer = configuration["Jwt:Issuer"];
            var audience = configuration["Jwt:Audience"];
            var key = configuration["Jwt:Key"];

            if(key == null) { return  Problem("Issue with processing request","",500,"Error"); };

            var keyBytes = Encoding.ASCII.GetBytes(key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim("Id", Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, loginRequest.UserName),
                new Claim(JwtRegisteredClaimNames.Email, loginRequest.UserName),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                }),
                Expires = DateTime.UtcNow.AddMinutes(5),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials
                (new SymmetricSecurityKey(keyBytes),
                SecurityAlgorithms.HmacSha512Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);            
            var stringToken = tokenHandler.WriteToken(token);
            return Ok(stringToken);


        }
    }
    
}
