﻿using AutoMapper;
using CourseManagementSystem.API.DTOs.Course;
using CourseManagementSystem.Core.Entities;

namespace CourseManagementSystem.Core.Mappers
{
    public class CourseAddRequestToCourseMappingProfile : Profile
    {
        public CourseAddRequestToCourseMappingProfile()
        {
            CreateMap<CourseAddRequest, Course>()
               .ForMember(dest => dest.Id,
               opt => opt.Ignore())

               .ForMember(dest => dest.Modules,
               opt => opt.Ignore())

               .ForMember(dest => dest.CreatedAt,
               opt => opt.Ignore())

               .ForMember(dest => dest.UpdatedAt,
               opt => opt.Ignore())

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
