using DotLms.Data.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DotLms.Data.Migrations
{
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DotLms.Data.DotLmsDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(DotLms.Data.DotLmsDbContext context)
        {
            this.SeedRoles(context);
            this.SeedUsers(context);
        }

        private void SeedRoles(DotLmsDbContext context)
        {
            RoleStore<IdentityRole> roleStore = new RoleStore<IdentityRole>(context);
            RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(roleStore);

            if (!context.Roles.Any(r => r.Name == DotLms.Common.Roles.Admin))
            {
                IdentityRole role = new IdentityRole { Name = DotLms.Common.Roles.Admin };
                roleManager.Create(role);
            }

            if (!context.Roles.Any(r => r.Name == DotLms.Common.Roles.Normal))
            {
                IdentityRole role = new IdentityRole { Name = DotLms.Common.Roles.Normal };
                roleManager.Create(role);
            }
        }

        private void SeedUsers(DotLmsDbContext context)
        {
            UserStore<User> userStore = new UserStore<User>(context);
            UserManager<User> userManager = new UserManager<User>(userStore);

            if (!context.Users.Any(u => u.UserName == "admin@admin.com"))
            {
                User adminUser = new User { UserName = "admin@admin.com", Email = "admin@admin.com" };

                IdentityResult isSuccessful = userManager.Create(adminUser, "admin1");
                userManager.AddToRole(adminUser.Id, DotLms.Common.Roles.Admin);
            }
        }
    }
}
