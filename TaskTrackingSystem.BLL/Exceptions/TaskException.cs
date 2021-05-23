using System;
using System.Collections.Generic;
using System.Text;

namespace TaskTrackingSystem.BLL.Exceptions
{
    /// <summary>
    /// A class of exception for tasks
    /// </summary>
    public class TaskException : Exception
    {
        public TaskException(string message) : base(message) { }
    }
}
