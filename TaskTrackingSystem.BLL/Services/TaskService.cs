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
using TaskTrackingSystem.BLL.Exceptions;

namespace TaskTrackingSystem.BLL.Services
{
    /// <summary>
    /// A task service
    /// </summary>
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
        /// <summary>
        /// This method return a list of DTO tasks of project.
        /// Project can be got by name
        /// </summary>
        /// <param name="projectName"></param>
        /// <returns></returns>
        public IEnumerable<TaskDTO> GetTasksOfProject(string projectName)
        {
            var tasks = _unitOfWork.ProjectRepo.GetAll()
                .FirstOrDefault(x => x.Name.Equals(projectName))
                .Tasks.AsEnumerable();

            var res = _mapper.Map<IEnumerable<TaskDTO>>(tasks);

            return res;
        }

        /// <summary>
        /// This method adds a new task to project. Project can be 
        /// got by name. If there is not any project with this name,
        /// a project exception will be thrown. If project already has
        /// a task, which name is same as new task's name, a task exception
        /// will be thrown
        /// </summary>
        /// <param name="projectName"></param>
        /// <param name="task"></param>
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
                else
                {
                    throw new TaskException(@"Project has a task with this name.
                        Please use other name.");
                }
            }
            else
            {
                throw new ProjectException("Project not found");
            }
        }

        /// <summary>
        /// This method changes task's info. If there is not any task, which name
        /// is same as name of DTO task from parameter, a task exception will be thrown
        /// </summary>
        /// <param name="task"></param>
        public void Change(TaskDTO task)
        {
            var tsk = _unitOfWork.TaskRepo.GetWithDetails(task.TaskName, task.Project.Name);
            if (tsk != null)
            {
                tsk.Status = _mapper.Map<DAL.Enums.Status>(task.Status);
                _unitOfWork.TaskRepo.Edit(tsk);
                ProjectStatusUpdater.ProjectStatusUpdater.UpdateInfo(_unitOfWork);
            }
            else
            {
                throw new TaskException("Task not found");
            }
        }

        /// <summary>
        /// This method deletes task from project. If there is not any task, which name
        /// is same as name of DTO task from parameter, a task exception will be thrown
        /// </summary>
        /// <param name="task"></param>
        public void Delete(TaskDTO task)
        {
            var tsk = _unitOfWork.TaskRepo.GetWithDetails(task.TaskName, task.Project.Name);
            if (tsk != null)
            {
                _unitOfWork.TaskRepo.Delete(tsk);
                ProjectStatusUpdater.ProjectStatusUpdater.UpdateInfo(_unitOfWork);
            }
            else
            {
                throw new TaskException("Task not found");
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
