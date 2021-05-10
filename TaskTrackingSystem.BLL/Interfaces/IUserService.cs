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
        UserDTO GetUser(string id);
        //Task<UserDTO> GetUser(string login);
        void AddUser(UserDTO user, string password);
        void EditUser(UserDTO userE);
        void DeleteUser(UserDTO user);
        void AddToProject(string projectName, UserDTO user);
    }
}
