﻿using System;
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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IJwtGenerator _jwtGenerator;

        public IUnitOfWork UnitOfWork { get => _unitOfWork; }

        public UserService(IUnitOfWork unifOfWork, IMapper mapper, IJwtGenerator jwt)
        {
            _unitOfWork = unifOfWork;
            _mapper = mapper;
            _jwtGenerator = jwt;
        }

        public async Task<IEnumerable<UserDTO>> GetUsersByRole(string roleName)
        {
            var users = await _unitOfWork.UserManager.GetUsersInRoleAsync(roleName);
            return _mapper.Map<IEnumerable<UserDTO>>(users);
        }

        public async Task<UserDTO> Authenticate(UserDTO user, string password)
        {
            var usr = await _unitOfWork.UserManager.FindByEmailAsync(user.Email);

            if (usr != null)
            {
                var res = await _unitOfWork.SignInManager
                    .CheckPasswordSignInAsync(usr, password, false);

                
                
                if (res.Succeeded)
                {
                    var role = _unitOfWork.UserManager.GetRolesAsync(usr)
                    .GetAwaiter().GetResult().FirstOrDefault();

                    return new UserDTO()
                    {
                        Email = usr.Email,
                        Id = usr.Id,
                        Name = usr.Name,
                        Projects = _mapper.Map<ICollection<ProjectDTO>>(usr.Projects),
                        Token = _jwtGenerator.CreateToken(usr, role),
                    };
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public async Task<UserDTO> Register(UserDTO newUser, string password)
        {
            if (_unitOfWork.UserManager.Users.Any(x => x.Email.Equals(newUser.Email)))
            {
                return null;
            }
            else
            {
                var usr = new User()
                {
                    Email = newUser.Email,
                    Name = newUser.Name,
                    UserName = newUser.Email,
                };

                var res = await _unitOfWork.UserManager.CreateAsync(usr, password);

                if (res.Succeeded)
                {
                    var userFromDatabase = await _unitOfWork.UserManager
                        .FindByEmailAsync(usr.Email);

                    var identityResult = await _unitOfWork.UserManager.
                        AddToRoleAsync(userFromDatabase, "Employee");
                    if (identityResult.Succeeded)
                    {
                        var roleName = _unitOfWork.UserManager
                            .GetRolesAsync(userFromDatabase).Result
                            .FirstOrDefault();

                        var role = await _unitOfWork.RoleManager
                            .FindByNameAsync(roleName);

                        userFromDatabase.Role = role;

                        _unitOfWork.SaveChanges();
                        return new UserDTO()
                        {
                            Email = userFromDatabase.Email,
                            Id = userFromDatabase.Id,
                            Name = userFromDatabase.Name,
                            Projects = _mapper.Map<ICollection<ProjectDTO>>(
                                userFromDatabase.Projects),
                            Role = role,
                        };
                    }
                    else return null;
                }
                else
                {
                    return null;
                }
            }
        }

        public async void AddToProject(string projectName, UserDTO user)
        {
            var userFromDatabase = _unitOfWork.GetUserWithDetails(user.Email);

            if (userFromDatabase != null)
            {
                var proj = _unitOfWork.ProjectRepo.Get(projectName);
                if (proj != null)
                {
                    
                    //if (userFromDatabase.Projects == null)
                    //{
                    //userFromDatabase.Projects = new List<Project>();
                    proj.Employees.Add(userFromDatabase);
                    userFromDatabase.Projects.Add(proj);
                        //userFromDatabase.Projects.Add(proj);
                    //}
                    //else
                       // userFromDatabase.Projects.Add(proj);
                    _unitOfWork.SaveChanges();
                }
                    
            }
        }

        public async void AddUser(UserDTO user, string password)
        {
            var email = user.Email;
            var name = user.Name;

            var usr = await _unitOfWork.UserManager.FindByEmailAsync(email);

            if (usr == null)
            {
                usr = new User()
                {
                    Email = email,
                    Name = name,
                    UserName = email,
                };

                var res = await _unitOfWork.UserManager.CreateAsync(usr, password);

                if (res.Succeeded)
                {
                    var createdUser = await _unitOfWork.UserManager.FindByNameAsync(email);
                    await _unitOfWork.UserManager.AddToRoleAsync(createdUser, "Employee");
                    _unitOfWork.SaveChanges();
                }
            }
        }

        public async void DeleteUser(UserDTO user)
        {
            var usr = await _unitOfWork.UserManager.FindByEmailAsync(user.Email);

            if (usr != null)
            {
                await _unitOfWork.UserManager.DeleteAsync(usr);
                _unitOfWork.SaveChanges();
            }
        }

        public async void EditUser(UserDTO user)
        {
            var usr = await _unitOfWork.UserManager.FindByEmailAsync(user.Email);

            if (usr != null)
            {
                usr.Email = user.Email;
                usr.Name = user.Name;
                usr.UserName = usr.Email;
                await _unitOfWork.UserManager.UpdateAsync(usr);
            }
        }

        public IEnumerable<UserDTO> GetAllUsers()
        {
            var users = _unitOfWork.UserManager.Users.AsEnumerable();
            return _mapper.Map<IEnumerable<User>, IEnumerable<UserDTO>>(users);
        }

        public async Task<UserDTO> GetUser(string email)
        {
            if (email == null)
                return null;
            else
            {
                var user = await _unitOfWork.UserManager.FindByEmailAsync(email);
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
                    _unitOfWork.Dispose();
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
