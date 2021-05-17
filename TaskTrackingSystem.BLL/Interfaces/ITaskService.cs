using System;
using System.Collections.Generic;
using System.Text;
using TaskTrackingSystem.BLL.DTO;

namespace TaskTrackingSystem.BLL.Interfaces
{
    public interface ITaskService : IDisposable
    {
        void AddToProject(string projectName, TaskDTO task);
        void Change(TaskDTO task);
        void Delete(TaskDTO task);
        IEnumerable<TaskDTO> GetTasksOfProject(string projectName);
    }
}
