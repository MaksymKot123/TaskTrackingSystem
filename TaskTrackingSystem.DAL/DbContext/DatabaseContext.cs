using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskTrackingSystem.DAL.Models;

namespace TaskTrackingSystem.DAL.DbContext
{
    public class DatabaseContext : IdentityDbContext<User>
    {
        public DbSet<Project> Projects { get; set; }
        public DbSet<TaskProject> Tasks { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> option) : base(option)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Project>()
                .HasMany(p => p.Tasks)
                .WithOne(t => t.Project)
                .IsRequired();

            builder.Entity<Project>()
                .HasMany(p => p.Employees)
                .WithMany(e => e.Projects)
                .UsingEntity(x => x.ToTable("ProjectEmployee"));

            builder.Entity<IdentityRole>()
                .HasData(new IdentityRole()
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    Id = "Admin_ID",
                    ConcurrencyStamp = "Admin_ID"
                });

            builder.Entity<IdentityRole>()
                .HasData(new IdentityRole()
                {
                    Name = "Employee",
                    NormalizedName = "EMPLOYEE",
                    Id = "Employee_ID",
                    ConcurrencyStamp = "Employee_ID"
                });

            builder.Entity<IdentityRole>()
                .HasData(new IdentityRole()
                {
                    Name = "Manager",
                    NormalizedName = "MANAGER",
                    Id = "Manager_ID",
                    ConcurrencyStamp = "Manager_ID"
                });

            var user1 = new User()
            {
                Email = "mymail@mail.com",
                Id = "3123123",
                Name = "Bob",
                UserName = "mymail@mail.com",
            };

            var user2 = new User()
            {
                Email = "joe@gmail.com",
                Id = "dasdqwe1",
                Name = "Joe",
                UserName = "joe@gmail.com",
            };

            var user3 = new User()
            {
                Email = "john@yahoo.com",
                Id = "drrj321tr",
                Name = "Zack",
                UserName = "john@yahoo.com",
            };

            var ph = new PasswordHasher<User>();

            user1.PasswordHash = ph.HashPassword(user1, "qwe123Aa");
            user2.PasswordHash = ph.HashPassword(user2, "qwe123Aa");
            user3.PasswordHash = ph.HashPassword(user3, "qwe123Aa");


            builder.Entity<User>()
                .HasData(user1, user2, user3);



            builder.Entity<IdentityUserRole<string>>()
                .HasData(new IdentityUserRole<string>()
                {
                    RoleId = "Admin_Id",
                    UserId = "3123123",
                });

            builder.Entity<IdentityUserRole<string>>()
                .HasData(new IdentityUserRole<string>()
                {
                    RoleId = "Employee_Id",
                    UserId = "drrj321tr",
                });

            builder.Entity<IdentityUserRole<string>>()
                .HasData(new IdentityUserRole<string>()
                {
                    RoleId = "Manager_Id",
                    UserId = "dasdqwe1",
                });


        }
    }
}
