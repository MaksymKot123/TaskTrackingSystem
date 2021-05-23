using System;
using System.Collections.Generic;
using System.Text;
using TaskTrackingSystem;

namespace TaskTrackingSystem.BLL.DTO
{
    /// <summary>
    /// DTO model of project
    /// </summary>
    public class ProjectDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public BLL.Enums.StatusDTO Status { get; set; }
        public string Description { get; set; }
        public string ClientEmail { get; set; }
        public double PercentCompletion { get; set; }
        public ICollection<UserDTO> Employees { get; set; }
        public ICollection<TaskDTO> Tasks { get; set; }
    }
}
