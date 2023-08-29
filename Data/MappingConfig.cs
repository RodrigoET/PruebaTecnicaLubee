using AutoMapper;
using Data.DTOs.SchoolDTO;
using DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<School, GetSchoolDTO>().ReverseMap();
            CreateMap<School, CreateSchoolDTO>().ReverseMap();
        }
    }
}
