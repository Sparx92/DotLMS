using System.Linq;
using System.Web.Mvc;
using System.Web.WebPages;
using Bytes2you.Validation;
using DotLms.Data;
using DotLms.Data.Models;
using DotLms.Services.Data;
using DotLms.Web.Identity.Managers;
using DotLms.Web.Models;
using Microsoft.AspNet.Identity;

namespace DotLms.Web.Controllers
{
    public class DotLmsPagesController : Controller
    {
        private readonly PageRetrivalService pageRetrivalService;

        public DotLmsPagesController(PageRetrivalService pageRetrivalService)
        {
            Guard.WhenArgument(pageRetrivalService, nameof(pageRetrivalService)).IsNull().Throw();

            this.pageRetrivalService = pageRetrivalService;
        }

        public ActionResult GetPage(string pageName)
        {
            PageViewModel model = this.pageRetrivalService.GetPage(pageName);

            if (model == null)
            {
                
            }

            return View(model);
        }
    }
}