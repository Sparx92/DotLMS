using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DotLms.Web.Areas.Backoffice.Models;
using DotLms.Web.Attributes;
using DotLms.Web.Identity.Managers;
using Microsoft.AspNet.Identity.Owin;

namespace DotLms.Web.Areas.Backoffice.Controllers
{
    public class BackofficeHomeController : Controller
    {
        public BackofficeHomeController()
        {
        }


        [Security(Roles = Common.Roles.Admin)]
        public ActionResult Index()
        {
            return View();
        }
    }
}