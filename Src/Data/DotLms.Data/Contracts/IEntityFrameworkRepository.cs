using System.Data.Entity;
using System.Linq;

namespace DotLms.Data.Contracts
{
    public interface IEntityFrameworkRepository<T> where T : class
    {
        IQueryable<T> All { get; }

        IDbSet<T> DbSet { get; set; }

        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);
    }
}
