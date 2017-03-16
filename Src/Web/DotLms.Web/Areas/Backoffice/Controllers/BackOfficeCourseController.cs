using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bytes2you.Validation;
using DotLms.Data.Models;
using DotLms.Services.Data;
using DotLms.Web.Models;

namespace DotLms.Web.Areas.Backoffice.Controllers
{
    public class BackOfficeCourseController : Controller
    {
        private readonly CourseCategoryService categoryService;
        private readonly CourseService courseService;
        private readonly FileService fileService;

        public BackOfficeCourseController(CourseCategoryService categoryService,
            CourseService courseService, FileService fileService)
        {
            Guard.WhenArgument(categoryService,nameof(categoryService)).IsNull().Throw();
            Guard.WhenArgument(courseService, nameof(courseService)).IsNull().Throw();
            Guard.WhenArgument(fileService, nameof(fileService)).IsNull().Throw();

            this.categoryService = categoryService;
            this.courseService = courseService;
            this.fileService = fileService;
        }

        // GET: Backoffice/BackOfficeCourse
        public ActionResult Index()
        {
            IEnumerable<CourseViewModel> model = courseService.GetAllCourseViewModels();
            return View(model);
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
                MediaItem mediaItem = this.fileService.SaveFile(model.File);

                var category = categoryService.GetCategoryViewModel(model.Category.Name);

                model.Category = category;

                this.courseService.CreateCourse(model,mediaItem);
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