using System;
using System.Collections.Generic;
using System.Linq;
using TaskTrackingSystem.DAL.Models;
using TaskTrackingSystem.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace TaskTrackingSystem.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext db;

        private UserManager<User> userManager;
        private RoleManager<IdentityRole> roleManager;
        private SignInManager<User> signInManager;
        private IRepository<TaskProject> taskRepo;
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

        public IRepository<TaskProject> TaskRepo
        {
            get => taskRepo ??= new TaskRepository(db);
        }

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

        public User GetUserWithDetails(string email)
        {
            return db.Users
                .Include(x => x.Projects)
                .FirstOrDefault(x => x.Email.Equals(email));
        }
    }
}
