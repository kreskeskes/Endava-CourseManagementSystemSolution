using AutoMapper;
using CourseManagementSystem.API.DTOs;
using CourseManagementSystem.Core.Entities;

namespace CourseManagementSystem.Core.Mappers
{
    public class ModuleToModuleResponseMappingProfile : Profile
    {
        public ModuleToModuleResponseMappingProfile()
        {
            CreateMap<Module, ModuleResponse>()
                .ForMember(dest => dest.Id,
                opt => opt.MapFrom(src => src.Id))

                .ForMember(dest => dest.UpdatedAt,
                opt => opt.MapFrom(src => src.UpdatedAt))

                .ForMember(dest => dest.CreatedAt,
                opt => opt.MapFrom(src => src.CreatedAt))

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
