using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NuGet.Protocol;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApplication1.Models;
using WebApplication1.repository;
using WebApplication1.Services;


namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class AuthController : ControllerBase
    {
        
        private readonly IAccountService _accountService;

        public AuthController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("SignIn")]
        public async Task<IActionResult> Login([FromBody] SignInModel login)
        {
            try
            {
                var result = await _accountService.Signin(login);
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
            
        }

        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp([FromBody] SignUpModel model)
        {
            try
            {
                var result = await _accountService.SignUp(model);
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
            
            
        }

        
        /*[HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            // Đăng xuất và xóa cookie
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // Trả về thông báo thành công
            return Ok("Logged out successfully");
        }*/

    }

    
}
