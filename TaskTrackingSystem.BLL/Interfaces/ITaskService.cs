using System;
using System.Collections.Generic;
using System.Text;
using TaskTrackingSystem.BLL.DTO;

namespace TaskTrackingSystem.BLL.Interfaces
{
    public interface ITaskService : IDisposable
    {
        void AddToProject(ProjectDTO project);
        void Change(TaskDTO task);
        void Delete(TaskDTO task);
    }
}
