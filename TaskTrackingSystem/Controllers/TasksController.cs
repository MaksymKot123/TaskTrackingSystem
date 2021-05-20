using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using TaskTrackingSystem.BLL.Exceptions;
using TaskTrackingSystem.BLL.DTO;
using TaskTrackingSystem.BLL.Interfaces;
using TaskTrackingSystem.ViewModels;

namespace TaskTrackingSystem.Controllers
{
    [Route("[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin, Manager")]
        [HttpPost]
        public IActionResult AddTaskToProject([FromBody] TaskView task)
        {
            var newTask = new TaskDTO()
            {
                Description = task.Description,
                Status = BLL.Enums.StatusDTO.Started,
                StartTime = DateTime.Now,
                TaskName = task.TaskName,
                EndTime = task.EndTime,
                Project = new ProjectDTO() { Name = task.ProjName }
            };

            try
            {
                _taskService.AddToProject(task.ProjName, newTask);
                return Ok();
            }
            catch (ProjectException e)
            {
                return BadRequest(e.Message);
            }
            catch (TaskException e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin, Manager, Employee")]
        [HttpGet]
        public IEnumerable<TaskView> GetTasksOfProject([FromHeader] ProjectView project)
        {
            var result = _taskService.GetTasksOfProject(project.Name);
            return result.Select(x => new TaskView()
            {
                Description = x.Description,
                EndTime = x.EndTime,
                ProjName = x.Project.Name,
                StartTime = x.StartTime,
                Status = x.Status.ToString(),
                TaskName = x.TaskName
            });
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin, Manager, Employee")]
        [HttpPut]
        public IActionResult EditTaskOfProject([FromBody] TaskView task)
        {
            var changedTask = new TaskDTO()
            {
                Description = task.Description,
                Status = (BLL.Enums.StatusDTO)Enum.Parse(typeof(BLL.Enums.StatusDTO), task.Status, true),//BLL.Enums.StatusDTO(task.Status),
                StartTime = DateTime.Now,
                TaskName = task.TaskName,
                EndTime = task.EndTime,
                Project = new ProjectDTO() { Name = task.ProjName }
            };
            try
            {
                _taskService.Change(changedTask);
                return Ok();
            }
            catch(TaskException e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin, Manager")]
        [HttpDelete("project")]
        public IActionResult DeleteTaskInProject([FromBody] TaskView task)
        {
            var taskDTO = new TaskDTO()
            {
                Description = task.Description,
                Status = (BLL.Enums.StatusDTO)Enum.Parse(typeof(BLL.Enums.StatusDTO), task.Status, true),
                StartTime = DateTime.Now,
                TaskName = task.TaskName,
                EndTime = task.EndTime,
                Project = new ProjectDTO() { Name = task.ProjName }
            };
            try
            {
                _taskService.Delete(taskDTO);
                return Ok();
            }
            catch(TaskException e)
            {
                return BadRequest(e.Message);
            } 
        }
    }
}
