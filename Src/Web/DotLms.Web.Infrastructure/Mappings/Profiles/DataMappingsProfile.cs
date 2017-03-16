using System;
using AutoMapper;
using DotLms.Data.Models;
using DotLms.Web.Models;

namespace DotLms.Web.Infrastructure.Mappings.Profiles
{
    public class DataMappingsProfile : Profile
    {
        public DataMappingsProfile()
        {
            this.CreateMap<Page, PageViewModel>();
            this.CreateMap<PageViewModel, Page>();

            this.CreateMap<CourseCategory, CourseCategoryViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

            this.CreateMap<CourseCategoryViewModel, CourseCategory>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

            this.CreateMap<CourseCreationViewModel, Course>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
                .ForMember(dest => dest.ShortDescription, opt => opt.MapFrom(src => src.ShortDescription))
                .ForMember(dest => dest.FullDescription, opt => opt.MapFrom(src => src.FullDescription))
               .ForMember(dest => dest.ChildPages, opt => opt.MapFrom(src => src.ChildPages));

            this.CreateMap<CourseCreationViewModel, CourseCategory>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Category.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Category.Name));


            this.CreateMap<Course, CourseViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.UglyName, opt => opt.MapFrom(src => src.UglyName))
                .ForMember(dest => dest.ShortDescription, opt => opt.MapFrom(src => src.ShortDescription))
                .ForMember(dest => dest.FullDescription, opt => opt.MapFrom(src => src.FullDescription))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
                .ForMember(dest => dest.MainImage, opt => opt.MapFrom(src => src.MainImage))
                .ForMember(dest => dest.ChildPages, opt => opt.MapFrom(src => src.ChildPages))
                ;
        }
    }
}