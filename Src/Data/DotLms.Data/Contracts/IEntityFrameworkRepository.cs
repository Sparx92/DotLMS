using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DotLms.Data.Contracts
{
    public interface IEntityFrameworkRepository<T> where T : class
    {
        IQueryable<T> All { get; }

        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);
    }
}
