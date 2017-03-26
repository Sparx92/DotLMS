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
        private readonly IMemoryCacheProvider memoryCacheProvider;

        public CoursePresentationController(
            IPageRetrivalService pageRetrivalService,
            ICourseService courseService,
            ICourseCategoryService categoryService,
            IMemoryCacheProvider memoryCacheProvider)
        {
            Guard.WhenArgument(pageRetrivalService, nameof(pageRetrivalService)).IsNull().Throw();
            Guard.WhenArgument(courseService, nameof(courseService)).IsNull().Throw();
            Guard.WhenArgument(categoryService, nameof(categoryService)).IsNull().Throw();
            Guard.WhenArgument(memoryCacheProvider, nameof(memoryCacheProvider)).IsNull().Throw();

            this.pageRetrivalService = pageRetrivalService;
            this.courseService = courseService;
            this.categoryService = categoryService;
            this.memoryCacheProvider = memoryCacheProvider;
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            string cachedModelName = "AllCourseViewModels";
            object cachedModel = this.memoryCacheProvider.MemoryCache.Get(cachedModelName);
            if (cachedModel == null)
            {
                IEnumerable<CourseViewModel> model = courseService.GetAllCourseViewModels();
                this.memoryCacheProvider.MemoryCache.Add(cachedModelName, model,
                    Common.DateTimeVariables.FiveMinutesFromUtcNow);

                var monitor = this.memoryCacheProvider
                    .MemoryCache.CreateCacheEntryChangeMonitor(new List<string> { cachedModelName });

                return View(model);
            }

            return View(cachedModel);
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
                return RedirectToAction("NotFound", "Error");
            }

            return View(model);
        }

        public ActionResult GetPage(string courseName, string childPageName)
        {
            if (string.IsNullOrWhiteSpace(courseName))
            {
                return RedirectToAction("NotFound", "Error");
            }

            PageViewModel model = this.pageRetrivalService.GetPage(childPageName);

            if (model == null)
            {
                return RedirectToAction("NotFound", "Error");
            }

            return View(model);
        }
    }
}