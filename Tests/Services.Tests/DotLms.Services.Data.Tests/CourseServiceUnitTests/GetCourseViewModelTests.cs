using System;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using DotLms.Common;
using DotLms.Data.Contracts;
using DotLms.Data.Models;
using DotLms.Services.Providers.Contracts;
using DotLms.Web.Models;
using Moq;
using NUnit.Framework;

namespace DotLms.Services.Data.Tests.CourseServiceUnitTests
{
    [TestFixture]
    [Category(TestConstants.UnitTestCategory)]
    public class GetCourseViewModelTests
    {
        private Mock<IEntityFrameworkRepository<Course>> mockedCourseRepository;
        private Mock<IDotLmsEfData> mockedDotLmsEfData;
        private Mock<IMapperProvider> mockedMapperProvider;
        private Mock<IMapper> mockedMapper;


        [SetUp]
        public void Init()
        {
            this.mockedMapper = new Mock<IMapper>();
            this.mockedMapper
                .Setup(x => x.Map<CourseViewModel>(It.IsAny<Course>()))
                .Returns(new CourseViewModel());

            this.mockedMapperProvider = new Mock<IMapperProvider>();
            this.mockedMapperProvider
                .SetupGet(x => x.Instance)
                .Returns(this.mockedMapper.Object);

            this.mockedCourseRepository = new Mock<IEntityFrameworkRepository<Course>>();


            this.mockedDotLmsEfData = new Mock<IDotLmsEfData>();
        }

        [Test]
        public void GetCourseViewModel_ShouldThrowArgumentNullException_WhenNameParameterIsNull()
        {
            // Arrange
            CourseService service = this.GetCourseService();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => service.GetCourseViewModel(null));
        }

        [Test]
        public void GetCourseViewModel_ShouldThrowArgumentException_WhenNameParameterIsStringEmpty()
        {
            // Arrange
            CourseService service = this.GetCourseService();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => service.GetCourseViewModel(string.Empty));
        }

        [Test]
        public void GetCourseViewModel_ShouldNotThrow_WhenCorrectParameterIsPassed()
        {
            // Arrange
            CourseService service = this.GetCourseService();

            // Act & Assert
            Assert.DoesNotThrow(() => service.GetCourseViewModel("test"));
        }

        [Test]
        public void GetCourseViewModel_ShouldCallCourseRepositoryAllOnce()
        {
            // Arrange
            CourseService service = this.GetCourseService();

            // Act
            service.GetCourseViewModel("test");

            // Assert
            this.mockedCourseRepository.Verify(x=>x.All,Times.Once);
        }

        [Test]
        public void GetCourseViewModel_ShouldReturnTypeCourseViewModel()
        {
            // Arrange 
            CourseService service = GetCourseService();

            // Act 
            CourseViewModel result = service.GetCourseViewModel("test");

            Assert.AreEqual(result.GetType(), typeof(CourseViewModel));
        }

        private CourseService GetCourseService()
        {
            return new CourseService(
                this.mockedCourseRepository.Object,
                this.mockedDotLmsEfData.Object,
                this.mockedMapperProvider.Object);
        }
    }
}