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
                .ForMember(dest => dest.Id, opt => opt.MapFrom(z => z.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(z => z.Name));

            this.CreateMap<CourseCategoryViewModel, CourseCategory>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(z => z.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(z => z.Name));
        }
    }
}