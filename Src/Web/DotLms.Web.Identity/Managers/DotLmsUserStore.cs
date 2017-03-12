using DotLms.Data;
using DotLms.Data.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DotLms.Web.Identity.Managers
{
    public class DotLmsUserStore : UserStore<User>
    {
        public DotLmsUserStore(DotLmsEfDbContext dotLmsEfDbContext) : base(dotLmsEfDbContext)
        {
            
        }
    }
}