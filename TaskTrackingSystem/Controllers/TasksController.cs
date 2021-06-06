using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using TaskTrackingSystem.BLL.DTO;
using TaskTrackingSystem.BLL.Exceptions;
using TaskTrackingSystem.BLL.Interfaces;
using TaskTrackingSystem.ViewModels;

namespace TaskTrackingSystem.Controllers
{
    [Route("[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        /// <summary>
        /// <see cref="BLL.Interfaces.ITaskService"/>
        /// </summary>
        private readonly ITaskService _taskService;

        /// <summary>
        /// Constructor for task controller. Via dependency injection 
        /// it will get a task service
        /// </summary>
        /// <param name="taskService"></param>
        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        /// <summary>
        /// This method adds a new task to existing project
        /// </summary>
        /// <param name="task"></param>
        /// <returns>If there are not any errors you will get status code 200. 
        /// Otherwise, you will get status code 400</returns>
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin, Manager")]
        [HttpPost]
        public IActionResult AddTaskToProject([FromBody] TaskView task)
        {
            var newTask = new TaskDto()
            {
                Description = task.Description,
                Status = BLL.Enums.StatusDTO.Started,
                StartTime = DateTime.Now,
                TaskName = task.TaskName,
                EndTime = task.EndTime,
                Project = new ProjectDto() { Name = task.ProjName }
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

        /// <summary>
        /// This methods returns a list of project's tasks
        /// </summary>
        /// <param name="project"></param>
        /// <returns>A list of <see cref="ViewModels.TaskView"/></returns>
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

        /// <summary>
        /// This method edits a task's info in projects
        /// </summary>
        /// <param name="task"></param>
        /// <returns>If there are not any errors you will get status code 200. 
        /// Otherwise, you will get status code 400</returns>
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin, Manager, Employee")]
        [HttpPut]
        public IActionResult EditTaskOfProject([FromBody] TaskView task)
        {
            var changedTask = new TaskDto()
            {
                Description = task.Description,
                Status = (BLL.Enums.StatusDTO)Enum.Parse(typeof(BLL.Enums.StatusDTO), task.Status, true),
                StartTime = DateTime.Now,
                TaskName = task.TaskName,
                EndTime = task.EndTime,
                Project = new ProjectDto() { Name = task.ProjName }
            };
            try
            {
                _taskService.Change(changedTask);
                return Ok();
            }
            catch (TaskException e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// This method deletes a task of project
        /// </summary>
        /// <param name="task"></param>
        /// <returns>If there are not any errors you will get status code 204. 
        /// Otherwise, you will get status code 400</returns>
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin, Manager")]
        [HttpDelete("project")]
        public IActionResult DeleteTaskInProject([FromBody] TaskView task)
        {
            var taskDTO = new TaskDto()
            {
                Description = task.Description,
                Status = (BLL.Enums.StatusDTO)Enum.Parse(typeof(BLL.Enums.StatusDTO), task.Status, true),
                StartTime = DateTime.Now,
                TaskName = task.TaskName,
                EndTime = task.EndTime,
                Project = new ProjectDto() { Name = task.ProjName }
            };
            try
            {
                _taskService.Delete(taskDTO);
                return StatusCode(204);
            }
            catch (TaskException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
