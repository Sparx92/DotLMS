using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DotLms.Web.Areas.Backoffice.Controllers
{
    public class BackOfficeCourseController : Controller
    {
        // GET: Backoffice/BackOfficeCourse
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateCourse()
        {
            return View();
        }

        public ActionResult CreateCourseCategory()
        {
            return View();
        }
    }
}