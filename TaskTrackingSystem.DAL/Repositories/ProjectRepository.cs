using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using TaskTrackingSystem.DAL.Models;
using TaskTrackingSystem.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace TaskTrackingSystem.DAL.Repositories
{
    public class ProjectRepository : IRepository<Project>
    {
        public DatabaseContext db { get; set; }

        public ProjectRepository(DatabaseContext context)
        {
            db = context;
        }

        public void Create(Project item)
        {
            db.Projects.Add(item);
            db.SaveChanges();
        }

        public void Delete(Project item)
        {
            db.Projects.Remove(item);
            db.SaveChanges();
        }

        public void Edit(Project item)
        {
            db.Entry(item).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public Project Get(int? id)
        {
            var proj = db.Projects.Find(id);

            if (proj != null)
                return proj;
            else return null;
        }

        public IEnumerable<Project> GetAll() => db.Projects;
    }
}
