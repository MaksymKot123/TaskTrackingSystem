using System;
using System.ComponentModel.DataAnnotations;

namespace TaskTrackingSystem.ViewModels
{
    /// <summary>
    /// A model of project, which controllers return in json
    /// </summary>
    public class ProjectView
    {
        [Required]
        public string Name { get; set; }
        public DateTime StartTime { get; set; }
        [Required]
        public DateTime EndTime { get; set; }
        public string Description { get; set; }
        [Required]
        public string ClientEmail { get; set; }
        public double PercentCompletion { get; internal set; }
        public string Status { get; internal set; }
    }
}
