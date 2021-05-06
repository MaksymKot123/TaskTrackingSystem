using System;
using System.Collections.Generic;
using System.Text;
using TaskTrackingSystem.DAL.Enums;

namespace TaskTrackingSystem.DAL.Models
{
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

        public virtual ICollection<User> Employees { get; set; }
        public virtual ICollection<TaskProject> Tasks { get; set; }
    }
}
