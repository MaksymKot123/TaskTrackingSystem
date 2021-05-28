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
using TaskTrackingSystem.BLL.Exceptions;

namespace TaskTrackingSystem.BLL.Services
{
    public class UserService : IUserService
    {
        private bool disposedValue;
        /// <summary>
        /// <see cref="DAL.Interfaces.IUnitOfWork"/>
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        /// <summary>
        /// <see cref="BLL.Interfaces.IJwtGenerator"/>
        /// </summary>
        private readonly IJwtGenerator _jwtGenerator;

        /// <summary>
        /// <see cref="DAL.Interfaces.IUnitOfWork"/>
        /// </summary>
        public IUnitOfWork UnitOfWork { get => _unitOfWork; }

        public UserService(IUnitOfWork unifOfWork, IMapper mapper, IJwtGenerator jwt)
        {
            _unitOfWork = unifOfWork;
            _mapper = mapper;
            _jwtGenerator = jwt;
        }

        /// <summary>
        /// This method deletes user from database. If user is not found, a user
        /// exception will be thrown
        /// </summary>
        /// <param name="user"></param>
        public async Task DeleteUser(UserDTO user)
        {
            var userFromDatabase = _unitOfWork.GetUserWithDetails(user.Email);

            if (userFromDatabase == null)
                throw new UserException("User not found");

            this.DeleteEmployeesProject(user.Email);

            await _unitOfWork.UserManager.DeleteAsync(userFromDatabase);
            _unitOfWork.SaveChanges();
        }

        /// <summary>
        /// Get users with specified role
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns>A list of <see cref="BLL.DTO.UserDTO"/></returns>
        public async Task<IEnumerable<UserDTO>> GetUsersByRole(string roleName)
        {
            var users = await _unitOfWork.UserManager.GetUsersInRoleAsync(roleName);
            return _mapper.Map<IEnumerable<UserDTO>>(users);
        }

        /// <summary>
        /// This method authenticates user. If email and password are correct,
        /// DTO user model will be return with JWT token. Otherwise,
        /// if password is incorrect or there is not any account with email,
        /// a user exception will be thrown
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns><see cref="BLL.DTO.UserDTO"/></returns>
        public async Task<UserDTO> Authenticate(UserDTO user, string password)
        {
            var usr = await _unitOfWork.UserManager.FindByEmailAsync(user.Email);

            if (usr != null)
            {
                var res = await _unitOfWork.SignInManager
                    .CheckPasswordSignInAsync(usr, password, false);

                if (res.Succeeded)
                {
                    var role = await _unitOfWork.UserManager.GetRolesAsync(usr);

                    return new UserDTO()
                    {
                        Email = usr.Email,
                        Id = usr.Id,
                        Name = usr.Name,
                        Projects = _mapper.Map<ICollection<ProjectDTO>>(usr.Projects),
                        Token = _jwtGenerator.CreateToken(usr, role.FirstOrDefault()),
                    };
                }
                else
                {
                    throw new UserException("Incorrect password");
                }
            }
            else
            {
                throw new UserException("Account with this email does not exists");
            }
        }

        /// <summary>
        /// Add new user to database. If email from parameter is already taken
        /// by other user, or password pattern is invalid
        /// a user exception will be throwns
        /// </summary>
        /// <param name="newUser"></param>
        /// <param name="password"></param>
        /// <returns><see cref="BLL.DTO.UserDTO"/></returns>
        public async Task<UserDTO> Register(UserDTO newUser, string password)
        {
            if (_unitOfWork.UserManager.Users.Any(x => x.Email.Equals(newUser.Email)))
            {
                throw new UserException("This email is already taken");
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

                    var identityResult = await _unitOfWork.UserManager
                        .AddToRoleAsync(userFromDatabase, "Employee");
                    if (identityResult.Succeeded)
                    {
                        var roles = await _unitOfWork.UserManager
                            .GetRolesAsync(userFromDatabase);

                        if (roles.Count == 0)
                            throw new UserException("Error during registration");

                        var role = await _unitOfWork.RoleManager
                            .FindByNameAsync(roles.First());

                        userFromDatabase.Role = role;

                        await _unitOfWork.UserManager.UpdateAsync(userFromDatabase);
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
                    else throw new UserException("Error");
                }
                else
                {
                    throw new UserException("Incorrect format of password");
                }
            }
        }

