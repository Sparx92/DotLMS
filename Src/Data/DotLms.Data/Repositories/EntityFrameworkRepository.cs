using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using Bytes2you.Validation;
using DotLms.Data.Contracts;

namespace DotLms.Data.Repositories
{
    public class EntityFrameworkRepository<T> : IEntityFrameworkRepository<T> where T : class
    {
        public EntityFrameworkRepository(IDotLmsEfDbContext context)
        {
            Guard.WhenArgument(context, nameof(context)).IsNull().Throw();
            
            this.Context = context;
            this.DbSet = this.Context.Set<T>();
        }

        public IQueryable<T> All
        {
            get { return this.DbSet; }
        }

        public IDotLmsEfDbContext Context { get; set; }

        protected IDbSet<T> DbSet { get; set; }

        public void Add(T entity)
        {
            var entry = AttachIfDetached(entity);
            entry.State = EntityState.Added;
        }

        public void Update(T entity)
        {
            var entry = AttachIfDetached(entity);
            entry.State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            var entry = AttachIfDetached(entity);
            entry.State = EntityState.Deleted;
        }

        protected DbEntityEntry AttachIfDetached(T entity)
        {
            var entry = this.Context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                this.DbSet.Attach(entity);
            }

            return entry;
        }
    }
}