using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TaskTrackingSystem.DAL.Models
{
    public class TaskContext : IdentityDbContext<User>
    {
        public TaskContext(DbContextOptions<TaskContext> option) : base(option)
        {
            Database.EnsureCreated();
        }
    }
}
