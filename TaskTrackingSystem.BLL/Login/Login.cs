using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskTrackingSystem.BLL.DTO;
using TaskTrackingSystem.DAL.Interfaces;
using TaskTrackingSystem.DAL.Models;
using AutoMapper;


namespace TaskTrackingSystem.BLL.Login
{
    public class Login
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly DatabaseContext _database;
        private readonly IMapper _mapper;

        public Login(IUnitOfWork unitOfWork, DatabaseContext database, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _database = database;
            _mapper = mapper;
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
