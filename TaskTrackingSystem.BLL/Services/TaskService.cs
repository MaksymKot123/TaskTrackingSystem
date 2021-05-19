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
                }
            }    
        }

        public void Change(TaskDTO task)
        {
            var tsk = _mapper.Map<TaskProject>(task);
            _unitOfWork.TaskRepo.Edit(tsk);
            _unitOfWork.SaveChanges();
        }

        public void Delete(TaskDTO task)
        {
            var tsk = _unitOfWork.TaskRepo.GetWithDetails(task.TaskName, task.Project.Name);
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
