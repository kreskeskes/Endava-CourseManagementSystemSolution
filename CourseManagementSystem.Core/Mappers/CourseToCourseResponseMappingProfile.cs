using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CourseManagementSystem.API.DTOs.Course;
using CourseManagementSystem.Core.Entities;

namespace CourseManagementSystem.Core.Mappers
{
    public class CourseToCourseResponseMappingProfile : Profile
    {
        public CourseToCourseResponseMappingProfile()
        {
            CreateMap<Course, CourseResponse>()
              .ForMember(dest => dest.Id,
              opt => opt.MapFrom(src=>src.Id))

              .ForMember(dest => dest.CreatedAt,
              opt => opt.MapFrom(src=>src.CreatedAt))

              .ForMember(dest => dest.UpdatedAt,
              opt => opt.MapFrom(src=>src.UpdatedAt))
              
               .ForMember(dest => dest.ModuleIds,
              opt => opt.MapFrom(src => src.ModuleIds))

                .ForMember(dest => dest.CreatedBy,
              opt => opt.MapFrom(src => src.CreatedBy))

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
