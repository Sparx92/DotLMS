using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotLms.Data.Identity;
using DotLms.Data.Migrations;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DotLms.Data
{
    public class DotLmsDbContext : IdentityDbContext<User>
    {
        public DotLmsDbContext()
            :base("DotLms")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DotLmsDbContext, Configuration>());
        }

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
