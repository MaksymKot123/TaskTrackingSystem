using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskTrackingSystem.BLL;
using TaskTrackingSystem.BLL.DTO;
using TaskTrackingSystem.ViewModels;

namespace TaskTrackingSystem.Controllers
{
    [Produces("application/json")]
    [Route("[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUserService _userService;

        public LoginController(IUserService userService)
        {
            _userService = userService;
        }
        [Route("/account/login")]
        [HttpPost]
        public async  Task<ActionResult<string>> Login([FromQuery] UserView user)
        {
            var usr = new UserDTO()
            {
                Email = user.Email,
                Name = user.Name,
            };

            //var a = await _userService.UnifOwWork.UserManager.FindByIdAsync("");
            //a.

            var res = await _userService.Authenticate(usr, user.Password);
            if (res == null)
                return Unauthorized(new { message = "Incorrect email or password" });
            else return res.Token;
        }
    }
}
