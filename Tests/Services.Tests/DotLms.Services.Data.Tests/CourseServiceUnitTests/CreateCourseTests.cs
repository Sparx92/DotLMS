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
    public class CreateCourseTests
    {
        private Mock<IEntityFrameworkRepository<Course>> mockedCourseRepository;
        private Mock<IDotLmsEfData> mockedDotLmsEfData;
        private Mock<IMapperProvider> mockedMapperProvider;
        private Mock<IMapper> mockedMapper;

        private readonly CourseCategory TestCourseCategory = new CourseCategory
        {
            Id = 1,
            Name = "Test Course Category"
        };

        private readonly Course TestCourse = new Course
        {
            Id = 1,
            Name = "Test Course Name"
        };

        private readonly MediaItemViewModel TestMediaItemViewModel = new MediaItemViewModel
        {
            Id = 1
        };

        [SetUp]
        public void Init()
        {
            this.mockedMapper = new Mock<IMapper>();
            this.mockedMapper
                .Setup(x => x.Map<CourseCategory>(It.IsAny<CourseCreationViewModel>()))
                .Returns(this.TestCourseCategory);
            this.mockedMapper
                .Setup(x => x.Map<Course>(It.IsAny<CourseCreationViewModel>()))
                .Returns(this.TestCourse);
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
        public void CreateCourse_ShouldThrowArgumentNullException_WhenAllParameterAreNull()
        {
            // Arrange
            CourseCreationViewModel model = new CourseCreationViewModel();
            CourseService service = this.GetCourseService();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                service.CreateCourse(null, null);
            });
        }

        [Test]
        public void CreateCourse_ShouldThrowArgumentNullException_WhenModelParameterIsNull()
        {
            // Arrange
            MediaItemViewModel image = new MediaItemViewModel();
            CourseService service = this.GetCourseService();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                service.CreateCourse(null, image);
            });
        }

        [Test]
        public void CreateCourse_ShouldThrowArgumentNullException_WhenImageParameterIsNull()
        {
            // Arrange
            CourseCreationViewModel model = new CourseCreationViewModel();
            CourseService service = this.GetCourseService();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                service.CreateCourse(model, null);
            });
        }

        [Test]
        public void CreateCourse_ShouldCall_courseEfRepository_Add_Once()
        {
            // Arrange
            CourseCreationViewModel model = new CourseCreationViewModel();
            MediaItemViewModel image = new MediaItemViewModel();
            CourseService service = GetCourseService();

            // Act
            service.CreateCourse(model, image);

            // Assert
            this.mockedCourseRepository.Verify(x => x.Add(It.IsAny<Course>()), Times.Once);
        }

        [Test]
        public void CreateCourse_ShouldCall_dotLmsEfData_SaveChanges_Once()
        {
            // Arrange
            CourseCreationViewModel model = new CourseCreationViewModel();
            MediaItemViewModel image = new MediaItemViewModel();
            CourseService service = GetCourseService();

            // Act
            service.CreateCourse(model, image);

            // Assert
            this.mockedCourseRepository.Verify(x => x.Add(It.IsAny<Course>()), Times.Once);
            this.mockedDotLmsEfData.Verify(x => x.SaveChanges(), Times.Once);
        }

        [Test]
        public void CreateCourse_ShouldCall_mapperProvider_Instance_ThreeTimes()
        {
            // Arrange
            CourseCreationViewModel model = new CourseCreationViewModel();
            MediaItemViewModel image = new MediaItemViewModel();
            CourseService service = GetCourseService();

            // Act
            service.CreateCourse(model, image);

            // Assert
            this.mockedCourseRepository.Verify(x => x.Add(It.IsAny<Course>()), Times.Once);
            this.mockedDotLmsEfData.Verify(x => x.SaveChanges(), Times.Once);
            this.mockedMapperProvider.Verify(x => x.Instance, Times.Exactly(3));
        }

        [Test]
        public void CreateCourse_ShouldSetCourseCategory()
        {
            // Arrange
            CourseCreationViewModel model = new CourseCreationViewModel();
            MediaItemViewModel image = new MediaItemViewModel();
            CourseService service = this.GetCourseService();

            // Act
            service.CreateCourse(model, image);

            // Assert
            Assert.AreEqual(this.TestCourse.Category, this.TestCourseCategory);
        }

        [Test]
        public void CreateCourse_ShouldSetCourseUglyName()
        {
            // Arrange
            CourseCreationViewModel model = new CourseCreationViewModel();
            MediaItemViewModel image = new MediaItemViewModel();
            CourseService service = this.GetCourseService();

            // Act
            service.CreateCourse(model, image);

            // Assert
            Assert.AreEqual(this.TestCourse.UglyName, "test-course-name");
        }

        [Test]
        public void CreateCourse_ShouldSetCourseUrl()
        {
            // Arrange
            CourseCreationViewModel model = new CourseCreationViewModel();
            MediaItemViewModel image = new MediaItemViewModel { Id = 5 };
            CourseService service = this.GetCourseService();

            // Act
            service.CreateCourse(model, image);

            // Assert
            Assert.AreEqual(this.TestCourse.Url, "/test-course-name");
        }

        public void CreateCourse_ShouldSetCourseMainImageId()
        {
            // Arrange
            CourseCreationViewModel model = new CourseCreationViewModel();
            MediaItemViewModel image = new MediaItemViewModel { Id = 5 };
            CourseService service = this.GetCourseService();

            // Act
            service.CreateCourse(model, image);

            // Assert
            Assert.AreEqual(this.TestCourse.MainImageId, image.Id);
        }

        public void CreateCourse_ShouldSetCourseCategoryId()
        {
            // Arrange
            CourseCreationViewModel model = new CourseCreationViewModel();
            MediaItemViewModel image = new MediaItemViewModel { Id = 5 };
            CourseService service = this.GetCourseService();

            // Act
            service.CreateCourse(model, image);

            // Assert
            Assert.AreEqual(this.TestCourse.CategoryId, this.TestCourseCategory.Id);
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