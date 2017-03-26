using System.Web.Mvc;
using DotLms.Web.Attributes;

namespace DotLms.Web.Areas.Backoffice.Controllers
{
    public class BackOfficeHomeController : Controller
    {
        [BackofficeAuthorizatuon]
        public ActionResult Index()
        {
            return View();
        }
    }
}