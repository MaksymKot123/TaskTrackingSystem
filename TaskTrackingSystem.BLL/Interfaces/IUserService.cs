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
        void AddUser(UserDTO user, string password);
        void EditUser(UserDTO userE);
        void DeleteUser(UserDTO user);
        void AddToProject(string projectName, UserDTO user);
        Task<UserDTO> Register(UserDTO user, string password);
        Task<UserDTO> Login(UserDTO user, string password);
    }
}
