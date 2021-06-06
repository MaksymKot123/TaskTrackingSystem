using System;

namespace TaskTrackingSystem.BLL.Exceptions
{
    /// <summary>
    /// A class of exception for users
    /// </summary>
    public class UserException : Exception
    {
        public UserException(string message) : base(message) { }
    }
}
