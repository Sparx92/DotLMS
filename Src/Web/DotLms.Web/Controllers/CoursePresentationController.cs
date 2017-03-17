using System.Web.Mvc;
using System.Web.WebPages;
using Bytes2you.Validation;
using DotLms.Services.Data;
using DotLms.Web.Models;

namespace DotLms.Web.Controllers
{
    public class CoursePresentationController : Controller
    {
        private readonly PageRetrivalService pageRetrivalService;
        private readonly CourseService courseService;
        private readonly CourseCategoryService categoryService;

        public CoursePresentationController(
            PageRetrivalService pageRetrivalService,
            CourseService courseService,
            CourseCategoryService categoryService)
        {
            Guard.WhenArgument(pageRetrivalService, nameof(pageRetrivalService)).IsNull().Throw();
            Guard.WhenArgument(courseService, nameof(courseService)).IsNull().Throw();
            Guard.WhenArgument(categoryService, nameof(categoryService)).IsNull().Throw();

            this.pageRetrivalService = pageRetrivalService;
            this.courseService = courseService;
            this.categoryService = categoryService;
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            CourseListViewModel model = new CourseListViewModel();
            model.CourseCategoryViewModels = this.categoryService.GetAllCategories();
            model.CourseViewModels = this.courseService.GetAllCourseViewModels();

            return View(model);
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

        public ActionResult GetPage(string courseName, string childPageName)
        {
            if (string.IsNullOrWhiteSpace(courseName))
            {
                this.HttpContext.RedirectLocal("/");
            }

            PageViewModel model = this.pageRetrivalService.GetPage(childPageName);

            if (model == null)
            {

            }

            return View(model);
        }
    }
}