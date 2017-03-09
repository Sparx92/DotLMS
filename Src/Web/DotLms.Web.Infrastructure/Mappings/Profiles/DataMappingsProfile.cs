using AutoMapper;
using DotLms.Data.Models;
using DotLms.Web.Models;

namespace DotLms.Web.Infrastructure.Mappings.Profiles
{
    public class DataMappingsProfile : Profile
    {
        protected override void Configure()
        {
            this.CreateMap<Page, PageViewModel>();
            this.CreateMap<PageViewModel, Page>();
        }
    }
}