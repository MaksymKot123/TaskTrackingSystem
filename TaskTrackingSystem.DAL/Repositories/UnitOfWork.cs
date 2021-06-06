using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using TaskTrackingSystem.DAL.DbContext;
using TaskTrackingSystem.DAL.Interfaces;
using TaskTrackingSystem.DAL.Models;

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
        private readonly DatabaseContext _db;

        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<User> _signInManager;

        /// <summary>
        /// <see cref="DAL.Interfaces.ITaskRepository"/>
        /// </summary>
        private readonly ITaskRepository _taskRepo;

        /// <summary>
        /// <see cref="DAL.Interfaces.IRepository{T}"/>
        /// </summary>
        private readonly IRepository<Project> _projectRepo;

        private bool disposedValue;

        public UnitOfWork(DatabaseContext db, UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager, SignInManager<User> signInManager)
        {
            this._db = db;
            this._userManager = userManager;
            this._roleManager = roleManager;
            this._signInManager = signInManager;
            _taskRepo = new TaskRepository(db);
            _projectRepo = new ProjectRepository(db);
        }

        public UserManager<User> UserManager
        {
            get => _userManager;
        }

        public RoleManager<IdentityRole> RoleManager
        {
            get => _roleManager;
        }

        public SignInManager<User> SignInManager
        {
            get => _signInManager;
        }

        /// <summary>
        /// <see cref="DAL.Interfaces.ITaskRepository"/>
        /// </summary>
        public ITaskRepository TaskRepo
        {
            get => _taskRepo;
        }

        /// <summary>
        /// <see cref="DAL.Interfaces.IRepository{T}"/>
        /// </summary>
        public IRepository<Project> ProjectRepo
        {
            get => _projectRepo;
        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _userManager.Dispose();
                    _roleManager.Dispose();
                    _projectRepo.Dispose();
                    _taskRepo.Dispose();
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
            return _db.Users
                .Include(x => x.Projects)
                .ThenInclude(x => x.Tasks)
                .FirstOrDefault(x => x.Email.Equals(email));
        }
    }
}
