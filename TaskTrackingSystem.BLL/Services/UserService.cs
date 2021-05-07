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
        private IUnitOfWork unifOfWork;

        public UserService(IUnitOfWork uow)
        {
            unifOfWork = uow;
        }

        public async void AddToProject(UserDTO user, ProjectDTO project)
        {
            var userFromDatabase = await unifOfWork.UserManager.FindByEmailAsync(user.Email);

            if (userFromDatabase != null)
            {
                var mapper = new MapperConfiguration(cfg => cfg
                .CreateMap<ProjectDTO, Project>()).CreateMapper();

                var proj = mapper.Map<Project>(project);

                userFromDatabase.Projects.Add(proj);
            }
        }

        public async void AddUser(UserDTO user, string password)
        {
            var email = user.Email;
            var name = user.Name;

            var usr = await unifOfWork.UserManager.FindByEmailAsync(email);

            if (usr == null)
            {
                usr = new User()
                {
                    Email = email,
                    Name = name,
                    UserName = email,
                };

                var res = await unifOfWork.UserManager.CreateAsync(usr, password);

                if (res.Succeeded)
                {
                    var createdUser = await unifOfWork.UserManager.FindByNameAsync(email);
                    await unifOfWork.UserManager.AddToRoleAsync(createdUser, "Employee");
                }
            }
        }

        public async void DeleteUser(UserDTO user)
        {
            var usr = await unifOfWork.UserManager.FindByEmailAsync(user.Email);

            if (usr != null)
            {
                var res = await unifOfWork.UserManager.DeleteAsync(usr);


            }
        }

        public async void EditUser(UserDTO user)
        {
            var usr = await unifOfWork.UserManager.FindByEmailAsync(user.Email);

            if (usr != null)
                unifOfWork.UserRepo.Edit(usr);
        }

        public IEnumerable<UserDTO> Find(Func<UserDTO, bool> func)
        {
            var configGet = new MapperConfiguration(cfg => cfg
                .CreateMap<Func<UserDTO, bool>, Func<User, bool>>());

            var mapperGet = new Mapper(configGet);

            var fnc = mapperGet.Map<Func<User, bool>>(func);

            var users = unifOfWork.UserRepo.Find(fnc);

            var configReturn = new MapperConfiguration(cfg => cfg
                .CreateMap<IEnumerable<User>, IEnumerable<UserDTO>>());
            var mapperReturn = new Mapper(configReturn);

            return mapperReturn.Map <IEnumerable<UserDTO>>(users);
        }

        public IEnumerable<UserDTO> GetAllUsers()
        {
            var config = new MapperConfiguration(cfg => cfg
                .CreateMap<User, UserDTO>()
                .ForMember("Id", x => x.MapFrom(u => u.Id))
                .ForMember("Name", x => x.MapFrom(u => u.Name))
                .ForMember("Email", x => x.MapFrom(u => u.Email))
                .ForMember("Role", x => x.MapFrom(u => u.Role))
                .ForMember("Projects", x => x.MapFrom(u => u.Projects)));

            var mapper = new Mapper(config);

            return mapper.Map<IEnumerable<User>, IEnumerable<UserDTO>>(
                unifOfWork.UserRepo.GetAll());
        }

        public UserDTO GetUser(int? id)
        {
            if (id == null)
                return null;
            else
            {
                var user = unifOfWork.UserRepo.Get(id);
                if (user == null)
                    return null;
                else
                {
                    var config = new MapperConfiguration(cfg => cfg
                        .CreateMap<User, UserDTO>()
                        .ForMember("Projects", x => x.MapFrom(u => u.Projects)));

                    var mapper = new Mapper(config);

                    return mapper.Map<UserDTO>(user);

                    //return new User()
                    //{
                    //    Email = user.Email,
                    //    Id = user.Id,
                    //    Name = user.Name,
                    //    //AccessFailedCount
                    //    Projects = mapper.Map<ICollection<Project>, ICollection<ProjectDTO>>(
                    //        user.Projects),

                    //};
                }
                
            }
        }

        public async Task<UserDTO> GetUser(string login)
        {
            var user = await unifOfWork.UserManager.FindByEmailAsync(login);

            if (user == null)
                return null;
            else 
            {
                return await Task.Run(() => new UserDTO() 
                {
                    Email = user.Email,
                    Id = user.Id,
                    Name = user.Name,
                });
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    unifOfWork.Dispose();
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
