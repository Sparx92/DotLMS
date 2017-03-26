using System;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using DotLms.Services.Data.Contracts;
using DotLms.Services.Providers.Contracts;
using DotLms.Web.Areas.Backoffice.Controllers;
using DotLms.Web.Attributes;
using DotLms.Web.Controllers;
using DotLms.Web.Models;
using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;

namespace DotLms.Web.Tests.Controllers.Backoffice.BackOfficeCourseControllerUnitTests
{
    [TestFixture]
    [Category(Common.TestConstants.UnitTestCategory)]
    public class CreateCourseHttpPostTests
    {
        private Mock<ICourseCategoryService> mockedCategoryService;
        private Mock<ICourseService> mockedCourseService;
        private Mock<IFileService> mockedFileService;
        private Mock<IMemoryCacheProvider> mockedMemoryCacheProvider;

        [SetUp]
        public void Init()
        {

            this.mockedCategoryService = new Mock<ICourseCategoryService>();

            this.mockedCourseService = new Mock<ICourseService>();
            this.mockedCourseService
                .Setup(x => x.CreateCourse(It.IsAny<CourseCreationViewModel>(), It.IsAny<MediaItemViewModel>()))
                .Returns(new CourseViewModel{ UglyName = "name"});

            this.mockedCategoryService.Setup(x => x.GetCategoryViewModel(It.IsAny<string>()))
                .Returns(new CourseCategoryViewModel{Id=1,Name = "CategoryName"});


            this.mockedFileService = new Mock<IFileService>();
            this.mockedFileService.Setup(x => x.SaveFile(It.IsAny<HttpPostedFileBase>()))
                .Returns(new MediaItemViewModel());

            this.mockedMemoryCacheProvider = new Mock<IMemoryCacheProvider>();
        }

        [Test]
        public void CreateCourseHttpPost_ShouldHaveHttpGetAttribute()
        {
            // Arrange, Act
            MethodInfo methodInfo = typeof(BackOfficeCourseController).GetMethod("CreateCourse", new Type[] { typeof(CourseCreationViewModel) });
            bool httpGetIsDefinded = Attribute.IsDefined(methodInfo, typeof(HttpPostAttribute));
            
            // Assert
            Assert.IsTrue(httpGetIsDefinded);
        }

        [Test]
        public void CreateCourseHttpPost_ShouldHaveBackofficeAuthorizationAttribute()
        {
            // Arrange, Act
            MethodInfo methodInfo = typeof(BackOfficeCourseController).GetMethod("CreateCourse", new Type[] { typeof(CourseCreationViewModel) });
            bool backofficeAuthorizatuonAttributeIsDefined = Attribute.IsDefined(
                methodInfo,
                typeof(BackofficeAuthorizatuonAttribute));

            // Assert
            Assert.IsTrue(backofficeAuthorizatuonAttributeIsDefined);
        }

        [Test]
        public void CreateCourseHttpPost_ShouldShouldRenderDefaultView()
        {
            // Arrange
            BackOfficeCourseController controller = this.GetController();

            // Act and Assert
            controller.WithCallTo(x => x.CreateCourse()).ShouldRenderDefaultView();
        }

        [Test]
        public void CreateCourseHttpPost_ShouldRedirect_WhenModelStateIsValid()
        {
            // Arrange
            BackOfficeCourseController controller = this.GetController();
            CourseCreationViewModel model = new CourseCreationViewModel
            {
                UglyName = "name",
                Category = new CourseCategoryViewModel { Id = 1, Name = "CategoryName"}
            };

            // Act & Assert
            controller.WithCallTo(x => x.CreateCourse(model))
                .ShouldRedirectTo<CoursePresentationController>(x => x.GetCourse(model.UglyName));
        }


        [Test]
        public void CreateCourseHttpPost_ShouldCallCategoryServiceGetAllCategoriesOnce()
        {
            // Arrange
            BackOfficeCourseController controller = this.GetController();
            CourseCreationViewModel model = new CourseCreationViewModel
            {
                UglyName = "name",
                Category = new CourseCategoryViewModel { Id = 1, Name = "CategoryName" }
            };

            // Act & Assert
            controller.WithCallTo(x => x.CreateCourse(model))
                .ShouldRedirectTo<CoursePresentationController>(x => x.GetCourse(model.UglyName));

            this.mockedCategoryService.Verify(x=>x.GetAllCategories(),Times.Once);
        }

        [Test]
        public void CreateCourseHttpPost_ShouldCallFileServiceSaveFilesOnce()
        {
            // Arrange
            BackOfficeCourseController controller = this.GetController();
            CourseCreationViewModel model = new CourseCreationViewModel
            {
                UglyName = "name",
                Category = new CourseCategoryViewModel { Id = 1, Name = "CategoryName" }
            };

            // Act & Assert
            controller.WithCallTo(x => x.CreateCourse(model))
                .ShouldRedirectTo<CoursePresentationController>(x => x.GetCourse(model.UglyName));
            
            this.mockedFileService.Verify(x => x.SaveFile(It.IsAny<HttpPostedFileBase>()), Times.Once);
        }

        [Test]
        public void CreateCourseHttpPost_ShouldCallCategoryServiceGetCategoryViewModelOnce()
        {
            // Arrange
            BackOfficeCourseController controller = this.GetController();
            CourseCreationViewModel model = new CourseCreationViewModel
            {
                UglyName = "name",
                Category = new CourseCategoryViewModel { Id = 1, Name = "CategoryName" }
            };

            // Act & Assert
            controller.WithCallTo(x => x.CreateCourse(model))
                .ShouldRedirectTo<CoursePresentationController>(x => x.GetCourse(model.UglyName));

            this.mockedCategoryService.Verify(x => x.GetCategoryViewModel(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void CreateCourseHttpPost_ShouldCallCourseServiceCreateCourseOnce()
        {
            // Arrange
            BackOfficeCourseController controller = this.GetController();
            CourseCreationViewModel model = new CourseCreationViewModel
            {
                UglyName = "name",
                Category = new CourseCategoryViewModel { Id = 1, Name = "CategoryName" }
            };

            // Act & Assert
            controller.WithCallTo(x => x.CreateCourse(model))
                .ShouldRedirectTo<CoursePresentationController>(x => x.GetCourse(model.UglyName));

            this.mockedCourseService.Verify(x => x.CreateCourse(It.IsAny<CourseCreationViewModel>(),It.IsAny<MediaItemViewModel>()), Times.Once);
        }


        private BackOfficeCourseController GetController()
        {
            return new BackOfficeCourseController(
                    this.mockedCategoryService.Object,
                    this.mockedCourseService.Object,
                    this.mockedFileService.Object
                    );
        }
    }
}