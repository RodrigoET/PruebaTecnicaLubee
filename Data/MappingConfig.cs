using AutoMapper;
using Data.DTOs.GradeDTO;
using Data.DTOs.ItemDTO;
using Data.DTOs.SchoolDTO;
using DB;
using Model;
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
            #region SCHOOL
            CreateMap<School, GetSchoolDTO>().ReverseMap();
            CreateMap<School, CreateSchoolDTO>().ReverseMap();
            CreateMap<School, UpdateSchoolDTO>().ReverseMap();
            #endregion

            #region GRADE
            CreateMap<Grade, GetGradeDTO>()
            .ForMember(dest => dest.Grade, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.School, opt => opt.MapFrom(src => src.School.Name));
            CreateMap<Grade, CreateGradeDTO>().ReverseMap();
            CreateMap<Grade, UpdateGradeDTO>().ReverseMap();
            #endregion

            #region ITEM
            CreateMap<Item, GetItemDTO>().ReverseMap();
            CreateMap<Item, CreateItemDTO>().ReverseMap();
            CreateMap<Item, UpdateItemDTO>().ReverseMap();
            #endregion

        }
    }
}
