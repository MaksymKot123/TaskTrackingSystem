using System;
using System.Collections.Generic;
using System.Text;
using TaskTrackingSystem.BLL.DTO;

namespace TaskTrackingSystem.BLL.Interfaces
{
    public interface IProjectService : IDisposable
    {
        IEnumerable<ProjectDTO> GetAllProjects();
        IEnumerable<ProjectDTO> Find(Func<ProjectDTO, bool> func);
        ProjectDTO GetProject(string name);
        void EditProject(ProjectDTO project);
    }
}
