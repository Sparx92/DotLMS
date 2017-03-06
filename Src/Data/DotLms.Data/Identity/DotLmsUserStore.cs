using Microsoft.AspNet.Identity.EntityFramework;

namespace DotLms.Data.Identity
{
    public class DotLmsUserStore : UserStore<User>
    {
        public DotLmsUserStore(DotLmsDbContext dbContext) : base(dbContext)
        {
            
        }
    }
}