using AutoMapper;
using DotLms.Services.Providers.Contracts;

namespace DotLms.Services.Providers
{
    public class MapperProvider : IMapperProvider
    {
        public IConfigurationProvider Configuration { get; set; }

        public IMapper Instance { get; set; }
    }
}