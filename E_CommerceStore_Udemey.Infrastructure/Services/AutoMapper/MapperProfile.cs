using AutoMapper;
using E_CommerceStore_Udemey.Core.Dtos;
using E_CommerceStore_Udemey.Core.ViewModels;
using E_CommerceStore_Udemey.DATA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourEstate.Infrastructure.AutoMapper
{

    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
          
            CreateMap<Category, CategoryViewModel>();
            CreateMap<CreateCategoryDto, Category>();
            CreateMap<UpdateCategoryDto, Category>();
            CreateMap<Category, UpdateCategoryDto>();
            //CreateMap<CategoryViewModel, paginationViewModel>();

            CreateMap<CoverType, CoverTypeViewModel>();
            CreateMap<CreateCoverTypeDto, CoverType>();
            CreateMap<UpdateCoverTypeDto, CoverType>();
            CreateMap<CoverType, UpdateCoverTypeDto>();
        }
    }
}
