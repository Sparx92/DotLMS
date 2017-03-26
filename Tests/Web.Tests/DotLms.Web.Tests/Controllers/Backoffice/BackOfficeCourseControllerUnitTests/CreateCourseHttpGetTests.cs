using System;
using System.Reflection;
using System.Web.Mvc;
using DotLms.Services.Data.Contracts;
using DotLms.Services.Providers.Contracts;
using DotLms.Web.Areas.Backoffice.Controllers;
using DotLms.Web.Attributes;
using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;

namespace DotLms.Web.Tests.Controllers.Backoffice.BackOfficeCourseControllerUnitTests
{
    [TestFixture]
    [Category(Common.TestConstants.UnitTestCategory)]
    public class CreateCourseHttpGetTests
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
        public void CreateCourseHttpGet_ShouldHaveHttpGetAttribute()
        {
            // Arrange, Act
            MethodInfo methodInfo = typeof(BackOfficeCourseController).GetMethod("CreateCourse", new Type[] { });
            bool httpGetIsDefinded = Attribute.IsDefined(methodInfo, typeof(HttpGetAttribute));
            
            // Assert
            Assert.IsTrue(httpGetIsDefinded);
        }

        [Test]
        public void CreateCourseHttpGet_ShouldHaveBackofficeAuthorizationAttribute()
        {
            // Arrange, Act
            MethodInfo methodInfo = typeof(BackOfficeCourseController).GetMethod("CreateCourse", new Type[] { });
            bool backofficeAuthorizatuonAttributeIsDefined = Attribute.IsDefined(
                methodInfo,
                typeof(BackofficeAuthorizatuonAttribute));

            // Assert
            Assert.IsTrue(backofficeAuthorizatuonAttributeIsDefined);
        }

        [Test]
        public void CreateCourseHttpGet_ShouldShouldRenderDefaultView()
        {
            // Arrange
            BackOfficeCourseController controller = this.GetController();

            // Act and Assert
            controller.WithCallTo(x => x.CreateCourse()).ShouldRenderDefaultView();
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