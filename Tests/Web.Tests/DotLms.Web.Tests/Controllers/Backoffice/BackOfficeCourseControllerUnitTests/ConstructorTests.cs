using System;
using DotLms.Services.Data.Contracts;
using DotLms.Services.Providers.Contracts;
using DotLms.Web.Areas.Backoffice.Controllers;
using Moq;
using NUnit.Framework;

namespace DotLms.Web.Tests.Controllers.Backoffice.BackOfficeCourseControllerUnitTests
{
    [TestFixture]
    [Category(Common.TestConstants.UnitTestCategory)]
    public class ConstructorTests
    {
        private Mock<ICourseCategoryService> mockedCategoryService;
        private Mock<ICourseService> mockedCourseService;
        private Mock<IFileService> mockedFileService;

        [SetUp]
        public void Init()
        {
            this.mockedCategoryService = new Mock<ICourseCategoryService>();
            this.mockedCourseService = new Mock<ICourseService>();
            this.mockedFileService = new Mock<IFileService>();
        }

        [Test]
        public void Constructor_ShouldThrowArgumentNullException_WhenAllArgumentsAreNull()
        {
            // Arrangr, Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                new BackOfficeCourseController(null, null, null);
            });
        }

        [Test]
        public void Constructor_ShouldThrowArgumentNullException_WhenCategoryServiceIsNull()
        {
            // Arrangr, Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                new BackOfficeCourseController(
                    null,
                    this.mockedCourseService.Object,
                    this.mockedFileService.Object
                    );
            });
        }

        [Test]
        public void Constructor_ShouldThrowArgumentNullException_WhenCourseServiceIsNull()
        {
            // Arrangr, Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                new BackOfficeCourseController(
                    this.mockedCategoryService.Object,
                    null,
                    this.mockedFileService.Object
                    );
            });
        }

        [Test]
        public void Constructor_ShouldThrowArgumentNullException_WhenFileServiceIsNull()
        {
            // Arrangr, Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                new BackOfficeCourseController(
                    this.mockedCategoryService.Object,
                    this.mockedCourseService.Object,
                    null
                    );
            });
        }
        

        [Test]
        public void Constructor_ShouldNotThrow_WhenAllParametersAreNotNull()
        {
            // Arrangr, Act & Assert
            Assert.DoesNotThrow(() =>
            {
                new BackOfficeCourseController(
                    this.mockedCategoryService.Object,
                    this.mockedCourseService.Object,
                    this.mockedFileService.Object
                    );
            });
        }
    }
}