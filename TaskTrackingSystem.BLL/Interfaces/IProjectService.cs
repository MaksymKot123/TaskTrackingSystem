using System;
using System.Collections.Generic;
using System.Text;
using TaskTrackingSystem.BLL.DTO;

namespace TaskTrackingSystem.BLL.Interfaces
{
    public interface IProjectService : IDisposable
    {
        IEnumerable<ProjectDTO> GetAllProjects();
        ProjectDTO GetProject(string name);
        void EditProject(ProjectDTO project);
        void AddProject(ProjectDTO project);
        void DeleteProject(ProjectDTO project);
        IEnumerable<ProjectDTO> GetEmployeesProjects(string email);
    }
}
