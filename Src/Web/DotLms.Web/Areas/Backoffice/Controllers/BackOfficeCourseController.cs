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
            model.Categories = this.categoryService.GetAllCategories();
            if (ModelState.IsValid)
            {
                var category = categoryService.GetCategoryViewModel(model.Category.Name);
                model.Category = category;
                //this.
            }
            return View(model);
        }

        public ActionResult CreateCourseCategory()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateCourseCategory(CourseCategoryViewModel model)
        {
            this.categoryService.CreateNewCategory(model);
            return View(model);
        }
    }
}