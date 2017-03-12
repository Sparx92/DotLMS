using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Bytes2you.Validation;
using DotLms.Data.Contracts;
using DotLms.Services.Common.Contracts;

namespace DotLms.Data.Repositories
{
    public class ProjectableRepository<T> : EntityFrameworkRepository<T>, IProjectableRepository<T> where T : class
    {
        private readonly IProjectionService projectionService;

        public ProjectableRepository(IDotLmsEfDbContext context, IProjectionService projectionService)
            : base(context)
        {
            Guard.WhenArgument(context, nameof(context)).IsNull().Throw();
            Guard.WhenArgument(projectionService, nameof(projectionService)).IsNull().Throw();

            this.projectionService = projectionService;
        }

        public TDestitanion GetFirstMapped<TDestitanion>(Expression<Func<T, bool>> filterExpression)
        {
            IQueryable<T> query = this.All.Where(filterExpression);
            TDestitanion foundEntity = this.projectionService.ProjectToFirstOrDefault<T, TDestitanion>(query);

            return foundEntity;
        }

        public IEnumerable<TDestination> GetAllMapped<TDestination>()
        {
            List<TDestination> mappedEntities = this.projectionService.ProjectToList<T, TDestination>(this.All);

            return mappedEntities;
        }

        public IEnumerable<TDestination> GetAllMapped<TDestination>(Expression<Func<T, bool>> filterExpression)
        {
            IQueryable<T> query = this.All.Where(filterExpression);
            List<TDestination> mappedEntities = this.projectionService.ProjectToList<T, TDestination>(query);

            return mappedEntities;
        }
    }
}