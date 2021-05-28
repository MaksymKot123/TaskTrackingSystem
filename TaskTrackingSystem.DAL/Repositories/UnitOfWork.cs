using System;
using System.Collections.Generic;
using System.Linq;
using TaskTrackingSystem.DAL.Models;
using TaskTrackingSystem.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using TaskTrackingSystem.DAL.DbContext;

namespace TaskTrackingSystem.DAL.Repositories
{
    /// <summary>
    /// a class, which implements <see cref="DAL.Interfaces.IUnitOfWork"/>
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// <see cref="DAL.Models.DatabaseContext"/>
        /// </summary>
        private readonly DatabaseContext db;

        private UserManager<User> userManager;
        private RoleManager<IdentityRole> roleManager;
        private SignInManager<User> signInManager;

        /// <summary>
        /// <see cref="DAL.Interfaces.ITaskRepository"/>
        /// </summary>
        private ITaskRepository taskRepo;

        /// <summary>
        /// <see cref="DAL.Interfaces.IRepository{T}"/>
        /// </summary>
        private IRepository<Project> projectRepo;

        private bool disposedValue;

        public UnitOfWork(DatabaseContext db, UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager, SignInManager<User> signInManager)
        {
            this.db = db;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.signInManager = signInManager;
            taskRepo = new TaskRepository(db);  
            projectRepo = new ProjectRepository(db);
        }

        public UserManager<User> UserManager
        {
            get => userManager;
        }

        public RoleManager<IdentityRole> RoleManager
        {
            get => roleManager;
        }

        public SignInManager<User> SignInManager
        {
            get => signInManager;
        }

        /// <summary>
        /// <see cref="DAL.Interfaces.ITaskRepository"/>
        /// </summary>
        public ITaskRepository TaskRepo
        {
            get => taskRepo ??= new TaskRepository(db);
        }

        /// <summary>
        /// <see cref="DAL.Interfaces.IRepository{T}"/>
        /// </summary>
        public IRepository<Project> ProjectRepo
        {
            get => projectRepo ??= new ProjectRepository(db);
        }

        public void SaveChanges()
        {
            db.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    userManager.Dispose();
                    roleManager.Dispose();
                    projectRepo.Dispose();
                    taskRepo.Dispose();
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Get user with details
        /// </summary>
        /// <param name="email"></param>
        /// <returns><see cref="DAL.Models.User"/></returns>
        public User GetUserWithDetails(string email)
        {
            return db.Users
                .Include(x => x.Projects)
                .ThenInclude(x => x.Tasks)
                .FirstOrDefault(x => x.Email.Equals(email));
        }
    }
}
