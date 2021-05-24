using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskTrackingSystem.BLL;
using TaskTrackingSystem.BLL.Interfaces;
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
        /// <summary>
        /// <see cref="BLL.Interfaces.IUserService"/>
        /// </summary>
        private readonly IUserService _userService;

        /// <summary>
        /// Constructor for account controller. Via dependency injection 
        /// it will get a user service
        /// </summary>
        /// <param name="userService"></param>
        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public IActionResult ChangeUsersRole(string roleName, [FromBody] UserView user)
        {
            try
            {
                _userService.ChangeUsersRole(roleName, user.Email);
                return Ok();
            }
            catch(UserException e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// This method returns a list of users with a specific role
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns>A list of <see cref="ViewModels.UserView"/> with a specific role</returns>

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin, Manager")]
        [HttpGet]
        public IEnumerable<UserView> GetUsersByRole([FromQuery]string roleName)
        {
            return _userService.GetUsersByRole(roleName).GetAwaiter().GetResult()
                .Select(x => new UserView()
                {
                    Email = x.Email,
                    Name = x.Name
                });
        }

        /// <summary>
        /// This method uses <see cref="BLL.Services.UserService"/> for log in user's account
        /// </summary>
        /// <param name="user"></param>
        /// <returns>If email and password are correct, this method returns
        /// status code 200 and JWT token. Otherwise, you will error message
        /// on login page</returns>
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
        /// <summary>
        /// This method uses user service for registering new users
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Status code 200 if registration was succesful.
        /// Otherwise you will get status code 400 and will see error message 
        /// on registration page</returns>
        [HttpPost]
        public IActionResult Register([FromBody] RegisterView user)
        {
            var usr = new UserDTO()
            {
                Email = user.Email,
                Name = user.Name,
            };

            try
            {
                var responce = _userService.Register(usr, user.Password).GetAwaiter().GetResult();
                return Ok();
            }
            catch(UserException e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// This method adds some user to project with unique name
        /// </summary>
        /// <param name="projName"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin, Manager")]
        [HttpPost("addtoproject")]
        public IActionResult AddToProject(string projName, [FromBody] UserView user)
        {
            try
            {
                var usr = new UserDTO() { Email = user.Email, Name = user.Name };
                _userService.AddToProject(projName, usr);
                return Ok();
            }
            catch (ProjectException e1)
            {
                return BadRequest(e1.Message);
            }
            catch (UserException e2) 
            { 
                return BadRequest(e2.Message); 
            }
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        [HttpDelete]
        public IActionResult DeleteUser([FromBody] UserView user)
        {
            var usr = new UserDTO()
            {
                Email = user.Email,
                Name = user.Name,
            };
            try
            {
                _userService.DeleteUser(usr);
                return Ok();
            }
            catch(UserException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
