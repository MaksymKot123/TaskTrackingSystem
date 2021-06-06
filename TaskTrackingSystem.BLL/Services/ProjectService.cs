using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using TaskTrackingSystem.BLL.DTO;
using TaskTrackingSystem.BLL.Exceptions;
using TaskTrackingSystem.BLL.Interfaces;
using TaskTrackingSystem.DAL.Interfaces;
using TaskTrackingSystem.DAL.Models;

namespace TaskTrackingSystem.BLL.Services
{
    /// <summary>
    /// A project service
    /// </summary>
    public class ProjectService : IProjectService
    {
        private bool disposedValue;
        /// <summary>
        /// <see cref="DAL.Interfaces.IUnitOfWork"/>
        /// </summary>
        private readonly IUnitOfWork _unifOfWork;
        private readonly IMapper _mapper;

        public ProjectService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unifOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// This method returns all projects of employee, which you can get by email
        /// </summary>
        /// <param name="email"></param>
        /// <returns>A list of <see cref="BLL.DTO.ProjectDto"/></returns>
        public IEnumerable<ProjectDto> GetEmployeesProjects(string email)
        {
            var user = _unifOfWork.GetUserWithDetails(email);
            var projects = _mapper.Map<IEnumerable<ProjectDto>>(
                user.Projects.AsEnumerable());

            return projects;
        }

        /// <summary>
        /// This method deletes a project from database by name. If there is
        /// not any project with this name, a project exception will be thrown
        /// </summary>
        /// <param name="project"></param>
        public void DeleteProject(ProjectDto project)
        {
            var proj = _unifOfWork.ProjectRepo.Get(project.Name);

            if (proj != null)
            {
                var projct = _mapper.Map<Project>(proj);
                _unifOfWork.ProjectRepo.Delete(projct);
                _unifOfWork.SaveChanges();
            }
            else
            {
                throw new ProjectException("Project not found");
            }
        }

        /// <summary>
        /// This method edits project's info
        /// </summary>
        /// <param name="project"></param>
        public void EditProject(ProjectDto project)
        {
            var name = project.Name;

            var proj = _unifOfWork.ProjectRepo.GetAll()
                .FirstOrDefault(x => x.Name.Equals(name));

            if (proj != null)
            {
                proj.EndTime = project.EndTime;
                proj.Description = project.Description;
                proj.Name = project.Name;
                proj.Status = _mapper.Map<DAL.Enums.Status>(project.Status);
                proj.ClientEmail = project.ClientEmail;
                proj.StartTime = project.StartTime;

                _unifOfWork.ProjectRepo.Edit(proj);
                _unifOfWork.SaveChanges();
            }
            else
            {
                throw new ProjectException("Project not found");
            }
        }

        /// <summary>
        /// This method returns all projects from database
        /// </summary>
        /// <returns>A list of <see cref="BLL.DTO.ProjectDto"/></returns>
        public IEnumerable<ProjectDto> GetAllProjects()
        {
            return _mapper.Map<IEnumerable<Project>, IEnumerable<ProjectDto>>(
                _unifOfWork.ProjectRepo.GetAll());
        }

        /// <summary>
        /// This method add new project. If database has a project, which has
        /// a same name with a new project, a project exception will be thrown
        /// </summary>
        /// <param name="project"></param>
        public void AddProject(ProjectDto project)
        {
            var proj = _unifOfWork.ProjectRepo.Get(project.Name);
            if (proj == null)
            {
                _unifOfWork.ProjectRepo.Create(_mapper.Map<Project>(project));
                _unifOfWork.SaveChanges();
                var newProj = _unifOfWork.ProjectRepo.Get(project.Name);
                EmailSender.EmailSender.SendEmail(newProj.ClientEmail, newProj.Status);
            }
            else
            {
                throw new ProjectException("Please use other name for project");
            }
        }

        /// <summary>
        /// This method return DTO model of project by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns><see cref="BLL.DTO.ProjectDto"/></returns>
        public ProjectDto GetProject(string name)
        {
            var proj = _unifOfWork.ProjectRepo.GetAll()
                .FirstOrDefault(x => x.Name.Equals(name));

            if (proj != null)
            {
                return new ProjectDto()
                {
                    ClientEmail = proj.ClientEmail,
                    Description = proj.Description,
                    Employees = _mapper.Map<ICollection<UserDto>>(proj.Employees),
                    EndTime = proj.EndTime,
                    Id = proj.Id,
                    Name = proj.Name,
                    PercentCompletion = proj.PercentCompletion,
                    StartTime = proj.StartTime,
                    Status = _mapper.Map<BLL.Enums.StatusDTO>(proj.Status),
                    Tasks = _mapper.Map<ICollection<TaskDto>>(proj.Tasks)
                };
            }
            else
            {
                throw new ProjectException("Project not found");
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _unifOfWork.Dispose();
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
