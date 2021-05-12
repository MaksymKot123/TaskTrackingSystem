using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskTrackingSystem.BLL.DTO;
using TaskTrackingSystem.DAL.Interfaces;
using TaskTrackingSystem.DAL.Models;
using AutoMapper;
using TaskTrackingSystem.BLL.Interfaces;

namespace TaskTrackingSystem.BLL.Login
{
    public class Login
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly DatabaseContext _database;
        private readonly IMapper _mapper;
        private readonly IJwtGenerator _jwtGenerator;

        public Login(IUnitOfWork unitOfWork, DatabaseContext database, 
            IMapper mapper, IJwtGenerator jwtGenerator)
        {
            _unitOfWork = unitOfWork;
            _database = database;
            _mapper = mapper;
            _jwtGenerator = jwtGenerator;
        }

        public async Task<UserDTO> LogInAccount(UserDTO user, string password)
        {
            var usr = await _unitOfWork.UserManager.FindByEmailAsync(user.Email);

            if (usr != null)
            {
                var res = await _unitOfWork.SignInManager
                    .CheckPasswordSignInAsync(usr, password, false);

                if (res.Succeeded)
                {
                    return new UserDTO()
                    {
                        Email = usr.Email,
                        Id = usr.Id,
                        Name = usr.Name,
                        Projects = _mapper.Map<ICollection<ProjectDTO>>(usr.Projects),
                        Token = _jwtGenerator.CreateToken(usr),
                    };
                }
                else
                {
                    throw new Exception();
                }
            }
            else
            {
                throw new Exception();
            }
        }
    }
}
