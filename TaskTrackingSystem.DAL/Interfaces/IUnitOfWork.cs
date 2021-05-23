using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
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
        ITaskRepository TaskRepo { get; }
        IRepository<Project> ProjectRepo { get; }
        void SaveChanges();
    }
}
