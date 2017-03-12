using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using DotLms.Services.Data;
using DotLms.Web.Attributes;
using DotLms.Web.Models;
using PageViewModel = DotLms.Web.Models.PageViewModel;

namespace DotLms.Web.Areas.Backoffice.Controllers
{
    public class BackofficeHomeController : Controller
    {
        private PageCreationService pageCreationService;
        private PageRetrivalService pageRetrivalService;

        public BackofficeHomeController(PageCreationService pageCreationService, PageRetrivalService pageRetrivalService)
        {
            this.pageCreationService = pageCreationService;
            this.pageRetrivalService = pageRetrivalService;
        }
        
        [Security(Roles = Common.Roles.Admin)]
        public ActionResult Index()
        {
            BackOfficeIndexViewModel model = this.pageRetrivalService.GetAllPages();
            return View(model);
        }

        [Security(Roles = Common.Roles.Admin)]
        public ActionResult CreatePage()
        {
            return View();
        }

        [Security(Roles = Common.Roles.Admin)]
        public ActionResult EditPage(int? pageId)
        {
            var model = this.pageRetrivalService.GetPage(pageId);
            return View(model);
        }


        [Security(Roles = Common.Roles.Admin)]
        [HttpPost]
        public async Task<ActionResult> CreatePage(PageViewModel model)
        {
            string username = HttpContext.User.Identity.Name;
            this.pageCreationService.CreatePage(model, username);

            return View();
        }
    }
}