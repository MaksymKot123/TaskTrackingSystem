using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskTrackingSystem.BLL;
using TaskTrackingSystem.BLL.DTO;
using TaskTrackingSystem.BLL.Exceptions;
using TaskTrackingSystem.ViewModels;

namespace TaskTrackingSystem.Controllers
{
    [Produces("application/json")]
    [Route("[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin, Manager")]
        [HttpGet]
        public IEnumerable<UserDTO> GetUsersByRole([FromQuery]string roleName)
        {
            return _userService.GetUsersByRole(roleName).GetAwaiter().GetResult();
        }

        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginView user)
        {
            var usr = new UserDTO()
            {
                Email = user.Email,
            };

            try
            {
                var res = await _userService.Authenticate(usr, user.Password);
                return Ok(res.Token);
            }
            catch(UserException e)
            {
                return BadRequest(e.Message);
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterView user)
        {
            var usr = new UserDTO()
            {
                Email = user.Email,
                Name = user.Name,
            };

            try
            {
                var responce = await _userService.Register(usr, user.Password);
                return Ok();
            }
            catch(UserException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("addtoproject")]
        public IActionResult AddToProject(string projName, [FromBody] UserDTO user)
        {
            try
            {
                _userService.AddToProject(projName, user);
                return Ok();
            }
            catch(UserException e) 
            { 
                return BadRequest(e.Message); 
            }
            catch (ProjectException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
