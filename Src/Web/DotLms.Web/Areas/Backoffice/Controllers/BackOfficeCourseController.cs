using System;
using System.Collections.Generic;
using System.Runtime.Caching;
using System.Web.Mvc;
using Bytes2you.Validation;
using DotLms.Data.Models;
using DotLms.Services.Data;
using DotLms.Services.Data.Contracts;
using DotLms.Services.Providers.Contracts;
using DotLms.Web.Attributes;
using DotLms.Web.Models;
using Microsoft.Ajax.Utilities;

namespace DotLms.Web.Areas.Backoffice.Controllers
{
    public class BackOfficeCourseController : Controller
    {
        private readonly ICourseCategoryService categoryService;
        private readonly ICourseService courseService;
        private readonly IFileService fileService;
        private readonly IMemoryCacheProvider memoryCacheProvider;

        public BackOfficeCourseController(ICourseCategoryService categoryService,
            ICourseService courseService, IFileService fileService, IMemoryCacheProvider memoryCacheProvider)
        {
            Guard.WhenArgument(categoryService, nameof(categoryService)).IsNull().Throw();
            Guard.WhenArgument(courseService, nameof(courseService)).IsNull().Throw();
            Guard.WhenArgument(fileService, nameof(fileService)).IsNull().Throw();
            Guard.WhenArgument(memoryCacheProvider, nameof(memoryCacheProvider)).IsNull().Throw();

            this.categoryService = categoryService;
            this.courseService = courseService;
            this.fileService = fileService;
            this.memoryCacheProvider = memoryCacheProvider;
        }

        [BackofficeAuthorizatuon(Roles = Common.Roles.Admin)]
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
                    .MemoryCache.CreateCacheEntryChangeMonitor(new List<string> {cachedModelName});
                
                return View(model);
            }

            return View(cachedModel);
        }

        [BackofficeAuthorizatuon(Roles = Common.Roles.Admin)]
        public ActionResult CreateCourse()
        {
            CourseCreationViewModel model = new CourseCreationViewModel
            {
                Categories = this.categoryService.GetAllCategories(),
            };
            return View(model);
        }

        [HttpPost]
        [BackofficeAuthorizatuon(Roles = Common.Roles.Admin)]
        public ActionResult CreateCourse(CourseCreationViewModel model)
        {
            model.Categories = this.categoryService.GetAllCategories();
            if (ModelState.IsValid)
            {
                MediaItemViewModel mediaItem = this.fileService.SaveFile(model.File);

                CourseCategoryViewModel category = categoryService.GetCategoryViewModel(model.Category.Name);

                model.Category = category;

                CourseViewModel createdCourse = this.courseService.CreateCourse(model, mediaItem);

                return RedirectToAction("GetCourse", "CoursePresentation",
                    new { Area = "", courseName = createdCourse.UglyName });
            }
            return View(model);
        }

        [BackofficeAuthorizatuon(Roles = Common.Roles.Admin)]
        public ActionResult CreateCourseCategory()
        {
            return View();
        }

        [HttpPost]
        [BackofficeAuthorizatuon(Roles = Common.Roles.Admin)]
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

        [BackofficeAuthorizatuon(Roles = Common.Roles.Admin)]
        public ActionResult ManageCourse(string courseName)
        {
            CourseCreationViewModel model = this.courseService.GetCourseCreationViewModel(courseName);

            model.Categories = this.categoryService.GetAllCategories();

            return View(model);
        }

        [HttpPost]
        [BackofficeAuthorizatuon(Roles = Common.Roles.Admin)]
        public ActionResult ManageCourse(CourseCreationViewModel model)
        {
            model.Categories = this.categoryService.GetAllCategories();
            if (ModelState.IsValid)
            {
                CourseCategoryViewModel category = categoryService.GetCategoryViewModel(model.Category.Name);

                model.Category = category;

                if (model.File != null)
                {
                    MediaItemViewModel mediaItem = this.fileService.SaveFile(model.File);
                    Course courseWithImage = this.courseService.UpdateCourse(model, mediaItem);

                    return RedirectToAction("GetCourse", "CoursePresentation",
                        new { Area = "", courseName = courseWithImage.UglyName });
                }


                Course courseWithoutImage = this.courseService.UpdateCourse(model);
                return RedirectToAction("GetCourse", "CoursePresentation",
                        new { Area = "", courseName = courseWithoutImage.UglyName });

            }
            return View(model);
        }
    }
}