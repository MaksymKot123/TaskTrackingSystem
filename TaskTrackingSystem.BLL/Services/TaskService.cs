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
        private readonly IUnitOfWork unifOfWork;
        private bool disposedValue;

        public TaskService(IUnitOfWork uow)
        {
            unifOfWork = uow;
        }

        public void AddToProject(ProjectDTO project, TaskDTO task)
        {
            throw new NotImplementedException();
        }

        public void Change(TaskDTO task)
        {
            throw new NotImplementedException();
        }

        public void Delete(TaskDTO task)
        {
            throw new NotImplementedException();
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
