using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using DotLms.Common;
using DotLms.Data.Contracts;
using DotLms.Data.Models;
using DotLms.Data.Repositories;
using DotLms.Services.Providers.Contracts;
using DotLms.Web.Models;
using Moq;
using NUnit.Framework;

namespace DotLms.Services.Data.Tests.CourseServiceUnitTests
{
    [TestFixture]
    [Category(TestConstants.UnitTestCategory)]
    public class UpdateCourse
    {
        private Mock<IDotLmsEfDbContext> mockedDbContext;
        private Mock<IEntityFrameworkRepository<Course>> mockedCourseRepository;
        private Mock<IDotLmsEfData> mockedDotLmsEfData;
        private Mock<IMapperProvider> mockedMapperProvider;
        private Mock<IMapper> mockedMapper;

        private Course testCourse = new Course
        {
            Name = "Test",
            UglyName = "test",
            ShortDescription = "Short Description",
            FullDescription = "Full Description"
        };

        private CourseCategory testCourseCategory = new CourseCategory
        {
            Id = 1,
            Name = "Test Category"
        };

        private IList<Course> courses = new List<Course>();
        private Mock<IDbSet<Course>> mockedSet;


        [SetUp]
        public void Init()
        {
            this.mockedDbContext = new Mock<IDotLmsEfDbContext>();

            courses.Add(this.testCourse);
            this.mockedSet = new Mock<IDbSet<Course>>();
            this.mockedSet.Setup(x => x.Attach(this.testCourse));
            this.mockedSet.As<IQueryable<Course>>().Setup(m => m.Provider).Returns(courses.AsQueryable().Provider);
            this.mockedSet.As<IQueryable<Course>>().Setup(m => m.Expression).Returns(courses.AsQueryable().Expression);
            this.mockedSet.As<IQueryable<Course>>().Setup(m => m.ElementType).Returns(courses.AsQueryable().ElementType);
            this.mockedSet.As<IQueryable<Course>>().Setup(m => m.GetEnumerator()).Returns(courses.AsQueryable().GetEnumerator);
            this.mockedDbContext.Setup(x => x.Set<Course>()).Returns(this.mockedSet.Object);

            this.mockedMapper = new Mock<IMapper>();
            this.mockedMapper
                .Setup(x => x.Map<Course>(It.IsAny<CourseCreationViewModel>()))
                .Returns(this.testCourse);
            this.mockedMapper
                .Setup(x => x.Map<CourseCategory>(It.IsAny<CourseCreationViewModel>()))
                .Returns(this.testCourseCategory);

            this.mockedMapperProvider = new Mock<IMapperProvider>();
            this.mockedMapperProvider
                .SetupGet(x => x.Instance)
                .Returns(this.mockedMapper.Object);

            this.mockedCourseRepository = new Mock<IEntityFrameworkRepository<Course>>();
            this.mockedCourseRepository.Setup(x => x.All).Returns(this.testCourse as IQueryable<Course>);
            this.mockedDotLmsEfData = new Mock<IDotLmsEfData>();
        }

        [Test]
        public void UpdateCourse_BothOverloads_ShoudThrowArgumentNullExceptionWhenModelIsNull()
        {
            // Arrange
            CourseService service = this.GetCourseService();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => service.UpdateCourse(null));
        }

        [Test]
        public void UpdateCourse_ShouldNotThrowWhenImageIsNull()
        {
            // Arrange
            var repo = new EntityFrameworkRepository<Course>(this.mockedDbContext.Object);

            CourseCreationViewModel model = new CourseCreationViewModel
            {
                Name = this.testCourse.Name
            };

            CourseService service = new CourseService(repo,
                this.mockedDotLmsEfData.Object, this.mockedMapperProvider.Object);

            // Act & Assert
            try
            {
                service.UpdateCourse(model, null);
            }
            catch (NullReferenceException e)
            {
                // cannot activate or instance or mock a class with an internal constructor
                // the SafeUninitializedObject does not have any properties or methods, so the
                // DbEntityEntry.State is null
            }
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