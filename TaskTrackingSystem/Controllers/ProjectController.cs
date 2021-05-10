using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TaskTrackingSystem.BLL.Interfaces;
using TaskTrackingSystem.BLL.DTO;
using Microsoft.AspNetCore.Authorization;

namespace TaskTrackingSystem.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    //[AllowAnonymous]
    [ApiController]
    public class ProjectController : Controller
    {
        private readonly IProjectService _projService;
        public ProjectController(IProjectService projService)
        {
            _projService = projService;
        }

        [HttpGet]
        //public ActionResult<IEnumerable<ProjectDTO>> GetProjects()
        public IEnumerable<ProjectDTO> GetProjects()
        {
            var projects = _projService.GetAllProjects();
            return projects;
        }
    }
}
