using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DotLms.Services.Data;
using DotLms.Web.Attributes;
using DotLms.Web.Models;
using DotLms.Web.Models.Backoffice;

namespace DotLms.Web.Areas.Backoffice.Controllers
{
    public class BackOfficePagesController : Controller
    {
        private readonly PageCreationService pageCreationService;
        private readonly PageRetrivalService pageRetrivalService;

        public BackOfficePagesController(PageCreationService pageCreationService, PageRetrivalService pageRetrivalService)
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
        public ActionResult CreatePage(int courseId)
        {
            PageViewModel model = new PageViewModel { ParentCourseId = courseId };
            return View(model);
        }

        [Security(Roles = Common.Roles.Admin)]
        public ActionResult EditPage(int? pageId)
        {
            PageViewModel model = this.pageRetrivalService.GetPage(pageId);
            return View(model);
        }

        [Security(Roles = Common.Roles.Admin)]
        [HttpPost]
        public ActionResult CreatePage(PageViewModel model)
        {
            string username = HttpContext.User.Identity.Name;
            this.pageCreationService.CreatePage(model, username);

            return View();
        }
    }
}