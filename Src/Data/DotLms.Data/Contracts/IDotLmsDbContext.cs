using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using DotLms.Data.Models;

namespace DotLms.Data.Contracts
{
    public interface IDotLmsDbContext
    {
        IDbSet<Page> Pages { get; set; }

        new IDbSet<T> Set<T>() where T : class;

        DbEntityEntry<T> Entry<T>(T entity) where T : class;

        new int SaveChanges();
    }
}