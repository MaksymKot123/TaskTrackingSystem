using System;
using System.Collections.Generic;
using TaskTrackingSystem.DAL.Enums;

namespace TaskTrackingSystem.DAL.Models
{
    /// <summary>
    /// Entity of project
    /// </summary>
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Status Status { get; set; }
        public string Description { get; set; }
        public string ClientEmail { get; set; }
        public double PercentCompletion { get; set; }

        public ICollection<User> Employees { get; set; }
        public ICollection<TaskProject> Tasks { get; set; }
    }
}
