using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public void AddTaskToProject([FromBody] TaskView task)
        {
            var newTask = new TaskDTO()
            {
                Description = task.Description,
                Status = task.Status,
                StartTime = DateTime.Now,
                TaskName = task.TaskName,
                EndTime = task.EndTime,
            };
            _taskService.AddToProject(task.ProjName, newTask);
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
                Status = x.Status,
                TaskName = x.TaskName
            });
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin, Manager, Employee")]
        [HttpPut]
        public void EditTaskOfProject([FromBody] TaskView task)
        {
            var changedTask = new TaskDTO()
            {
                Description = task.Description,
                Status = task.Status,
                StartTime = DateTime.Now,
                TaskName = task.TaskName,
                EndTime = task.EndTime,
                
            };
            _taskService.Change(changedTask);
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin, Manager")]
        [HttpDelete("project")]
        public void DeleteTaskInProject([FromBody] TaskView task)
        {
            var taskDTO = new TaskDTO()
            {
                Description = task.Description,
                Status = task.Status,
                StartTime = DateTime.Now,
                TaskName = task.TaskName,
                EndTime = task.EndTime,
            };
            _taskService.Delete(taskDTO);
        }


    }
}
