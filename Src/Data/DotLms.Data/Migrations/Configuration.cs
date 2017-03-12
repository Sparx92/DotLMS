using System.Data.Entity.Migrations;
using System.Linq;

using DotLms.Data.Models;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DotLms.Data.Migrations
{

    internal sealed class Configuration : DbMigrationsConfiguration<DotLmsEfDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(DotLmsEfDbContext context)
        {
            this.SeedRoles(context);
            this.SeedUsers(context);
        }

        private void SeedRoles(DotLmsEfDbContext context)
        {
            RoleStore<IdentityRole> roleStore = new RoleStore<IdentityRole>(context);
            RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(roleStore);

            if (!context.Roles.Any(r => r.Name == Common.Roles.Admin))
            {
                IdentityRole role = new IdentityRole { Name = Common.Roles.Admin };
                roleManager.Create(role);
            }

            if (!context.Roles.Any(r => r.Name == Common.Roles.Normal))
            {
                IdentityRole role = new IdentityRole { Name = Common.Roles.Normal };
                roleManager.Create(role);
            }
        }

        private void SeedUsers(DotLmsEfDbContext context)
        {
            UserStore<User> userStore = new UserStore<User>(context);
            UserManager<User> userManager = new UserManager<User>(userStore);

            if (!context.Users.Any(u => u.UserName == "admin@admin.com"))
            {
                User adminUser = new User { UserName = "admin@admin.com", Email = "admin@admin.com" };

                IdentityResult isSuccessful = userManager.Create(adminUser, "admin1");
                if (isSuccessful.Succeeded)
                {
                    userManager.AddToRole(adminUser.Id, Common.Roles.Admin);
                }
            }
        }
    }
}
