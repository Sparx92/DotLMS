using System.Web.Mvc;
using DotLms.Services.Data;
using DotLms.Services.Data.Contracts;
using DotLms.Web.Attributes;
using DotLms.Web.Models;
using DotLms.Web.Models.Backoffice;

namespace DotLms.Web.Areas.Backoffice.Controllers
{
    public class BackOfficePagesController : Controller
    {
        private readonly IPageCreationService pageCreationService;
        private readonly IPageRetrivalService pageRetrivalService;

        public BackOfficePagesController(PageCreationService pageCreationService, PageRetrivalService pageRetrivalService)
        {
            this.pageCreationService = pageCreationService;
            this.pageRetrivalService = pageRetrivalService;
        }

        [BackofficeAuthorizatuon]
        public ActionResult Index()
        {
            BackOfficeIndexViewModel model = this.pageRetrivalService.GetAllPages();
            return View(model);
        }

        [BackofficeAuthorizatuon]
        public ActionResult CreatePage(int courseId)
        {
            PageViewModel model = new PageViewModel { ParentCourseId = courseId };
            return View(model);
        }

        [BackofficeAuthorizatuon]
        public ActionResult EditPage(int pageId)
        {
            PageViewModel model = this.pageRetrivalService.GetPage(pageId);
            return View(model);
        }

        [BackofficeAuthorizatuon]
        [HttpPost]
        public ActionResult CreatePage(PageViewModel model)
        {
            string username = HttpContext.User.Identity.Name;
            this.pageCreationService.CreatePage(model, username);

            return View();
        }
    }
}