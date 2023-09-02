﻿using AutoMapper;
using Data.DTOs.ContractDTO;
using Data.DTOs.GradeDTO;
using Data.DTOs.ItemDTO;
using Data.DTOs.ItemXContractDTO;
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
            #region CONTRACT
            CreateMap<Contract, CreateContractDTO>().ReverseMap();
            CreateMap<Contract, GetContractDTO>()
                .ForMember(dest => dest.School, opt => opt.MapFrom(src => src.Grade.School.Name))
                .ForMember(dest => dest.SchoolLevel, opt => opt.MapFrom(src => src.Grade.School.Level))
                .ForMember(dest => dest.SchoolLocation, opt => opt.MapFrom(src => src.Grade.School.Location))
                .ForMember(dest => dest.SchoolGrade, opt => opt.MapFrom(src => src.Grade.Name));
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

            #region ITEMXCONTRACT
            CreateMap<ItemXContract, CreateItemXContractDTO>().ReverseMap();
            CreateMap<ItemXContract, GetItemXContractDTO>()
                .ForMember(dest => dest.Item, opt => opt.MapFrom(src => src.Item.Name))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Item.Price));
            #endregion

            #region SCHOOL
            CreateMap<School, GetSchoolDTO>().ReverseMap();
            CreateMap<School, CreateSchoolDTO>().ReverseMap();
            CreateMap<School, UpdateSchoolDTO>().ReverseMap();
            #endregion



        }
    }
}
