using System;
using System.Collections.Generic;
using System.Text;
using TaskTrackingSystem.BLL.DTO;

namespace TaskTrackingSystem.BLL.Interfaces
{
    /// <summary>
    /// Interface of project service
    /// </summary>
    public interface IProjectService : IDisposable
    {
        /// <summary>
        /// Get all projects
        /// </summary>
        /// <returns>A list of <see cref="BLL.DTO.ProjectDTO"/></returns>
        IEnumerable<ProjectDTO> GetAllProjects();

        /// <summary>
        /// Get project by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns><see cref="BLL.DTO.ProjectDTO"/></returns>
        ProjectDTO GetProject(string name);

        /// <summary>
        /// Edit a project's info
        /// </summary>
        /// <param name="project"></param>
        void EditProject(ProjectDTO project);

        /// <summary>
        /// Add new project to database
        /// </summary>
        /// <param name="project"></param>
        void AddProject(ProjectDTO project);

        /// <summary>
        /// Delete project from database
        /// </summary>
        /// <param name="project"></param>
        void DeleteProject(ProjectDTO project);

        /// <summary>
        /// Get employee's all DTO project models
        /// </summary>
        /// <param name="email"></param>
        /// <returns>A list of <see cref="BLL.DTO.ProjectDTO"/></returns>
        IEnumerable<ProjectDTO> GetEmployeesProjects(string email);
    }
}
