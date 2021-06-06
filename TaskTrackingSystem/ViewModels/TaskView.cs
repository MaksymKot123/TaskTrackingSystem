using System;
using System.ComponentModel.DataAnnotations;

namespace TaskTrackingSystem.ViewModels
{
    /// <summary>
    /// A model of task, which controllers return in json 
    /// </summary>
    public class TaskView
    {
        [Required]
        public string TaskName { get; set; }
        [Required]
        public string ProjName { get; set; }
        public DateTime StartTime { get; set; }
        [Required]
        public DateTime EndTime { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
    }
}
