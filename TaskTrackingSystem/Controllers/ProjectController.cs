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
    [Route("api/[controller]")]
    //[AllowAnonymous]
    [ApiController]
    public class ProjectController : Controller
    {
        private readonly IProjectService _projService;
        private readonly IMapper _mapper;
        public ProjectController(IProjectService projService, IMapper mapper)
        {
            _projService = projService;
            _mapper = mapper;
        }

        [HttpGet]
        //public ActionResult<IEnumerable<ProjectDTO>> GetProjects()
        public IEnumerable<ProjectDTO> GetProjects()
        {
            var projects = _projService.GetAllProjects();
            return projects;
        }

        [HttpPost]
        public void AddProject([FromQuery] ProjectView proj)
        {
            var newProj = new ProjectDTO()
            {
                ClientEmail = proj.ClientEmail,
                Description = proj.Description,
                EndTime = proj.EndTime,
                Name = proj.Name,
                PercentCompletion = 0,
                Status = proj.StartTime > DateTime.Now ? BLL.Enums.StatusDTO.Pending : BLL.Enums.StatusDTO.Started,
                StartTime = proj.StartTime,
            };

            _projService.AddProject(newProj);
        }
    }
}
