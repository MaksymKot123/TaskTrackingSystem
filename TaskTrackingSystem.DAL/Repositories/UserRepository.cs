using System;
using System.Collections.Generic;
using System.Linq;
using TaskTrackingSystem.DAL.Models;
using TaskTrackingSystem.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using TaskTrackingSystem.DAL.DbContext;

namespace TaskTrackingSystem.DAL.Repositories
{
    public class UserRepository : IRepository<User>
    {
        /// <summary>
        /// <see cref="DAL.Models.DatabaseContext"/>
        /// </summary>
        public DatabaseContext db { get; set; }

        public UserRepository(DatabaseContext context)
        {
            db = context;
        }

        /// <summary>
        /// Add new user to database
        /// </summary>
        /// <param name="item"></param>
        public void Create(User item)
        {
            var entity = db.Users.FirstOrDefault(x => x.Email.Equals(item.Email));
            if (entity != null)
            {
                db.Users.Add(item);
            }
        }

        /// <summary>
        /// Detele user from database
        /// </summary>
        /// <param name="item"></param>
        public void Delete(User item)
        {
            var entity = db.Users.FirstOrDefault(x => x.Email.Equals(item.Email));
            if (entity != null)
            {
                db.Users.Remove(entity);
            }
                
        }

        /// <summary>
        /// Edit user from database
        /// </summary>
        /// <param name="item"></param>
        public void Edit(User item)
        {
            var entity = db.Users.FirstOrDefault(x => x.Email.Equals(item.Email));
            if (entity != null)
            {
                entity.Name = item.Name;
                db.Entry(entity).State = EntityState.Modified;
            }
            
        }

        public void Dispose()
        {
            db.Dispose();
        }

        /// <summary>
        /// Get user from database by email with details
        /// </summary>
        /// <param name="email"></param>
        /// <returns><see cref="DAL.Models.User"/></returns>
        public User Get(string email)
        {
            if (email == null)
                return null;
            else
                return db.Users
                    .Include(x => x.Projects)
                    .FirstOrDefault(x => x.Email.Equals(email));
        }

        /// <summary>
        /// Get all users with details
        /// </summary>
        /// <returns>A list of <see cref="DAL.Models.User"/></returns>
        public IEnumerable<User> GetAll() => db.Users.Include(x => x.Projects);
    }
}
