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
            var project = db.Projects.FirstOrDefault(x => x.Name.Equals(item.Name));

            if (project == null)
            {
                db.Projects.Add(item);
                db.SaveChanges();
            }
            
        }

        public void Delete(Project item)
        {
            var project = db.Projects.FirstOrDefault(x => x.Name.Equals(item.Name));

            if (project != null)
            {
                db.Projects.Remove(project);
                db.SaveChanges();
            }
        }

        public void Edit(Project item)
        {
            var project = db.Projects.FirstOrDefault(x => x.Name.Equals(item.Name));

            if (project != null)
            {
                db.Entry(project).State = EntityState.Modified;
                db.SaveChanges();
            }
            
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public Project Get(string name)
        {
            var proj = db.Projects
                .Include(x => x.Tasks)
                .Include(x => x.Employees)
                .FirstOrDefault(x => x.Name.Equals(name));

            return proj;
        }

        public IEnumerable<Project> GetAll() 
            => db.Projects
            .Include(x => x.Tasks)
            .Include(x => x.Employees);
    }
}
