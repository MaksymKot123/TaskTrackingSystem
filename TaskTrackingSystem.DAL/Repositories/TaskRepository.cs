using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using TaskTrackingSystem.DAL.Models;
using TaskTrackingSystem.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using TaskTrackingSystem.DAL.DbContext;

namespace TaskTrackingSystem.DAL.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        /// <summary>
        /// <see cref="DAL.Models.DatabaseContext"/>
        /// </summary>
        private readonly DatabaseContext _db;

        public TaskRepository(DatabaseContext context)
        {
            _db = context;
        }

        /// <summary>
        /// Add new task to database
        /// </summary>
        /// <param name="item"></param>
        public void Create(TaskProject item)
        {
            var entity = _db.Tasks
                .FirstOrDefault(x => x.TaskName.Equals(item.TaskName) && 
                x.Project.Name.Equals(item.Project.Name));

            if (entity == null)
            {
                _db.Tasks.Add(item);
            }
            
        }

        /// <summary>
        /// Delete task from database
        /// </summary>
        /// <param name="item"></param>
        public void Delete(TaskProject item)
        {
            var entity = _db.Tasks
                .FirstOrDefault(x => x.TaskName.Equals(item.TaskName) && 
                x.Project.Name.Equals(item.Project.Name));

            if (entity != null)
            {
                _db.Tasks.Remove(entity);
            }
            
        }

        /// <summary>
        /// Edit task from database
        /// </summary>
        /// <param name="item"></param>
        public void Edit(TaskProject item)
        {
            var entity = _db.Tasks
                .FirstOrDefault(x => x.TaskName.Equals(item.TaskName) &&
                x.Project.Name.Equals(item.Project.Name));

            if (entity != null)
            {
                entity.StartTime = item.StartTime;
                entity.Status = item.Status;
                entity.EndTime = item.EndTime;
                entity.Description = item.Description;
                
                _db.Entry(entity).State = EntityState.Modified;
            } 
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        /// <summary>
        /// Get task from database by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns><see cref="DAL.Models.TaskProject"/></returns>
        public TaskProject Get(string name)
        {
            if (name == null)
                return null;
            else
                return _db.Tasks.Include(x => x.Project)
                    .FirstOrDefault(x => x.TaskName.Equals(name));
        }

        /// <summary>
        /// Get task from database by name with details
        /// </summary>
        /// <param name="taskName"></param>
        /// <param name="projName"></param>
        /// <returns><see cref="DAL.Models.TaskProject"/></returns>
        public TaskProject GetWithDetails(string taskName, string projName)
        {
            if (taskName == null || projName == null)
                return null;
            else
            {
                return _db.Tasks
                    .Include(x => x.Project)
                    .ThenInclude(x => x.Tasks)
                    .FirstOrDefault(x => x.TaskName.Equals(taskName) &&
                    x.Project.Name.Equals(projName));
            }
        }

        /// <summary>
        /// Get all tasks with details from database
        /// </summary>
        /// <returns>A list of <see cref="DAL.Models.TaskProject"/></returns>
        public IEnumerable<TaskProject> GetAll()
            => _db.Tasks.Include(x => x.Project);
    }
}
