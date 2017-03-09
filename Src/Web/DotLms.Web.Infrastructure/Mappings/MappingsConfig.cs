using AutoMapper;
using DotLms.Services.Providers.Contracts;
using DotLms.Web.Infrastructure.Mappings.Profiles;

namespace DotLms.Web.Infrastructure.Mappings
{
    public class MappingsConfig
    {
        private readonly IMapperProvider mapperProvider;

        public MappingsConfig(IMapperProvider mapperProvider)
        {
            this.mapperProvider = mapperProvider;
        }

        public static MapperConfiguration Configuration { get; private set; }

        public static IMapper Map()
        {
            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DataMappingsProfile>();
            });

            IMapper mapper = config.CreateMapper();

            Configuration = config;
            return mapper;
        }
    }
}