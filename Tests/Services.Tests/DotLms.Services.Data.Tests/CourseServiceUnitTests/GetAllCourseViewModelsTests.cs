using System.Collections.Generic;
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
    public class GetAllCourseViewModelsTests
    {
        private Mock<IEntityFrameworkRepository<Course>> mockedCourseRepository;
        private Mock<IDotLmsEfData> mockedDotLmsEfData;
        private Mock<IMapperProvider> mockedMapperProvider;
        private Mock<IMapper> mockedMapper;

        private IEnumerable<CourseViewModel> courseViewModels = new List<CourseViewModel>();

        [SetUp]
        public void Init()
        {
            this.mockedMapper = new Mock<IMapper>();
            this.mockedMapper
                .Setup(x => x.Map<IEnumerable<CourseViewModel>>(It.IsAny<IEnumerable<Course>>()))
                .Returns(courseViewModels);

            this.mockedMapperProvider = new Mock<IMapperProvider>();
            this.mockedMapperProvider
                .SetupGet(x => x.Instance)
                .Returns(this.mockedMapper.Object);

            this.mockedCourseRepository = new Mock<IEntityFrameworkRepository<Course>>();


            this.mockedDotLmsEfData = new Mock<IDotLmsEfData>();
        }

        [Test]
        public void GetAllCourseViewModels_ShouldCallCourseRepositoryAllOnce()
        {
            // Arrange
            CourseService service = this.GetCourseService();

            // Act
            service.GetAllCourseViewModels();

            // Assert
            this.mockedCourseRepository.Verify(x => x.All, Times.Once);
        }

        [Test]
        public void GetAllCourseViewModels_ShouldCallMapperProviderInstanceOnce()
        {
            // Arrange
            CourseService service = this.GetCourseService();

            // Act
            service.GetAllCourseViewModels();

            // Assert
            this.mockedMapperProvider.Verify(x => x.Instance, Times.Once);
        }

        [Test]
        public void GetAllCourseViewModels_ShouldReturnTypeCourseViewModel()
        {
            // Arrange 
            CourseService service = GetCourseService();

            // Act 
            IEnumerable<CourseViewModel> result = service.GetAllCourseViewModels();

            // Assert
            Assert.AreEqual(result,courseViewModels);

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