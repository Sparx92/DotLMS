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
    public class CoursePresentationController : Controller
    {
        private readonly PageRetrivalService pageRetrivalService;
        private CourseService courseService;

        public CoursePresentationController(
            PageRetrivalService pageRetrivalService,
            CourseService courseService)
        {
            Guard.WhenArgument(pageRetrivalService, nameof(pageRetrivalService)).IsNull().Throw();
            Guard.WhenArgument(courseService, nameof(courseService)).IsNull().Throw();

            this.pageRetrivalService = pageRetrivalService;
            this.courseService = courseService;
        }

        public ActionResult GetCourse(string courseName)
        {
            if (string.IsNullOrWhiteSpace(courseName))
            {
                this.HttpContext.RedirectLocal("/");
            }

            CourseViewModel model = this.courseService.GetCourseViewModel(courseName);

            if (model == null)
            {
                
            }

            return View(model);
        }
    }
}