using System.Threading.Tasks;
using System.Web.Mvc;
using DotLms.Services.Data;
using DotLms.Web.Attributes;
using DotLms.Web.Models;
using DotLms.Web.Models.Backoffice;
using PageViewModel = DotLms.Web.Models.PageViewModel;

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