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
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin, Manager")]
        [HttpPost("addtoproject/{name}")]
        public void AddTaskToProject(string name, [FromQuery] TaskView task)
        {
            var newTask = new TaskDTO()
            {
                Description = task.Description,
                Status = task.Status,
                StartTime = task.StartTime,
                TaskName = task.TaskName,
                EndTime = task.EndTime,
            };
            _taskService.AddToProject(name, newTask);
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPut("project/{projName}/edittask")]
        public void EditTaskOfProject(string projName, [FromQuery] TaskView task)
        {
            var changedTask = new TaskDTO()
            {
                Description = task.Description,
                Status = task.Status,
                StartTime = task.StartTime,
                TaskName = task.TaskName,
                EndTime = task.EndTime,
            };
            _taskService.Change(changedTask);
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin, Manager")]
        [HttpDelete("project/{projName}")]
        public void DeleteTaskInProject(string projName, [FromQuery] TaskView task)
        {
            var taskDTO = new TaskDTO()
            {
                Description = task.Description,
                Status = task.Status,
                StartTime = task.StartTime,
                TaskName = task.TaskName,
                EndTime = task.EndTime,
            };
            _taskService.Delete(taskDTO);
        }


    }
}
