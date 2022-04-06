using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentCounselling.Context;
using StudentCounselling.Data;
using StudentCounselling.Entities;
using System.Linq;

namespace StudentCounselling.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class LoginAuthorizationController : Controller
    {
        private readonly StudentCouncellingDbContext _dataBase;
        private readonly IJwtAuthenticationManager jwtAuthenticationManager;

        public LoginAuthorizationController(
            StudentCouncellingDbContext dataBase,
            IJwtAuthenticationManager _JwtAuthenticationManager)
        {
            this._dataBase = dataBase;
            this.jwtAuthenticationManager = _JwtAuthenticationManager;
        }
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] UserLogin user)
        {
            var result = this._dataBase.UserLogin.Any(u => u.UserName == user.UserName && u.Password == user.Password);
            if (!result)
            {
                return null;
            }
            var token = this.jwtAuthenticationManager.Authenticate(user.UserName, user.Password);
            if (token != null)
            {
                return Ok(token);
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}
