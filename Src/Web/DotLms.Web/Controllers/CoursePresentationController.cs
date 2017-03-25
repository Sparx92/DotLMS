using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.WebPages;
using Bytes2you.Validation;
using DotLms.Services.Data;
using DotLms.Services.Data.Contracts;
using DotLms.Services.Providers.Contracts;
using DotLms.Web.Models;
using Newtonsoft.Json;

namespace DotLms.Web.Controllers
{
    public class CoursePresentationController : Controller
    {
        private readonly IPageRetrivalService pageRetrivalService;
        private readonly ICourseService courseService;
        private readonly ICourseCategoryService categoryService;
        private IJsonConvertProvider<CourseListViewModel> jsonConvertProvider;

        public CoursePresentationController(
            IPageRetrivalService pageRetrivalService,
            ICourseService courseService,
            ICourseCategoryService categoryService,
            IJsonConvertProvider<CourseListViewModel> jsonConvertProvider)
        {
            Guard.WhenArgument(pageRetrivalService, nameof(pageRetrivalService)).IsNull().Throw();
            Guard.WhenArgument(courseService, nameof(courseService)).IsNull().Throw();
            Guard.WhenArgument(categoryService, nameof(categoryService)).IsNull().Throw();
            Guard.WhenArgument(jsonConvertProvider, nameof(jsonConvertProvider)).IsNull().Throw();

            this.pageRetrivalService = pageRetrivalService;
            this.courseService = courseService;
            this.categoryService = categoryService;
            this.jsonConvertProvider = jsonConvertProvider;
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            CourseListViewModel model = new CourseListViewModel();
            model.CourseCategoryViewModels = this.categoryService.GetAllCategories();
            model.CourseViewModels = this.courseService.GetAllCourseViewModels();

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string query)
        {
            IEnumerable<CourseViewModel> courseViewModels = this.courseService.GetCourseViewModelsByName(query);

            return PartialView("_CourseGrid", courseViewModels);
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