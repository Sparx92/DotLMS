using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bytes2you.Validation;
using DotLms.Services.Data;
using DotLms.Web.Models;

namespace DotLms.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly CourseService courseService;
        private readonly CourseCategoryService categoryService;

        public HomeController(CourseService courseService, CourseCategoryService categoryService)
        {
            Guard.WhenArgument(courseService, nameof(courseService)).IsNull().Throw();
            Guard.WhenArgument(categoryService, nameof(categoryService)).IsNull().Throw();

            this.courseService = courseService;
            this.categoryService = categoryService;
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            CourseListViewModel model = new CourseListViewModel
            {
                CourseViewModels = this.courseService.GetAllCourseViewModels(),
                CourseCategoryViewModels = this.categoryService.GetAllCategories()
            };


            return View(model);
        }
    }
}