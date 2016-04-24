using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UMS.Web
{
    public static class AutoMapperConfig
    {
        public static void InitializeAutoMapper()
        {
            Mapper.Reset();
            Mapper.AssertConfigurationIsValid();
        }
    }
}