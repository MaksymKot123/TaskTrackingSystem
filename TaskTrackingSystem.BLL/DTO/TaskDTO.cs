using System;
using TaskTrackingSystem;

namespace TaskTrackingSystem.BLL.DTO
{
    public class TaskDTO
    {
        public int Id { get; set; }
        public string TaskName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DAL.Enums.Status Status { get; set; }
        public string Description { get; set; }
        public virtual ProjectDTO Project { get; set; }
    }
}