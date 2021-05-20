using System;
using System.Collections.Generic;
using System.Text;

namespace TaskTrackingSystem.BLL.Exceptions
{
    public class ProjectException : Exception
    {
        public ProjectException(string message) : base(message) { }
    }
}
