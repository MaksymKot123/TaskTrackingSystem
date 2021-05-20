using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaskTrackingSystem.BLL.DTO;
using TaskTrackingSystem.DAL.Interfaces;

namespace TaskTrackingSystem.BLL
{
    public interface IUserService : IDisposable
    {
        public IUnitOfWork UnitOfWork { get; }
        IEnumerable<UserDTO> GetAllUsers();
        Task<UserDTO> GetUser(string id);
        void EditUser(UserDTO userE);
        void DeleteUser(UserDTO user);
        void AddToProject(string projectName, UserDTO user);
        Task<UserDTO> Register(UserDTO user, string password);
        Task<UserDTO> Authenticate(UserDTO user, string password);
        Task<IEnumerable<UserDTO>> GetUsersByRole(string roleName);
    }
}
