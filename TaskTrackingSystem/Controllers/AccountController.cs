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
using System.Net;

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

        /// <summary>
        /// This method changes users role
        /// </summary>
        /// <param name="roleName"></param>
        /// <param name="user"></param>
        /// <returns>Status code 200 if role was succesfully edited.
        /// Otherwise you will get status code 400 and will see error message
        /// </returns>
        [HttpPut]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public async Task<IActionResult> ChangeUsersRole(string roleName, [FromBody] UserView user)
        {
            if (user == null || roleName == null)
                return BadRequest(); 

            try
            {
                await _userService.ChangeUsersRole(roleName, user.Email);
                return Ok();
            }
            catch (UserException e)
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
        public async Task<IEnumerable<UserView>> GetUsersByRole([FromQuery] string roleName)
        {
            var users = await _userService.GetUsersByRole(roleName);
            return users.Select(x => new UserView()
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
            if (user == null)
                return BadRequest();

            var usr = new UserDTO()
            {
                Email = user.Email,
            };

            try
            {
                var res = await _userService.Authenticate(usr, user.Password);
                return Ok(res.Token);
            }
            catch (UserException e)
            {
                return Unauthorized(e.Message);
            }

        }

        /// <summary>
        /// This method uses user service for registering new users
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Status code 201 if registration was succesful.
        /// Otherwise you will get status code 400 and will see error message 
        /// on registration page</returns>
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterView user)
        {
            if (user == null)
                return BadRequest();

            var usr = new UserDTO()
            {
                Email = user.Email,
                Name = user.Name,
            };

            try
            {
                var newUser = await _userService.Register(usr, user.Password);
                return Created("", newUser);
            }
            catch (UserException e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// This method adds some user to project with unique name
        /// </summary>
        /// <param name="projName"></param>
        /// <param name="user"></param>
        /// <returns>Status code 200 if user was succesfully added to project.
        /// Otherwise you will get status code 400 and will see error message
        /// </returns>
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin, Manager")]
        [HttpPost("addtoproject")]
        public IActionResult AddToProject(string projName, [FromBody] UserView user)
        {
            if (projName == null || user == null)
                return BadRequest();

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

        /// <summary>
        /// This method deletes user from database
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Status code 204 if deleting was succesful.
        /// Otherwise you will get NotFound error if user is not found or status
        /// code 400 for other errors and will see error message</returns>
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        [HttpDelete]
        public async Task<IActionResult> DeleteUser([FromBody] UserView user)
        {
            if (user == null)
                return BadRequest();

            var usr = new UserDTO()
            {
                Email = user.Email,
                Name = user.Name,
            };
            try
            {
                await _userService.DeleteUser(usr);
                return StatusCode(204);
            }
            catch(UserException e)
            {
                if (e.Message.Equals("User not found"))
                    return NotFound(e.Message);
                return BadRequest(e.Message);
            }
        }
    }
}
