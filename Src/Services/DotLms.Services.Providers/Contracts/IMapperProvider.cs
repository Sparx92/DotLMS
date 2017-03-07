using AutoMapper;

namespace DotLms.Services.Providers.Contracts
{
    public interface IMapperProvider
    {
        IMapper Instance { get; set; }

        IConfigurationProvider Configuration { get; set; }
    }
}