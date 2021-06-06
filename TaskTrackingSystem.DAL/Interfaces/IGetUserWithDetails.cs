using TaskTrackingSystem.DAL.Models;

namespace TaskTrackingSystem.DAL.Interfaces
{
    /// <summary>
    /// Interface for getting user with details
    /// </summary>
    public interface IGetUserWithDetails
    {
        /// <summary>
        /// Get user with projects
        /// </summary>
        /// <param name="email"></param>
        /// <returns>An instance of <see cref="DAL.Models.User"/></returns>
        User GetUserWithDetails(string email);
    }
}
