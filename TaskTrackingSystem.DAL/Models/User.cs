using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace TaskTrackingSystem.DAL.Models
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
        public ICollection<Project> Projects { get; set; }
        public IdentityRole<string> Role { get; set; }
        //public virtual ICollection<EmployeesInProject> EmployeesInProject { get; set; }
    }
}