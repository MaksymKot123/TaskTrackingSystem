using System;

namespace TaskTrackingSystem.BLL.Exceptions
{
    /// <summary>
    /// A class of exception for projects
    /// </summary>
    public class ProjectException : Exception
    {
        public ProjectException(string message) : base(message) { }
    }
}
