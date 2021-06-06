using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskTrackingSystem.BLL.DTO;
using TaskTrackingSystem.DAL.Interfaces;

namespace TaskTrackingSystem.BLL.Interfaces
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
        /// <returns>A list of <see cref="BLL.DTO.UserDto"/></returns>
        IEnumerable<UserDto> GetAllUsers();

        /// <summary>
        /// Get user by email
        /// </summary>
        /// <param name="email"></param>
        /// <returns><see cref="BLL.DTO.UserDto"/></returns>
        Task<UserDto> GetUser(string email);

        /// <summary>
        /// Change users info
        /// </summary>
        /// <param name="user"></param>
        void EditUser(UserDto user);

        /// <summary>
        /// Delete user
        /// </summary>
        /// <param name="user"></param>
        Task DeleteUser(UserDto user);

        /// <summary>
        /// Add user to project. Project can be got by name
        /// </summary>
        /// <param name="projectName"></param>
        /// <param name="user"></param>
        void AddToProject(string projectName, UserDto user);

        /// <summary>
        /// Register a new account
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns><see cref="BLL.DTO.UserDto"/></returns>
        Task<UserDto> Register(UserDto user, string password);

        /// <summary>
        /// Get JWT token of user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns><see cref="BLL.DTO.UserDto"/> with JWT token</returns>
        Task<UserDto> Authenticate(UserDto user, string password);

        /// <summary>
        /// Get users with specific role
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns>A list of <see cref="BLL.DTO.UserDto"/></returns>
        Task<IEnumerable<UserDto>> GetUsersByRole(string roleName);

        Task ChangeUsersRole(string roleName, string userEmail);
    }
}
