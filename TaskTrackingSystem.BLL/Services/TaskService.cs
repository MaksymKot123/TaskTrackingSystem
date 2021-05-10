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

        public void AddToProject(string projectName, TaskDTO task)
        {
            var proj = _unitOfWork.ProjectRepo.Get(projectName);
            if (proj != null)
                proj.Tasks.Add(_mapper.Map<TaskProject>(task));
        }

        public void Change(TaskDTO task)
        {
            _unitOfWork.TaskRepo.Edit(_mapper.Map<TaskProject>(task));
        }

        public void Delete(TaskDTO task)
        {
            _unitOfWork.TaskRepo.Delete(_mapper.Map<TaskProject>(task));
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
