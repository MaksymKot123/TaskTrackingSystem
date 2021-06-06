using System;
using System.Collections.Generic;
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
        void AddToProject(string projectName, TaskDto task);

        /// <summary>
        /// Change task's info
        /// </summary>
        /// <param name="task"></param>
        void Change(TaskDto task);

        /// <summary>
        /// Delete task
        /// </summary>
        /// <param name="task"></param>
        void Delete(TaskDto task);

        /// <summary>
        /// Get project's all DTO task models
        /// </summary>
        /// <param name="projectName"></param>
        /// <returns>A list of <see cref="BLL.DTO.TaskDto"/></returns>
        IEnumerable<TaskDto> GetTasksOfProject(string projectName);
    }
}
