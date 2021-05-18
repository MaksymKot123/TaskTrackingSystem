﻿using System;
using System.Collections.Generic;
using System.Text;
using TaskTrackingSystem.DAL.Interfaces;
using TaskTrackingSystem.DAL.Models;
using TaskTrackingSystem.BLL.DTO;
using TaskTrackingSystem.BLL;
using TaskTrackingSystem.BLL.Interfaces;
using AutoMapper;
using System.Linq;

namespace TaskTrackingSystem.BLL.Services
{
    public class ProjectService : IProjectService
    {
        private bool disposedValue;
        private readonly IUnitOfWork _unifOfWork;
        private readonly IMapper _mapper;

        public ProjectService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unifOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IEnumerable<ProjectDTO> GetEmployeesProjects(string email)
        {
            var user = _unifOfWork.GetUserWithDetails(email);
            var projects = _mapper.Map<IEnumerable<ProjectDTO>>(
                user.Projects.AsEnumerable());

            return projects;

        }

        public void DeleteProject(ProjectDTO project)
        {
            var proj = _unifOfWork.ProjectRepo.Get(project.Name);

            if (proj != null)
            {
                var projct = _mapper.Map<Project>(proj);
                _unifOfWork.ProjectRepo.Delete(projct);
                _unifOfWork.SaveChanges();
            }
        }

        public void EditProject(ProjectDTO project)
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
        }

        public IEnumerable<ProjectDTO> GetAllProjects()
        {
            return _mapper.Map<IEnumerable<Project>, IEnumerable<ProjectDTO>>(
                _unifOfWork.ProjectRepo.GetAll());
        }

        public void AddProject(ProjectDTO project)
        {
            _unifOfWork.ProjectRepo.Create(_mapper.Map<Project>(project));
            _unifOfWork.SaveChanges();
        }

        public ProjectDTO GetProject(string name)
        {
            var proj = _unifOfWork.ProjectRepo.GetAll()
                .FirstOrDefault(x => x.Name.Equals(name));

            if (proj != null)
            {
                return new ProjectDTO()
                {
                    ClientEmail = proj.ClientEmail,
                    Description = proj.Description,
                    Employees = _mapper.Map<ICollection<UserDTO>>(proj.Employees),
                    EndTime = proj.EndTime,
                    Id = proj.Id,
                    Name = proj.Name,
                    PercentCompletion = proj.PercentCompletion,
                    StartTime = proj.StartTime,
                    Status = _mapper.Map<BLL.Enums.StatusDTO>(proj.Status),
                    Tasks = _mapper.Map<ICollection<TaskDTO>>(proj.Tasks)
                };
            }
            else return null;
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
