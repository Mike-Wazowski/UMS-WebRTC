using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UMS.Database.Models;
using UMS.Web.Helpers;
using UMS.Web.Hubs.HubModels;

namespace UMS.Web
{
    public static class AutoMapperConfig
    {
        public static void InitializeAutoMapper()
        {
            Mapper.Reset();
            Mapper.CreateMap<User, OnlineUser>()
                .IgnoreAllUnmapped()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName));
            Mapper.AssertConfigurationIsValid();
        }
    }
}