using System.Linq;
using System.Web.Mvc;
using DotLms.Data;
using DotLms.Data.Models;
using DotLms.Web.Identity.Managers;
using Microsoft.AspNet.Identity;

namespace DotLms.Web.Controllers
{
    public class HomeController : Controller
    {
        public string AddUser()
        {
            User user;
            DotLmsUserStore store = new DotLmsUserStore(new DotLmsDbContext());
            DotLmsUserManager userManager = new DotLmsUserManager(store);
            user = new User
            {
                UserName = "TestUser",
                Email = "TestUser@test.com"
            };

            var result = userManager.Create(user);
            if (!result.Succeeded)
            {
                return result.Errors.First();
            }
            return "User Added";
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}