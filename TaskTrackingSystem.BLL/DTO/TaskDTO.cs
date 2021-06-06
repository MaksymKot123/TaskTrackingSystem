using System;

namespace TaskTrackingSystem.BLL.DTO
{
    /// <summary>
    /// DTO model of project's task
    /// </summary>
    public class TaskDto
    {
        public int Id { get; set; }
        public string TaskName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Enums.StatusDTO Status { get; set; }
        public string Description { get; set; }
        public ProjectDto Project { get; set; }
    }
}