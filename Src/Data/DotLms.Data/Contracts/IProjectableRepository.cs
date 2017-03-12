using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DotLms.Data.Contracts
{
    public interface IProjectableRepository<T> : IEntityFrameworkRepository<T> where T : class
    {
        TDestitanion GetFirstMapped<TDestitanion>(Expression<Func<T, bool>> filterExpression);

        IEnumerable<TDestination> GetAllMapped<TDestination>();

        IEnumerable<TDestination> GetAllMapped<TDestination>(Expression<Func<T, bool>> filterExpression);
    }
}