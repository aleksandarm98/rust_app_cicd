using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PetMeetApp.BLL.Interfaces;
using PetMeetApp.Common.Services;
using PetMeetApp.Models;
using PetMeetApp.Models.HelpModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PetMeetApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IAuthBLL _iAuthBLL;
        public AuthController(IConfiguration configuration, IAuthBLL IAuthBLL)
        {
            _configuration = configuration;
            _iAuthBLL = IAuthBLL;
        }

        [AllowAnonymous]
        [HttpPost("Auth")]
        public IActionResult Auth([FromBody] LoginModel data)
        {
            UserModel user = _iAuthBLL.Login(data);

            if (user != null)
            {
                var tokenString = JWTService.JWT.GenerateJwtToken(user, _configuration);
                return Ok(new { Token = tokenString, Message = "Success", User = user});
            }
            return BadRequest();
        }
        [AllowAnonymous]
        [HttpPost("ForgotPassword")]
        public IActionResult ForgotPassword([FromBody] ForgotPasswordModel data)
        {
            if(_iAuthBLL.ForgotPassword(data) == true)
            {
                return Ok();
            }
            return BadRequest();
        }

        [AllowAnonymous]
        [HttpPost("ResetPassword")]
        public IActionResult ResetPassword([FromBody] ForgotPasswordModel data)
        {
            if (_iAuthBLL.ResetPassword(data) == true)
            {
                return Ok();
            }
            return BadRequest();
        }
        
        [AllowAnonymous]
        [HttpPost("SendValidationCode")]
        public IActionResult SendValidationCode([FromBody] EmailValidationModel data)
        {
            if(_iAuthBLL.SendValidationCode(data) == true)
            {
                return Ok();
            }
            return BadRequest();
        }
        
        [AllowAnonymous]
        [HttpPost("ValidateEmail")]
        public IActionResult ValidateEmail([FromBody] EmailValidationModel data)
        {
            if(_iAuthBLL.ValidateEmail(data) == true)
            {
                return Ok();
            }
            return BadRequest();
        }
        
    }
}
