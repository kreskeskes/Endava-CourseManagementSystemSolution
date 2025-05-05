using AutoMapper;
using CourseManagementSystem.API.DTOs.Course;
using CourseManagementSystem.Core.Entities;

namespace CourseManagementSystem.Core.Mappers
{
    public class CourseUpdateRequestToCourseMappingProfile : Profile
    {
        public CourseUpdateRequestToCourseMappingProfile()
        {
            CreateMap<CourseUpdateRequest, Course>()


              .ForMember(dest => dest.CreatedBy,
              opt => opt.Ignore())

              .ForMember(dest => dest.Id,
              opt => opt.MapFrom(src => src.Id))

               .ForMember(dest => dest.ModuleIds,
              opt => opt.MapFrom(src => src.ModuleIds))

              .ForMember(dest => dest.Contributors,
              opt => opt.MapFrom(src => src.Contributors))

              .ForMember(dest => dest.Difficulty,
              opt => opt.MapFrom(src => src.Difficulty))

              .ForMember(dest => dest.Enrollments,
              opt => opt.MapFrom(src => src.Enrollments))

              .ForMember(dest => dest.Title,
              opt => opt.MapFrom(src => src.Title))

              .ForMember(dest => dest.Description,
              opt => opt.MapFrom(src => src.Description))

              .ForMember(dest => dest.Discipline,
              opt => opt.MapFrom(src => src.Discipline));
        }
    }
}
