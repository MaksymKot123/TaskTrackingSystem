using System;
using System.Collections.Generic;
using System.Text;

namespace TaskTrackingSystem.BLL.Exceptions
{
    public class TaskException : Exception
    {
        public TaskException(string message) : base(message) { }
    }
}
