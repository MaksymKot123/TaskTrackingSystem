using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TaskTrackingSystem.DAL.Models
{
    public class DatabaseContext : IdentityDbContext<User>
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<TaskProject> Tasks { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> option) : base(option)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Project>()
                .HasMany(p => p.Tasks)
                .WithOne(t => t.Project)
                .IsRequired();

            builder.Entity<EmployeesInProject>()
                .HasKey(e => new { e.EmployeeId, e.ProjectId });
        }
    }
}
