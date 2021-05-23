using System;
using TaskTrackingSystem.DAL.Enums;

namespace TaskTrackingSystem.DAL.Models
{
    /// <summary>
    /// Entity of project's task
    /// </summary>
    public class TaskProject
    {
        public int Id { get; set; }
        public string TaskName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Status Status { get; set; }
        public string Description { get; set; }
        public Project Project { get; set; }
    }
}