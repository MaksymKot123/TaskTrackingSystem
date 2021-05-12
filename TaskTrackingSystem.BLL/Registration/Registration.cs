﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskTrackingSystem.BLL.DTO;
using TaskTrackingSystem.DAL.Interfaces;
using TaskTrackingSystem.DAL.Models;
using AutoMapper;
using TaskTrackingSystem.BLL.Interfaces;
using System.Threading;

namespace TaskTrackingSystem.BLL.Registation
{
    public class Registration
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly DatabaseContext _database;
        private readonly IJwtGenerator _jwtGenerator;
        private readonly IMapper _mapper;

        public Registration(IUnitOfWork unitOfWork, DatabaseContext database, 
            IMapper mapper, IJwtGenerator jwtGenerator) 
        {
            _unitOfWork = unitOfWork;
            _database = database;
            _mapper = mapper;
            _jwtGenerator = jwtGenerator;
        }

        public async Task<UserDTO> Register(UserDTO newUser, string password)
        {
            if (_unitOfWork.UserManager.Users.Any(x => x.Email.Equals(newUser.Email)))
            {
                throw new Exception();
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
                    await _unitOfWork.UserManager.AddToRoleAsync(userFromDatabase, "Employee");
                    _unitOfWork.SaveChanges();
                    return new UserDTO()
                    {
                        Email = userFromDatabase.Email,
                        Id = userFromDatabase.Id,
                        Name = userFromDatabase.Name,
                        Projects = _mapper.Map<ICollection<ProjectDTO>>(
                            userFromDatabase.Projects),
                        Token = _jwtGenerator.CreateToken(usr),
                    };
                }
                else
                {
                    throw new Exception();
                }
            }
        }
    }
}
