using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DotLms.Web.Areas.Backoffice.Models;
using DotLms.Web.Identity.Managers;
using hbehr.recaptcha;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace DotLms.Web.Areas.Backoffice.Controllers
{
    public class BackOfficeAuthorizationController : Controller
    {
        private DotLmsSignInManager signInManager;
        private DotLmsUserManager userManager;

        public BackOfficeAuthorizationController(DotLmsSignInManager dotLmsSignInManager, DotLmsUserManager dotLmsUserManager)
        {
            this.signInManager = dotLmsSignInManager;
            this.userManager = dotLmsUserManager;
        }

        public DotLmsSignInManager SignInManager
        {
            get
            {
                return signInManager ?? HttpContext.GetOwinContext().Get<DotLmsSignInManager>();
            }
            private set
            {
                signInManager = value;
            }
        }

        public DotLmsUserManager UserManager
        {
            get
            {
                return userManager ?? HttpContext.GetOwinContext().GetUserManager<DotLmsUserManager>();
            }
            private set
            {
                userManager = value;
            }
        }

        [AllowAnonymous]
        public ActionResult Authorize()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Authorize(AuthViewModel model, string returnUrl)
        {
            string userResponse = HttpContext.Request.Params["g-recaptcha-response"];
            bool validCaptcha = ReCaptcha.ValidateCaptcha(userResponse);
            model.BotChallange = validCaptcha;
            
            if (!ModelState.IsValid && !validCaptcha)
            {
                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            SignInStatus result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);

            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                //case SignInStatus.RequiresVerification:
                //    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToLocal(Common.Constants.BackofficeUrl);
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "BackOfficeHome");
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
    }
}