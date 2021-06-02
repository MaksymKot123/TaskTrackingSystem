using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TaskTrackingSystem.BLL.Interfaces;
using TaskTrackingSystem.BLL.DTO;
using Microsoft.AspNetCore.Authorization;
using TaskTrackingSystem.ViewModels;
using TaskTrackingSystem.BLL.Exceptions;

namespace TaskTrackingSystem.Controllers
{
    [Produces("application/json")]
    [Route("[controller]")]
    [ApiController]
    public class ProjectController : Controller
    {
        private readonly IProjectService _projService;

        /// <summary>
        /// Constructor for project controller. Via dependency injection 
        /// it will get a project service
        /// </summary>
        /// <param name="projService"></param>
        public ProjectController(IProjectService projService)
        {
            _projService = projService;
        }

        /// <summary>
        /// This method returns all projects from database
        /// </summary>
        /// <returns>A list of project</returns>
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin, Manager")]
        [HttpGet("all")]
        public IEnumerable<ProjectView> Get()
        {
            var projects = _projService.GetAllProjects();
            return projects.Select(x => new ProjectView()
            {
                ClientEmail = x.ClientEmail,
                Description = x.Description,
                EndTime = x.EndTime,
                Name = x.Name,
                StartTime = x.StartTime,
                PercentCompletion = x.PercentCompletion,
                Status = x.Status.ToString()
            });
        }

        /// <summary>
        /// This method returns a employee's list of project.
        /// The employee can be got by email
        /// </summary>
        /// <param name="email"></param>
        /// <returns>A list of <see cref="BLL.DTO.ProjectDTO"/></returns>
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin, Manager, Employee")]
        [HttpGet]
        public IEnumerable<ProjectView> GetEmployeesProjects(string email)
        {
            return _projService.GetEmployeesProjects(email)
                .Select(x => new ProjectView()
                {
                    ClientEmail = x.ClientEmail,
                    Description = x.Description,
                    EndTime = x.EndTime,
                    PercentCompletion = x.PercentCompletion,
                    StartTime = x.StartTime,
                    Status = x.Status.ToString(),
                    Name = x.Name
                });
        }

        /// <summary>
        /// This method adds new project to database
        /// </summary>
        /// <param name="proj"></param>
        /// <returns>If there are not any errors you will get status code 200. 
        /// Otherwise, you will get status code 400</returns>
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin, Manager")]
        [HttpPost]
        public IActionResult AddProject([FromBody] ProjectView proj)
        {
            if (proj == null)
                return BadRequest(); 

            var newProj = new ProjectDTO()
            {
                ClientEmail = proj.ClientEmail,
                Description = proj.Description,
                EndTime = proj.EndTime,
                Name = proj.Name,
                PercentCompletion = 0,
                Status = BLL.Enums.StatusDTO.Started,
                StartTime = DateTime.Now,
            };
            try
            {
                _projService.AddProject(newProj);
                return Ok();
            }
            catch(ProjectException e)
            {
                if (e.Message.Equals("Project not found"))
                    return NotFound(e.Message);
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// This methods deletes a project from database
        /// </summary>
        /// <param name="proj"></param>
        /// <returns>If there are not any errors you will get status code 200. 
        /// Otherwise, you will get status code 400</returns>
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin, Manager")]
        [HttpDelete]
        public IActionResult DeleteProject([FromBody] ProjectView proj)
        {
            if (proj == null)
                return BadRequest();

            var project = new ProjectDTO()
            {
                Name =proj.Name,
                ClientEmail = proj.ClientEmail,
                EndTime = proj.EndTime,
            };

            try
            {
                _projService.DeleteProject(project);
                return StatusCode(204);
            }
            catch (ProjectException e)
            {
                if (e.Message.Equals("Project not found"))
                    return NotFound(e.Message);
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// This methods changes project's info
        /// </summary>
        /// <param name="proj"></param>
        /// <returns>If there are not any errors you will get status code 200. 
        /// Otherwise, you will get status code 400</returns>
        [HttpPut]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin, Manager")]
        public IActionResult EditProject([FromBody] ProjectView proj)
        {
            if (proj == null)
                return BadRequest(); 

            var project = new ProjectDTO()
            {
                ClientEmail = proj.ClientEmail,
                Description = proj.Description,
                EndTime = proj.EndTime,
                Name = proj.Name,
                Status = BLL.Enums.StatusDTO.Started,
                StartTime = DateTime.Now,
            };
            try
            {
                _projService.EditProject(project);
                return Ok();
            }
            catch(ProjectException e)
            {
                if (e.Message.Equals("Project not found"))
                    return NotFound(e.Message);
                return BadRequest(e.Message);
            } 
        }
    }
}
