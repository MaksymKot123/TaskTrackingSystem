using System;
using System.Collections.Generic;
using System.Text;
using TaskTrackingSystem.BLL.DTO;

namespace TaskTrackingSystem.BLL.Interfaces
{
    /// <summary>
    /// Interface of task service
    /// </summary>
    public interface ITaskService : IDisposable
    {
        /// <summary>
        /// Add new task to project
        /// </summary>
        /// <param name="projectName"></param>
        /// <param name="task"></param>
        void AddToProject(string projectName, TaskDTO task);

        /// <summary>
        /// Change task's info
        /// </summary>
        /// <param name="task"></param>
        void Change(TaskDTO task);

        /// <summary>
        /// Delete task
        /// </summary>
        /// <param name="task"></param>
        void Delete(TaskDTO task);

        /// <summary>
        /// Get project's all DTO task models
        /// </summary>
        /// <param name="projectName"></param>
        /// <returns></returns>
        IEnumerable<TaskDTO> GetTasksOfProject(string projectName);
    }
}
