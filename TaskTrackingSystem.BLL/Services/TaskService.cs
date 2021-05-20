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
using TaskTrackingSystem.BLL.ProjectStatusUpdater;

namespace TaskTrackingSystem.BLL.Services
{
    public class TaskService : ITaskService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private bool disposedValue;

        public TaskService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IEnumerable<TaskDTO> GetTasksOfProject(string projectName)
        {
            var tasks = _unitOfWork.ProjectRepo.GetAll()
                .FirstOrDefault(x => x.Name.Equals(projectName))
                .Tasks.AsEnumerable();

            var res = _mapper.Map<IEnumerable<TaskDTO>>(tasks);

            return res;
        }

        public void AddToProject(string projectName, TaskDTO task)
        {
            var proj = _unitOfWork.ProjectRepo.Get(projectName);
            if (proj != null)
            {
                var taskFromProj = proj.Tasks
                    .FirstOrDefault(x => x.TaskName.Equals(task.TaskName));

                if (taskFromProj == null)
                {
                    var newTask = _mapper.Map<TaskProject>(task);
                    proj.Tasks.Add(newTask);
                    ProjectStatusUpdater.ProjectStatusUpdater.UpdateInfo(_unitOfWork);
                }
                
            }    
        }

        public void Change(TaskDTO task)
        {
            var tsk = _unitOfWork.TaskRepo.GetWithDetails(task.TaskName, task.Project.Name);
            tsk.Status = _mapper.Map<DAL.Enums.Status>(task.Status);
            _unitOfWork.TaskRepo.Edit(tsk);
            ProjectStatusUpdater.ProjectStatusUpdater.UpdateInfo(_unitOfWork);
        }

        public void Delete(TaskDTO task)
        {
            var tsk = _unitOfWork.TaskRepo.GetWithDetails(task.TaskName, task.Project.Name);
            if (tsk != null)
            {
                _unitOfWork.TaskRepo.Delete(tsk);
                ProjectStatusUpdater.ProjectStatusUpdater.UpdateInfo(_unitOfWork);
            }
            
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _unitOfWork.Dispose();
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
