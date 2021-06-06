using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TaskTrackingSystem.BLL.DTO
{
    /// <summary>
    /// DTO model of user
    /// </summary>
    public class UserDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        public IdentityRole Role { get; set; }
        public ICollection<ProjectDto> Projects { get; set; }
        public string Token { get; set; }
    }
}
