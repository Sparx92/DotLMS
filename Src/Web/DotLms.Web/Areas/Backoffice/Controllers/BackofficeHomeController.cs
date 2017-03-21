using System.Web.Mvc;
using DotLms.Web.Attributes;

namespace DotLms.Web.Areas.Backoffice.Controllers
{
    public class BackOfficeHomeController : Controller
    {
        [Security(Roles = Common.Roles.Admin)]
        public ActionResult Index()
        {
            return View();
        }
    }
}