using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace TaskTrackingSystem.DAL.Models
{
    public class User : IdentityUser
    {
        /// <summary>
        /// Entity of user
        /// </summary>
        public string Name { get; set; }
        public ICollection<Project> Projects { get; set; }
        public IdentityRole Role { get; set; }
    }
}