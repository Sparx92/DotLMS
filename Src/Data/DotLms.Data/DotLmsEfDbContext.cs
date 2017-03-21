using System.Data.Entity;
using DotLms.Data.Contracts;
using DotLms.Data.Migrations;
using DotLms.Data.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DotLms.Data
{
    public class DotLmsEfDbContext : IdentityDbContext<User>, IDotLmsEfDbContext
    {
        public DotLmsEfDbContext()
            : base("DotLms")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DotLmsEfDbContext, Configuration>());
        }

        public IDbSet<Page> Pages { get; set; }

        public IDbSet<MediaItem> MediaItems { get; set; }

        public IDbSet<CourseCategory> CourseCategories { get; set; }

        public IDbSet<Course> Courses { get; set; }

       
        public  IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }

        public static DotLmsEfDbContext Create()
        {
            return new DotLmsEfDbContext();
        }
    }
}
