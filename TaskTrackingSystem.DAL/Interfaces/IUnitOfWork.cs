using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using TaskTrackingSystem.DAL.Models;

namespace TaskTrackingSystem.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable, IGetUserWithDetails
    {
        UserManager<User> UserManager { get; }
        RoleManager<IdentityRole> RoleManager { get; }
        SignInManager<User> SignInManager { get; }
        IRepository<TaskProject> TaskRepo { get; }
        IRepository<Project> ProjectRepo { get; }
        void SaveChanges();
    }
}
