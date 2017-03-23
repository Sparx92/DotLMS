using System;
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
    public class GetCourseCreationViewModelTests
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
                .Setup(x => x.Map<CourseCreationViewModel>(It.IsAny<Course>()))
                .Returns(new CourseCreationViewModel());

            this.mockedMapperProvider = new Mock<IMapperProvider>();
            this.mockedMapperProvider
                .SetupGet(x => x.Instance)
                .Returns(this.mockedMapper.Object);

            this.mockedCourseRepository = new Mock<IEntityFrameworkRepository<Course>>();


            this.mockedDotLmsEfData = new Mock<IDotLmsEfData>();
        }

        [Test]
        public void GetCourseCreationViewModel_ShouldThrowArgumentNullException_WhenNameParameterIsNull()
        {
            // Arrange
            CourseService service = this.GetCourseService();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => service.GetCourseCreationViewModel(null));
        }

        [Test]
        public void GetCourseCreationViewModel_ShouldThrowArgumentException_WhenNameParameterIsStringEmpty()
        {
            // Arrange
            CourseService service = this.GetCourseService();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => service.GetCourseCreationViewModel(string.Empty));
        }

        [Test]
        public void GetCourseCreationViewModel_ShouldNotThrow_WhenCorrectParameterIsPassed()
        {
            // Arrange
            CourseService service = this.GetCourseService();

            // Act & Assert
            Assert.DoesNotThrow(() => service.GetCourseCreationViewModel("test"));
        }

        [Test]
        public void GetCourseCreationViewModel_ShouldCallCourseRepositoryAllOnce()
        {
            // Arrange
            CourseService service = this.GetCourseService();

            // Act
            service.GetCourseCreationViewModel("test");

            // Assert
            this.mockedCourseRepository.Verify(x => x.All, Times.Once);
        }

        [Test]
        public void GetCourseCreationViewModel_ShouldCallMapperProviderInstanceOnce()
        {
            // Arrange
            CourseService service = this.GetCourseService();

            // Act
            service.GetCourseCreationViewModel("test");

            // Assert
            this.mockedMapperProvider.Verify(x => x.Instance, Times.Once);
        }

        [Test]
        public void GetCourseCreationViewModel_ShouldReturnTypeCourseCreationViewModel()
        {
            // Arrange 
            CourseService service = GetCourseService();

            // Act 
            CourseCreationViewModel result = service.GetCourseCreationViewModel("test");

            // Assert
            Assert.AreEqual(result.GetType(), typeof(CourseCreationViewModel));
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