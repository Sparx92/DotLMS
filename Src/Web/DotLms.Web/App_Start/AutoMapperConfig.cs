using AutoMapper;
using DotLms.Services.Providers.Contracts;
using DotLms.Web.Infrastructure;
using DotLms.Web.Infrastructure.Mappings;
using Ninject;

namespace DotLms.Web
{
    public class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            IMapperProvider mapperProvider = NinjectWebCommon.Kernel.Get<IMapperProvider>();
            IMapper mapper = MappingsConfig.Map();

            mapperProvider.Instance = mapper;
            mapperProvider.Configuration = MappingsConfig.Configuration;
        }
    }
}