using System.Data.Entity;
using DotLms.Data.Migrations;
using DotLms.Data.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DotLms.Data
{
    public class DotLmsDbContext : IdentityDbContext<User>
    {
        public DotLmsDbContext()
            : base("DotLms")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DotLmsDbContext, Configuration>());
        }

        public IDbSet<Page> Pages { get; set; }

        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }

        public new int SaveChanges()
        {
            return base.SaveChanges();
        }

        public static DotLmsDbContext Create()
        {
            return new DotLmsDbContext();
        }
    }
}
