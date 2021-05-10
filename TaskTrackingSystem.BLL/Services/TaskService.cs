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
            {
                var newTask = _mapper.Map<TaskProject>(task);
                if (proj.Tasks == null)
                {

                    proj.Tasks = new List<TaskProject>() { newTask };
                    _unitOfWork.SaveChanges();
                }
                else
                {

                    proj.Tasks.Add(newTask);
                    _unitOfWork.SaveChanges();
                }
                //_unitOfWork.TaskRepo.
            }
                
        }

        public void Change(TaskDTO task)
        {
            _unitOfWork.TaskRepo.Edit(_mapper.Map<TaskProject>(task));
            _unitOfWork.SaveChanges();
        }

        public void Delete(TaskDTO task)
        {
            var tsk = _unitOfWork.TaskRepo.Get(task.TaskName);
            if (tsk != null)
            {
                _unitOfWork.TaskRepo.Delete(tsk);
                _unitOfWork.SaveChanges();
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
