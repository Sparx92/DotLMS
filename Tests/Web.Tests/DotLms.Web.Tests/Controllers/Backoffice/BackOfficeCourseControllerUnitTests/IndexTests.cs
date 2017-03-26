using System;
using DotLms.Services.Data.Contracts;
using DotLms.Services.Providers.Contracts;
using DotLms.Web.Areas.Backoffice.Controllers;
using DotLms.Web.Attributes;
using Moq;
using NUnit.Framework;

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
        public void Index_Should()
        {
            
        }

        private BackOfficeCourseController GetController()
        {
            return new BackOfficeCourseController(
                    this.mockedCategoryService.Object,
                    this.mockedCourseService.Object,
                    this.mockedFileService.Object,
                    this.mockedMemoryCacheProvider.Object
                    );
        }
    }
}