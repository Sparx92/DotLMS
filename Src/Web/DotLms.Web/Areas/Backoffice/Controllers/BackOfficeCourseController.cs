using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bytes2you.Validation;
using DotLms.Services.Data;
using DotLms.Web.Models;

namespace DotLms.Web.Areas.Backoffice.Controllers
{
    public class BackOfficeCourseController : Controller
    {
        private CourseCategoryService categoryService;

        public BackOfficeCourseController(CourseCategoryService categoryService)
        {
            Guard.WhenArgument(categoryService,nameof(categoryService)).IsNull().Throw();

            this.categoryService = categoryService;
        }

        // GET: Backoffice/BackOfficeCourse
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateCourse()
        {
            var model = new CourseCreationViewModel
            {
                Categories= this.categoryService.GetAllCategories(),
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult CreateCourse(CourseCreationViewModel model)
        {
            if (ModelState.IsValid)
            {
                var debg = model;
            }
            return View(model);
        }

        public ActionResult CreateCourseCategory()
        {
            return View();
        }
    }
}