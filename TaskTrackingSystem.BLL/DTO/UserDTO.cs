using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TaskTrackingSystem.BLL.DTO
{
    public class UserDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        public IdentityRole Role { get; set; }
        public ICollection<ProjectDTO> Projects { get; set; }
        public string Token { get; set; }
    }
}
