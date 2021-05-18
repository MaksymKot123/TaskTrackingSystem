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


namespace TaskTrackingSystem.Controllers
{
    [Produces("application/json")]
    [Route("[controller]")]
    [ApiController]
    public class ProjectController : Controller
    {
        private readonly IProjectService _projService;

        public ProjectController(IProjectService projService)
        {
            _projService = projService;
        }

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

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin, Manager")]
        [HttpPost]
        public void AddProject([FromBody] ProjectView proj)
        {
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

            _projService.AddProject(newProj);
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin, Manager")]
        public void Delete([FromBody] ProjectView proj)
        {
            var project = new ProjectDTO()
            {
                Name =proj.Name,
                ClientEmail = proj.ClientEmail,
                EndTime = proj.EndTime,
            };

            _projService.DeleteProject(project);
        }
        [HttpPut]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin, Manager")]
        public void EditProject([FromBody] ProjectView proj)
        {
            var project = new ProjectDTO()
            {
                ClientEmail = proj.ClientEmail,
                Description = proj.Description,
                EndTime = proj.EndTime,
                Name = proj.Name,
                Status = BLL.Enums.StatusDTO.Started,
                StartTime = DateTime.Now,
            };

            _projService.EditProject(project);
        }
    }
}
