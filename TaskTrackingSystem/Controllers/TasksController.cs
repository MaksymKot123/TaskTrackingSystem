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

        [HttpPost("addtoproject/{name}/")]
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
    }
}
