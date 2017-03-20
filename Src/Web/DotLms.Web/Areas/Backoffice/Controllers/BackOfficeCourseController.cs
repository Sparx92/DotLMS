using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
            Guard.WhenArgument(categoryService, nameof(categoryService)).IsNull().Throw();
            Guard.WhenArgument(courseService, nameof(courseService)).IsNull().Throw();
            Guard.WhenArgument(fileService, nameof(fileService)).IsNull().Throw();

            this.categoryService = categoryService;
            this.courseService = courseService;
            this.fileService = fileService;
        }

        public ActionResult Index()
        {
            IEnumerable<CourseViewModel> model = courseService.GetAllCourseViewModels();
            return View(model);
        }

        public ActionResult CreateCourse()
        {
            CourseCreationViewModel model = new CourseCreationViewModel
            {
                Categories = this.categoryService.GetAllCategories(),
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

                CourseCategoryViewModel category = categoryService.GetCategoryViewModel(model.Category.Name);

                model.Category = category;

                Course createdCourse = this.courseService.CreateCourse(model, mediaItem);

                return RedirectToAction("GetCourse", "CoursePresentation",
                    new { Area = "", courseName = createdCourse.UglyName });
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
            if (ModelState.IsValid)
            {
                ViewBag.CourseCreationMessage = $"Successfuly created category: \"{model.Name}\".";
                ViewBag.CourseCreationMessageType = "success";
                try
                {
                    this.categoryService.CreateNewCategory(model);
                }
                catch (Exception exception)
                {
                    ViewBag.CourseCreationMessageType = "error";
                    ViewBag.CourseCreationMessage = $"Category: \"{model.Name}\", already exists, please try another one.";
                }
            }
            return View(model);
        }

        public ActionResult ManageCourse(string courseName)
        {
            CourseCreationViewModel model = this.courseService.GetCourseCreationViewModel(courseName);

            model.Categories = this.categoryService.GetAllCategories();

            return View(model);
        }
    }
}