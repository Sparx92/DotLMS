using System.Web.Mvc;
using Bytes2you.Validation;
using DotLms.Services.Data;
using DotLms.Services.Data.Contracts;
using DotLms.Web.Attributes;
using DotLms.Web.Models;

namespace DotLms.Web.Areas.Backoffice.Controllers
{
    public class BackOfficeMediaController : Controller
    {
        private readonly IFileService fileService;

        public BackOfficeMediaController(IFileService fileService)
        {
            Guard.WhenArgument(fileService, nameof(fileService)).IsNull().Throw();

            this.fileService = fileService;
        }

        [BackofficeAuthorizatuon(Roles = Common.Roles.Admin)]
        public ActionResult Index()
        {
            return View();
        }

        [BackofficeAuthorizatuon(Roles = Common.Roles.Admin)]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [BackofficeAuthorizatuon(Roles = Common.Roles.Admin)]
        public ActionResult Add(MediaItemViewModel model)
        {
            if (ModelState.IsValid)
            {
                fileService.SaveFile(model.File);
            }
            return View();
        }
    }
}