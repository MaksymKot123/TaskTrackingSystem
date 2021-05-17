using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TaskTrackingSystem.ViewModels
{
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
