using System;
using System.Collections.Generic;
using System.Linq;
using TaskTrackingSystem.DAL.Models;
using TaskTrackingSystem.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace TaskTrackingSystem.DAL.Repositories
{
    public class UserRepository : IRepository<User>
    {
        public DatabaseContext db { get; set; }

        public UserRepository(DatabaseContext context)
        {
            db = context;
        }

        public void Create(User item)
        {
            var entity = db.Users.FirstOrDefault(x => x.Email.Equals(item.Email));
            if (entity != null)
            {
                db.Users.Add(item);
                db.SaveChanges();
            }
        }

        public void Delete(User item)
        {
            var entity = db.Users.FirstOrDefault(x => x.Email.Equals(item.Email));
            if (entity != null)
            {
                db.Users.Remove(entity);
                db.SaveChanges();
            }
                
        }

        public void Edit(User item)
        {
            var entity = db.Users.FirstOrDefault(x => x.Email.Equals(item.Email));
            if (entity != null)
            {
                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
            }
            
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public User Get(string email)
        {
            if (email == null)
                return null;
            else
                return db.Users
                    .Include(x => x.Projects)
                    .FirstOrDefault(x => x.Email.Equals(email));
        }

        public IEnumerable<User> GetAll() => db.Users.Include(x => x.Projects);
    }
}
