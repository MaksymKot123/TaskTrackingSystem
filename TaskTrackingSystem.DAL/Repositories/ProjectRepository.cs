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
    public class ProjectRepository : IRepository<Project>
    {
        /// <summary>
        /// <see cref="DAL.Models.DatabaseContext"/>
        /// </summary>
        private readonly DatabaseContext _db;

        public ProjectRepository(DatabaseContext context)
        {
            _db = context;
        }

        /// <summary>
        /// Add new project to database
        /// </summary>
        /// <param name="item"></param>
        public void Create(Project item)
        {
            var project = _db.Projects.FirstOrDefault(x => x.Name.Equals(item.Name));

            if (project == null)
            {
                _db.Projects.Add(item);
            }
            
        }

        /// <summary>
        /// delete project from database
        /// </summary>
        /// <param name="item"></param>
        public void Delete(Project item)
        {
            var project = _db.Projects.FirstOrDefault(x => x.Name.Equals(item.Name));

            if (project != null)
            {

                _db.Projects.Remove(project);
            }
        }

        /// <summary>
        /// Edit project from database
        /// </summary>
        /// <param name="item"></param>
        public void Edit(Project item)
        {
            var project = _db.Projects.FirstOrDefault(x => x.Name.Equals(item.Name));

            if (project != null)
            {
                _db.Entry(project).State = EntityState.Modified;
            }
            
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        /// <summary>
        /// Get project by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns><see cref="DAL.Models.Project"/></returns>
        public Project Get(string name)
        {
            var proj = _db.Projects
                .Include(x => x.Tasks)
                .Include(x => x.Employees)
                .FirstOrDefault(x => x.Name.Equals(name));

            return proj;
        }

        /// <summary>
        /// Get all project from database
        /// </summary>
        /// <returns>A list <see cref="DAL.Models.Project"/></returns>
        public IEnumerable<Project> GetAll() 
            => _db.Projects
            .Include(x => x.Tasks)
            .Include(x => x.Employees)
            .AsEnumerable();
    }
}
