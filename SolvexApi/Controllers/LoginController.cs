using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DataAccess.Entities;
using BusinessLayer.Interfaces.UserService;

namespace SolvexApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IUserService _userService;
        public LoginController( IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login([FromBody] User login)
        {
            User user = AuthenticateUser(login);
            if (user is null) return Unauthorized();

            var tokenString = _userService.GenerateJWTToken(user);
            return Ok(new { token = tokenString, userDetails = user, });
        }

        User AuthenticateUser(User loginCredentials)
        {
            return _userService.GetAuthenticatedUser(loginCredentials);
        }
    }
}