        /// <summary>
        /// This method adds user to project. If project is not found, a project
        /// exception will be thrown. If user if not found, a user exception
        /// will be thrown. If project already has this user, a project exception
        /// will be thrown
        /// </summary>
        /// <param name="projectName"></param>
        /// <param name="user"></param>
        public void AddToProject(string projectName, UserDTO user)
        {
            var proj = _unitOfWork.ProjectRepo.Get(projectName);
            var userFromDatabase = _unitOfWork.GetUserWithDetails(user.Email);

            if (proj == null)
                throw new ProjectException("Project not found");
            if (userFromDatabase == null)
                throw new UserException("User not found");

            if (proj.Employees.Contains(userFromDatabase))
                throw new ProjectException("User has already this project");
          
            proj.Employees.Add(userFromDatabase);
            userFromDatabase.Projects.Add(proj);
            _unitOfWork.SaveChanges();
        }

        /// <summary>
        /// This method edits user's info. User can be got by email.
        /// If there is not any user with this email, a user exception will
        /// be thrown
        /// </summary>
        /// <param name="user"></param>
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
            else
            {
                throw new UserException("User not found");
            }
        }

        /// <summary>
        /// This method return all DTO user models
        /// </summary>
        /// <returns>A list of <see cref="BLL.DTO.UserDTO"/></returns>
        public IEnumerable<UserDTO> GetAllUsers()
        {
            var users = _unitOfWork.UserManager.Users.AsEnumerable();
            return _mapper.Map<IEnumerable<User>, IEnumerable<UserDTO>>(users);
        }

        /// <summary>
        /// This method returns a DTO model of user. User can be got by email
        /// </summary>
        /// <param name="email"></param>
        /// <returns><see cref="BLL.DTO.UserDTO"/></returns>
        public async Task<UserDTO> GetUser(string email)
        {
            if (email == null)
                return null;
            else
            {
                var user = await _unitOfWork.UserManager.FindByEmailAsync(email);
                if (user == null)
                {
                    throw new UserException("User not found");
                }
                else
                {
                    return _mapper.Map<UserDTO>(user);
                }
            }
        }

        public async void ChangeUsersRole(string roleName, string userEmail)
        {
            var user = await _unitOfWork.UserManager.FindByEmailAsync(userEmail);//.GetAwaiter().GetResult();//_unitOfWork.GetUserWithDetails(userEmail);

            if (user == null)
                throw new UserException("User not found");

            var roles = await _unitOfWork.UserManager.GetRolesAsync(user);//.GetAwaiter()
                //.GetResult().AsEnumerable();
            if (roles.Count() == 1 && roles.First().Equals(roleName))
                throw new UserException($"This user is already {roleName.ToLower()}");

            await _unitOfWork.UserManager.RemoveFromRolesAsync(user, roles);//.GetAwaiter().GetResult();
            await _unitOfWork.UserManager.AddToRoleAsync(user, roleName);//.GetAwaiter().GetResult();
            await _unitOfWork.UserManager.UpdateAsync(user);//.GetAwaiter().GetResult();

            if (roles.Contains("Employee"))
                this.DeleteEmployeesProject(userEmail);
        }

        /// <summary>
        /// This method removes employee's projects
        /// </summary>
        /// <param name="email"></param>
        private void DeleteEmployeesProject(string email)
        {
            var user = _unitOfWork.GetUserWithDetails(email);
            var projects = user.Projects;
            
            while(projects.Count > 0)
            {
                var proj = projects.First();
                proj.Employees.Remove(user);
                _unitOfWork.ProjectRepo.Edit(proj);
                _unitOfWork.SaveChanges();
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
