using System;
using System.Collections.Generic;
using System.Text;
using TaskTrackingSystem.BLL.DTO;
using TaskTrackingSystem.BLL.Interfaces;
using AutoMapper;
using TaskTrackingSystem.DAL.Interfaces;
using TaskTrackingSystem.DAL.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using TaskTrackingSystem.DAL.Models;
using System.Linq;
using System.Threading.Tasks;

namespace TaskTrackingSystem.BLL.Services
{
    public class UserService : IUserService
    {
        private bool disposedValue;
        private readonly IUnitOfWork _unifOfWork;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unifOfWork, IMapper mapper)
        {
            _unifOfWork = unifOfWork;
            _mapper = mapper;

        }

        public async void AddToProject(string projectName, UserDTO user)
        {
            var userFromDatabase = await _unifOfWork.UserManager.FindByEmailAsync(user.Email);

            if (userFromDatabase != null)
            {
                var proj = _unifOfWork.ProjectRepo.Get(projectName);
                if (proj != null)
                    userFromDatabase.Projects.Add(proj);
                _unifOfWork.SaveChanges();
            }
        }

        public async void AddUser(UserDTO user, string password)
        {
            var email = user.Email;
            var name = user.Name;

            var usr = await _unifOfWork.UserManager.FindByEmailAsync(email);

            if (usr == null)
            {
                usr = new User()
                {
                    Email = email,
                    Name = name,
                    UserName = email,
                };

                var res = await _unifOfWork.UserManager.CreateAsync(usr, password);

                if (res.Succeeded)
                {
                    var createdUser = await _unifOfWork.UserManager.FindByNameAsync(email);
                    await _unifOfWork.UserManager.AddToRoleAsync(createdUser, "Employee");
                    _unifOfWork.SaveChanges();
                }
            }
        }

        public async void DeleteUser(UserDTO user)
        {
            var usr = await _unifOfWork.UserManager.FindByEmailAsync(user.Email);

            if (usr != null)
            {
                await _unifOfWork.UserManager.DeleteAsync(usr);
                _unifOfWork.SaveChanges();
            }
        }

        public async void EditUser(UserDTO user)
        {
            var usr = await _unifOfWork.UserManager.FindByEmailAsync(user.Email);

            if (usr != null)
            {
                _unifOfWork.UserRepo.Edit(usr);
                _unifOfWork.SaveChanges();
            }
        }

        public IEnumerable<UserDTO> GetAllUsers()
        {
            return _mapper.Map<IEnumerable<User>, IEnumerable<UserDTO>>(
                _unifOfWork.UserRepo.GetAll());
        }

        public UserDTO GetUser(string email)
        {
            if (email == null)
                return null;
            else
            {
                var user = _unifOfWork.UserRepo.Get(email);
                if (user == null)
                    return null;
                else
                {
                    return _mapper.Map<UserDTO>(user);
                }
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _unifOfWork.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
