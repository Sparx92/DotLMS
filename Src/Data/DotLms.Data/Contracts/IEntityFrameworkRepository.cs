using System.Linq;

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
