using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskTrackingSystem.BLL.DTO;
using TaskTrackingSystem.DAL.Interfaces;
using TaskTrackingSystem.DAL.Models;
using AutoMapper;

namespace TaskTrackingSystem.BLL.Registation
{
    public class Registration
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly DatabaseContext _database;
        private readonly IMapper _mapper;

        public Registration(IUnitOfWork unitOfWork, DatabaseContext database, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _database = database;
            _mapper = mapper;
        }

        public async Task<UserDTO> Register(UserDTO newUser, string password)
        {
            if (_unitOfWork.UserManager.Users.Any(x => x.Email.Equals(newUser.Email)))
            {

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

                    return new UserDTO()
                    {
                        Email = userFromDatabase.Email,
                        Id = userFromDatabase.Id,
                        Name = userFromDatabase.Name,
                        Projects = _mapper.Map<ICollection<ProjectDTO>>(
                            userFromDatabase.Projects),
                        
                    };
                }
            }

            
        }
    }
}
