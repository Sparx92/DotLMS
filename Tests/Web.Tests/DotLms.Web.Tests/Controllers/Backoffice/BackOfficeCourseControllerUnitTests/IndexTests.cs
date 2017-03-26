using System;
using System.Collections.Generic;
using System.Web.Mvc;
using DotLms.Services.Data.Contracts;
using DotLms.Services.Providers.Contracts;
using DotLms.Web.Areas.Backoffice.Controllers;
using DotLms.Web.Attributes;
using DotLms.Web.Models;
using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;

namespace DotLms.Web.Tests.Controllers.Backoffice.BackOfficeCourseControllerUnitTests
{
    [TestFixture]
    [Category(Common.TestConstants.UnitTestCategory)]
    public class IndexTests
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
            this.mockedFileService = new Mock<IFileService>();
            this.mockedMemoryCacheProvider = new Mock<IMemoryCacheProvider>();
        }

        [Test]
        public void Index_ShouldHaveBackofficeAuthorizationAttribute()
        {
            // Arrange, Act
            bool backofficeAuthorizatuonAttributeIsDefined = Attribute.IsDefined(
                typeof(BackOfficeCourseController).GetMethod(nameof(BackOfficeCourseController.Index)),
                typeof(BackofficeAuthorizatuonAttribute));

            // Assert
            Assert.IsTrue(backofficeAuthorizatuonAttributeIsDefined);
        }

        [Test]
        public void Index_ShouldHaveHttpGetAttribute()
        {
            // Arrange, Act
            bool httpGetIsDefinded = Attribute.IsDefined(
                typeof(BackOfficeCourseController).GetMethod(nameof(BackOfficeCourseController.Index)),
                typeof(HttpGetAttribute));

            // Assert
            Assert.IsTrue(httpGetIsDefinded);
        }

        [Test]
        public void Index_ShouldShouldRenderDefaultViewWithCorrectModel()
        {
            // Arrange
            BackOfficeCourseController controller = this.GetController();

            // Act & Assert
            controller.WithCallTo(x => x.Index())
                .ShouldRenderDefaultView()
                .WithModel<IEnumerable<CourseViewModel>>();
        }

        [Test]
        public void Index_ShouldCall_CouseServiceGetAllCourseViewModel_Once()
        {
            // Arrange
            BackOfficeCourseController controller = this.GetController();

            // Act
            controller.Index();

            // Assert
            this.mockedCourseService.Verify(x => x.GetAllCourseViewModels(), Times.Once);
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