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
    [Route("api/[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public async  Task<ActionResult<UserDTO>> Login([FromQuery] UserView user)
        {
            var usr = new UserDTO()
            {
                Email = user.Email,
                Name = user.Name,
            };

            //var a = await _userService.UnifOwWork.UserManager.FindByIdAsync("");
            //a.

            return await _userService.Login(usr, user.Password);
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> Register([FromQuery] UserView user)
        {
            var usr = new UserDTO()
            {
                Email = user.Email,
                Name = user.Name,
            };

            return await _userService.Register(usr, user.Password);
        }

    }
}
