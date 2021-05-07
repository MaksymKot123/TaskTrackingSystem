using System;
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
        private IUnitOfWork unifOfWork;

        public ProjectService(IUnitOfWork uow)
        {
            unifOfWork = uow;
        }

        public void EditProject(ProjectDTO project)
        {
            var name = project.Name;

            var proj = unifOfWork.ProjectRepo.Find(x => x.Name.Equals(name)).First();

            var statusConfig = new MapperConfiguration(cfg => cfg
                .CreateMap<BLL.Enums.StatusDTO, DAL.Enums.Status > ());
            
            var statusMapper = new Mapper(statusConfig);

            if (proj != null)
            {
                proj.EndTime = project.EndTime;
                proj.Description = project.Description;
                proj.Name = project.Name;
                proj.Status = statusMapper
                    .Map<DAL.Enums.Status>(proj.Status);

                unifOfWork.ProjectRepo.Edit(proj);
            }

        }

        public IEnumerable<ProjectDTO> Find(Func<ProjectDTO, bool> func)
        {
            var configGet = new MapperConfiguration(cfg => cfg
                .CreateMap<Func<ProjectDTO, bool>, Func<Project, bool>>());
            var mapperGet = new Mapper(configGet);
            var pred = mapperGet.Map<Func<Project, bool>>(func);

            var user = unifOfWork.ProjectRepo.Find(pred);

            var config_return = new MapperConfiguration(cfg => cfg
                .CreateMap<IEnumerable<Project>, IEnumerable<ProjectDTO>>());
            var mapperReturn = new Mapper(config_return);
            return mapperReturn.Map<IEnumerable<ProjectDTO>>(user);
        }

        public IEnumerable<ProjectDTO> GetAllProjects()
        {
            var config = new MapperConfiguration(cfg => cfg
                .CreateMap<IEnumerable<Project>, IEnumerable<ProjectDTO>>());

            var mapper = new Mapper(config);

            return mapper.Map<IEnumerable<Project>, IEnumerable<ProjectDTO>>(
                unifOfWork.ProjectRepo.GetAll());
        }

        public ProjectDTO GetProject(string name)
        {
            var proj = unifOfWork.ProjectRepo.Find(x => x.Name.Equals(name)).First();

            if (proj != null)
            {
                var taskConfig = new MapperConfiguration(cfg => cfg
                    .CreateMap<ICollection<TaskProject>, ICollection<TaskDTO>>());
                var taskMapper = new Mapper(taskConfig);

                var projectTasks = taskMapper.Map<ICollection<TaskProject>,
                    ICollection<TaskDTO>>(proj.Tasks);

                var employeesConfig = new MapperConfiguration(cfg => cfg
                    .CreateMap<ICollection<User>, ICollection<UserDTO>>());
                var employeeMapper = new Mapper(employeesConfig);

                var projectEmployees = employeeMapper.Map<ICollection<User>,
                        ICollection<UserDTO>>(proj.Employees);


                var statusConfig = new MapperConfiguration(cfg => cfg
                    .CreateMap<BLL.Enums.StatusDTO, DAL.Enums.Status>());
                var statusMapper = new Mapper(statusConfig);

                var projectStatus = statusMapper.Map<BLL.Enums.StatusDTO>(proj.Status);

                return new ProjectDTO()
                {
                    ClientEmail = proj.ClientEmail,
                    Description = proj.Description,
                    Employees = projectEmployees,
                    EndTime = proj.EndTime,
                    Id = proj.Id,
                    Name = proj.Name,
                    PercentCompletion = proj.PercentCompletion,
                    StartTime = proj.StartTime,
                    Status = projectStatus,
                    Tasks = projectTasks,
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
                    unifOfWork.Dispose();
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
