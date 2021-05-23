using System;
using System.Collections.Generic;
using System.Text;
using TaskTrackingSystem.DAL.Models;

namespace TaskTrackingSystem.BLL.Interfaces
{
    /// <summary>
    /// Interface of jwt generator
    /// </summary>
    public interface IJwtGenerator
    {
        /// <summary>
        /// This method generates a JWT token
        /// </summary>
        /// <param name="user"></param>
        /// <param name="role"></param>
        /// <returns>JWT token</returns>
        string CreateToken(User user, string role);
    }
}
