using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using TaskTrackingSystem.DAL.Models;

namespace TaskTrackingSystem.DAL.Interfaces
{
    public interface IUnifOfWork : IDisposable
    {
        UserManager<User> UserManager { get; }
        RoleManager<User> RoleManager { get; }

        IRepository<TaskProject> TaskRepo { get; }
        IRepository<Project> ProjectRepo { get; }
        IRepository<User> UserRepo { get; }

    }
}
