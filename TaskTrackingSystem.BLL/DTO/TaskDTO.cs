using System;
using TaskTrackingSystem;
using TaskTrackingSystem.BLL;

namespace TaskTrackingSystem.BLL.DTO
{
    public class TaskDTO
    {
        public int Id { get; set; }
        public string TaskName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Enums.StatusDTO Status { get; set; }
        public string Description { get; set; }
        public ProjectDTO Project { get; set; }
    }
}