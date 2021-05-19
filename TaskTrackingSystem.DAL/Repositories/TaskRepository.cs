using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using TaskTrackingSystem.DAL.Models;
using TaskTrackingSystem.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using TaskTrackingSystem.DAL.UpdatePercentOfCompletionAndStatus;

namespace TaskTrackingSystem.DAL.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        public DatabaseContext db { get; set; }

        public TaskRepository(DatabaseContext context)
        {
            db = context;
        }

        public void Create(TaskProject item)
        {
            var entity = db.Tasks
                .FirstOrDefault(x => x.TaskName.Equals(item.TaskName) && 
                x.Project.Name.Equals(item.Project.Name));

            if (entity == null)
            {
                db.Tasks.Add(item);
                db.SaveChanges();
            }
            
        }

        public void Delete(TaskProject item)
        {
            var entity = db.Tasks
                .FirstOrDefault(x => x.TaskName.Equals(item.TaskName) && 
                x.Project.Name.Equals(item.Project.Name));

            if (entity != null)
            {
                db.Tasks.Remove(entity);
                db.SaveChanges();
            }
            
        }

        public void Edit(TaskProject item)
        {
            var entity = db.Tasks
                .FirstOrDefault(x => x.TaskName.Equals(item.TaskName) &&
                x.Project.Name.Equals(item.Project.Name));

            if (entity != null)
            {
                entity.StartTime = item.StartTime;
                entity.Status = item.Status;
                entity.EndTime = item.EndTime;
                entity.Description = item.Description;
                
                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
            } 
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public TaskProject Get(string name)
        {
            if (name == null)
                return null;
            else
                return db.Tasks.Include(x => x.Project)
                    .FirstOrDefault(x => x.TaskName.Equals(name));
        }

        public TaskProject GetWithDetails(string taskName, string projName)
        {
            if (taskName == null || projName == null)
                return null;
            else
            {
                return db.Tasks
                    .Include(x => x.Project)
                    .ThenInclude(x => x.Tasks)
                    .FirstOrDefault(x => x.TaskName.Equals(taskName) &&
                    x.Project.Name.Equals(projName));
            }
        }

        public IEnumerable<TaskProject> GetAll() 
            => db.Tasks.Include(x => x.Project);
    }
}
