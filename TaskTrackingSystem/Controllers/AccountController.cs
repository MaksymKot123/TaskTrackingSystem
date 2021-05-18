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
        public async  Task<ActionResult<string>> Login([FromBody] LoginView user)
        {
            var usr = new UserDTO()
            {
                Email = user.Email,
            };

            var res = await _userService.Authenticate(usr, user.Password);
            if (res == null)
                return Unauthorized(new { message = "Incorrect email or password" });
            else return Ok(res.Token);
        }

        //[HttpGet]
        //public IEnumerable<ProjectView> GetEmployeesProjects(string employeeEmail)
        //{
            
        //}

        [HttpPost]
        public async Task<ActionResult<UserDTO>> Register([FromBody] RegisterView user)
        {
            var usr = new UserDTO()
            {
                Email = user.Email,
                Name = user.Name,
            };

            var responce = await _userService.Register(usr, user.Password);
            if (responce == null)
                return BadRequest();
            else return responce;
        }

        [HttpPost("addtoproject")]
        public IActionResult AddToProject(string projName, [FromBody] UserDTO user)
        {
            _userService.AddToProject(projName, user);
            return Ok();
        }
    }
}
