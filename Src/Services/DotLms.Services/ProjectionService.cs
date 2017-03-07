using System.Collections.Generic;
using System.Linq;

using Bytes2you.Validation;

using DotLms.Services.Contracts;
using DotLms.Services.Providers.Contracts;

using AutoMapper;

namespace DotLms.Services
{
    public class ProjectionService : IProjectionService
    {
        private readonly IMapperProvider mapperProvider;

        public ProjectionService(IMapperProvider mapperProvider)
        {
            Guard.WhenArgument(mapperProvider, nameof(mapperProvider)).IsNull().Throw();

            this.mapperProvider = mapperProvider;
        }

        public TDestination ProjectToFirstOrDefault<TSource, TDestination>(IQueryable<TSource> query)
        {
            TDestination projectedItem = query.ProjectToFirstOrDefault<TDestination>(this.mapperProvider.Configuration);
            return projectedItem;
        }

        public List<TDestination> ProjectToList<TSource, TDestination>(IQueryable<TSource> query)
        {
            List<TDestination> projectedCollection = query.ProjectToList<TDestination>(this.mapperProvider.Configuration);
            return projectedCollection;
        }
    }
}
