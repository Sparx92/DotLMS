using System;
using System.Collections.Generic;
using System.Web;
using AutoMapper;
using DotLms.Data.Models;
using DotLms.Web.Models;

namespace DotLms.Web.Infrastructure.Mappings.Profiles
{
    public class DataMappingsProfile : Profile
    {
        public DataMappingsProfile()
        {
            this.CreateMap<CourseCategory, CourseCategoryViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

            this.CreateMap<CourseCategoryViewModel, CourseCategory>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

            this.CreateMap<CourseCreationViewModel, Course>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
                .ForMember(dest => dest.ShortDescription, opt => opt.MapFrom(src => src.ShortDescription))
                .ForMember(dest => dest.FullDescription, opt => opt.MapFrom(src => src.FullDescription))
                .ForMember(dest => dest.ChildPages, opt => opt.MapFrom(src => src.ChildPages));

            this.CreateMap<CourseCreationViewModel, CourseCategory>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Category.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Category.Name));

            this.CreateMap<Page, PageViewModel>().MaxDepth(2);

            this.CreateMap<PageViewModel, Page>().MaxDepth(2);

            this.CreateMap<Course, CourseViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.UglyName, opt => opt.MapFrom(src => src.UglyName))
                .ForMember(dest => dest.ShortDescription, opt => opt.MapFrom(src => src.ShortDescription))
                .ForMember(dest => dest.FullDescription, opt => opt.MapFrom(src => src.FullDescription))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
                .ForMember(dest => dest.MainImage, opt => opt.MapFrom(src => src.MainImage))
                .ForMember(dest => dest.ChildPages, opt => opt.MapFrom(src => src.ChildPages));

            this.CreateMap<CourseViewModel, Course>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
               .ForMember(dest => dest.UglyName, opt => opt.MapFrom(src => src.UglyName))
               .ForMember(dest => dest.ShortDescription, opt => opt.MapFrom(src => src.ShortDescription))
               .ForMember(dest => dest.FullDescription, opt => opt.MapFrom(src => src.FullDescription))
               .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
               .ForMember(dest => dest.MainImage, opt => opt.MapFrom(src => src.MainImage))
               .ForMember(dest => dest.ChildPages, opt => opt.MapFrom(src => src.ChildPages));

            this.CreateMap<Course, CourseCreationViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.UglyName, opt => opt.MapFrom(src => src.UglyName))
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
                .ForMember(dest => dest.ShortDescription, opt => opt.MapFrom(src => src.ShortDescription))
                .ForMember(dest => dest.FullDescription, opt => opt.MapFrom(src => src.FullDescription))
                .ForMember(dest => dest.ChildPages, opt => opt.MapFrom(src => src.ChildPages))
                .ForMember(dest => dest.File, opt => opt.ResolveUsing(src => src.MainImage));

            this.CreateMap<MediaItem, HttpPostedFileBase>()
                .ForMember(dest => dest.FileName, opt => opt.MapFrom(src => src.FullName));
        }
    }
}