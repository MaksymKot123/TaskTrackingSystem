using Microsoft.AspNetCore.Identity;
using System;
using TaskTrackingSystem.DAL.Models;

namespace TaskTrackingSystem.DAL.Interfaces
{
    /// <summary>
    /// interface for unit of work
    /// </summary>
    public interface IUnitOfWork : IDisposable, IGetUserWithDetails
    {
        UserManager<User> UserManager { get; }
        RoleManager<IdentityRole> RoleManager { get; }
        SignInManager<User> SignInManager { get; }
        /// <summary>
        /// <see cref="DAL.Interfaces.ITaskRepository"/>
        /// </summary>
        ITaskRepository TaskRepo { get; }
        /// <summary>
        /// <see cref="DAL.Interfaces.IRepository{T}"/>
        /// </summary>
        IRepository<Project> ProjectRepo { get; }
        void SaveChanges();
    }
}
