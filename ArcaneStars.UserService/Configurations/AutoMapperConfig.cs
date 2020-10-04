using ArcaneStars.UserService.Applications.Dtos;
using ArcaneStars.UserService.Domains.Aggregates;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArcaneStars.UserService.Configurations
{
    public class AutoMapperConfig:Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<UserDto, User>();
            CreateMap<User, UserDto>();
        }
    }
}
