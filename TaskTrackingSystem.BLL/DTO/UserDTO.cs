    using System;
using System.Collections.Generic;
using System.Text;

namespace TaskTrackingSystem.BLL.DTO
{
    public class UserDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
