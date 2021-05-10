using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using TaskTrackingSystem.DAL.Models;
using TaskTrackingSystem.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace TaskTrackingSystem.DAL.Repositories
{
    public class TaskRepository : IRepository<TaskProject>
    {
        public DatabaseContext db { get; set; }

        public TaskRepository(DatabaseContext context)
        {
            db = context;
        }

        public void Create(TaskProject item)
        {
            db.Tasks.Add(item);
            db.SaveChanges();
        }

        public void Delete(TaskProject item)
        {
            db.Tasks.Remove(item);
            db.SaveChanges();
        }

        public void Edit(TaskProject item)
        {
            var entity = db.Tasks
                .FirstOrDefault(x => x.TaskName.Equals(item.TaskName));

            entity.StartTime = item.StartTime;
            entity.Status = item.Status;
            entity.TaskName = item.TaskName;
            entity.EndTime = item.EndTime;
            entity.Description = item.Description;

            db.Entry(entity).State = EntityState.Modified;
            db.SaveChanges();
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
                return db.Tasks.FirstOrDefault(x => x.TaskName.Equals(name));
        }

        public IEnumerable<TaskProject> GetAll() 
            => db.Tasks.Include(x => x.Project);
    }
}
