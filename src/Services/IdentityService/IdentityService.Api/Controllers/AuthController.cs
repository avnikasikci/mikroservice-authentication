using Core.Security.Dtos;
using Core.Security.JWT;
using IdentityService.Application.Features.Auth.Command;
using IdentityService.Application.Features.Auth.Dto;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromQuery] UserForLoginDto userForLogin)
        {
            LoginCommand loginCommand = new() { userForLoginDto = userForLogin };
            LoginDto result = await Mediator.Send(loginCommand);
            //string resultJsonStr = JsonConvert.SerializeObject(result);

            return Ok(result);
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromQuery] UserForRegisterDto userForRegister)
        {
            RegisterCommand registerCommand = new() { userForRegisterDto = userForRegister };
            AccessToken result = await Mediator.Send(registerCommand);
            return Ok(result);

        }
    }
}
