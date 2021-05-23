using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaskTrackingSystem.BLL.DTO;
using TaskTrackingSystem.DAL.Interfaces;

namespace TaskTrackingSystem.BLL
{
    /// <summary>
    /// Interface of user service
    /// </summary>
    public interface IUserService : IDisposable
    {
        /// <summary>
        /// <see cref="DAL.Interfaces.IUnitOfWork"/>
        /// </summary>
        public IUnitOfWork UnitOfWork { get; }

        /// <summary>
        /// Get all DTO user models
        /// </summary>
        /// <returns>A list of <see cref="BLL.DTO.UserDTO"/></returns>
        IEnumerable<UserDTO> GetAllUsers();

        /// <summary>
        /// Get user by email
        /// </summary>
        /// <param name="email"></param>
        /// <returns><see cref="BLL.DTO.UserDTO"/></returns>
        Task<UserDTO> GetUser(string email);

        /// <summary>
        /// Change users info
        /// </summary>
        /// <param name="user"></param>
        void EditUser(UserDTO user);

        /// <summary>
        /// Delete user
        /// </summary>
        /// <param name="user"></param>
        void DeleteUser(UserDTO user);

        /// <summary>
        /// Add user to project. Project can be got by name
        /// </summary>
        /// <param name="projectName"></param>
        /// <param name="user"></param>
        void AddToProject(string projectName, UserDTO user);

        /// <summary>
        /// Register a new account
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns><see cref="BLL.DTO.UserDTO"/></returns>
        Task<UserDTO> Register(UserDTO user, string password);

        /// <summary>
        /// Get JWT token of user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns><see cref="BLL.DTO.UserDTO"/> with JWT token</returns>
        Task<UserDTO> Authenticate(UserDTO user, string password);

        /// <summary>
        /// Get users with specific role
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns>A list of <see cref="BLL.DTO.UserDTO"/></returns>
        Task<IEnumerable<UserDTO>> GetUsersByRole(string roleName);
    }
}
