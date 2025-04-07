using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CourseManagementSystem.API.DTOs;
using CourseManagementSystem.Core.Entities;

namespace CourseManagementSystem.Core.Mappers
{
    public class ModuleUpdateRequestToModuleMappingProfile : Profile
    {

        public ModuleUpdateRequestToModuleMappingProfile()
        {
            CreateMap<ModuleUpdateRequest, Module>()
                .ForMember(dest => dest.Id,
                opt => opt.MapFrom(src => src.Id))

                .ForMember(dest => dest.UpdatedAt,
                opt => opt.Ignore())

                .ForMember(dest => dest.CreatedAt,
                opt => opt.Ignore())

                .ForMember(dest => dest.CreatedBy,
                opt => opt.MapFrom(src => src.CreatedBy))

                .ForMember(dest => dest.Description,
                opt => opt.MapFrom(src => src.Description))

                .ForMember(dest => dest.Order,
                opt => opt.MapFrom(src => src.Order))

                .ForMember(dest => dest.Content,
                opt => opt.MapFrom(src => src.Content))

                .ForMember(dest => dest.Title,
                opt => opt.MapFrom(src => src.Title))

                .ForMember(dest => dest.CourseId,
                opt => opt.MapFrom(src => src.CourseId));

        }
    }
}
