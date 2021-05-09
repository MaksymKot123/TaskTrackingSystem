using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaskTrackingSystem.BLL.DTO;

namespace TaskTrackingSystem.BLL
{
    public interface IUserService : IDisposable
    {
        IEnumerable<UserDTO> GetAllUsers();
        UserDTO GetUser(int? id);
        Task<UserDTO> GetUser(string login);
        void AddUser(UserDTO user, string password);
        void EditUser(UserDTO userE);
        void DeleteUser(UserDTO user);
        void AddToProject(UserDTO user, ProjectDTO project);
    }
}
