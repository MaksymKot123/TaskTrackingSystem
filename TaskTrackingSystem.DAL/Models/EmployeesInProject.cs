using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TaskTrackingSystem.DAL.Models
{
    public class EmployeesInProject
    {
        public string EmployeeId { get; set; }
        public int ProjectId { get; set; }

        [ForeignKey("EmployeeId")]
        public User Employee { get; set; }
        [ForeignKey("ProjectId")]
        public Project Project { get; set; }
    }
}
