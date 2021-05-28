using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using TaskTrackingSystem.BLL.DTO;
using TaskTrackingSystem.BLL.Interfaces;
using TaskTrackingSystem.DAL.Models;

namespace TaskTrackingSystem.BLL.Automapper
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Project, ProjectDTO>()
                .ForMember(dest => dest.ClientEmail, src => src.MapFrom(x => x.ClientEmail))
                .ForMember(dest => dest.Description, src => src.MapFrom(x => x.Description))
                .ForMember(dest => dest.EndTime, src => src.MapFrom(x => x.EndTime))
                .ForMember(dest => dest.Id, src => src.MapFrom(x => x.Id))
                .ForMember(dest => dest.Name, src => src.MapFrom(x => x.Name))
                .ForMember(dest => dest.PercentCompletion, src => src.MapFrom(x => x.PercentCompletion))
                .ForMember(dest => dest.StartTime, src => src.MapFrom(x => x.StartTime))
                .ForMember(dest => dest.Status, src => src.MapFrom(x => x.Status))
                .ForMember(dest => dest.Tasks, src => src.MapFrom(x => x.Tasks))
                .ForMember(dest => dest.Employees, src => src.MapFrom(x => x.Employees))
                .ReverseMap();

            CreateMap<User, UserDTO>()
                .ForMember(dest => dest.Email, src => src.MapFrom(x => x.Email))
                .ForMember(dest => dest.Id, src => src.MapFrom(x => x.Id))
                .ForMember(dest => dest.Name, src => src.MapFrom(x => x.Name))
                .ForMember(dest => dest.Role, src => src.MapFrom(x => x.Role))
                .ForMember(dest => dest.Projects, src => src.MapFrom(x => x.Projects))
                .ReverseMap();

            CreateMap<TaskProject, TaskDTO>()
                .ForMember(dest => dest.Description, src => src.MapFrom(x => x.Description))
                .ForMember(dest => dest.EndTime, src => src.MapFrom(x => x.EndTime))
                .ForMember(dest => dest.Id, src => src.MapFrom(x => x.Id))
                .ForMember(dest => dest.StartTime, src => src.MapFrom(x => x.StartTime))
                .ForMember(dest => dest.TaskName, src => src.MapFrom(x => x.TaskName))
                .ForMember(dest => dest.Status, src => src.MapFrom(x => x.Status))
                .ForMember(dest => dest.Project, src => src.MapFrom(x => x.Project))
                .ReverseMap();

            CreateMap<DAL.Enums.Status, BLL.Enums.StatusDTO>()
                .ReverseMap();
        }        
    }
}
