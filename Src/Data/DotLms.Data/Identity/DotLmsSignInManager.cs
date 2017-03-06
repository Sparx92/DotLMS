using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;

namespace DotLms.Data.Identity
{
    public class DotLmsSignInManager : SignInManager<User, string>
    {
        public DotLmsSignInManager(
           DotLmsUserManager userManager,
           IAuthenticationManager authenticationManager) :
           base(userManager, authenticationManager)
        {
        }

        public static DotLmsSignInManager Create(
            IdentityFactoryOptions<DotLmsSignInManager> options,
            IOwinContext context)
        {
            return new DotLmsSignInManager(
                context.GetUserManager<DotLmsUserManager>(),
                context.Authentication);
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(User user)
        {
            return user.GenerateUserIdentityAsync((DotLmsUserManager)UserManager);
        }
    }
}