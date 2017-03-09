using System.Threading.Tasks;
using System.Web.Mvc;
using DotLms.Services.Data;
using DotLms.Web.Attributes;
using PageViewModel = DotLms.Web.Models.PageViewModel;

namespace DotLms.Web.Areas.Backoffice.Controllers
{
    public class BackofficeHomeController : Controller
    {
        private PageCreationService pageCreationService;

        public BackofficeHomeController(PageCreationService pageCreationService)
        {
            this.pageCreationService = pageCreationService;
        }


        [Security(Roles = Common.Roles.Admin)]
        public ActionResult Index()
        {
            return View();
        }

        [Security(Roles = Common.Roles.Admin)]
        public ActionResult CreatePage()
        {
            return View();
        }

        [Security(Roles = Common.Roles.Admin)]
        [HttpPost]
        public async Task<ActionResult> CreatePage(PageViewModel model)
        {
            string username = HttpContext.User.Identity.Name;
            this.pageCreationService.CreatePage(model,username);

            return View();
        }

        [Security(Roles = Common.Roles.Admin)]
        public ActionResult GetPageTree(int? ParentPageId)
        {
            return View();
        }
    }
}