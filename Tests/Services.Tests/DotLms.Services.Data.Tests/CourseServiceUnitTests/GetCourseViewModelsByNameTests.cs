using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
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

    public class GetCourseViewModelsByNameTests
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
        private IEnumerable<CourseViewModel> courseViewModels = new List<CourseViewModel>();
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
            this.mockedMapper
               .Setup(x => x.Map<IEnumerable<CourseViewModel>>(It.IsAny<IEnumerable<Course>>()))
               .Returns(courseViewModels);

            this.mockedMapperProvider = new Mock<IMapperProvider>();
            this.mockedMapperProvider
                .SetupGet(x => x.Instance)
                .Returns(this.mockedMapper.Object);

            this.mockedCourseRepository = new Mock<IEntityFrameworkRepository<Course>>();
            this.mockedCourseRepository.Setup(x => x.All).Returns(this.testCourse as IQueryable<Course>);
            this.mockedDotLmsEfData = new Mock<IDotLmsEfData>();
        }

        [Test]
        public void GetCourseViewModelsByName_SholdCallGetAllCourseViewModels_WhenQueryIsNullOrEmpty()
        {
            // Arrange
            EntityFrameworkRepository<Course> stubRepo = new EntityFrameworkRepository<Course>(this.mockedDbContext.Object);
            CourseService service = new CourseService(stubRepo,
                this.mockedDotLmsEfData.Object, this.mockedMapperProvider.Object);

            // Act && Assert
            Assert.DoesNotThrow(() =>
            {
                IEnumerable<CourseViewModel> result = service.GetCourseViewModelsByName("");

                Assert.AreEqual(typeof(List<CourseViewModel>), result.GetType());
            });
        }

        [Test]
        public void GetCourseViewModelsByName_ShouldCallDbContextSet_WhichIsEquvalentToIQuerableAllOfEfRepo_WhenQueryIsNotNullOrEmpty()
        {
            // Arrange
            EntityFrameworkRepository<Course> stubRepo = new EntityFrameworkRepository<Course>(this.mockedDbContext.Object);
            CourseService service = new CourseService(stubRepo,this.mockedDotLmsEfData.Object, this.mockedMapperProvider.Object);

            service.GetCourseViewModelsByName("teststring");

            mockedDbContext.Verify(x=>x.Set<Course>(), Times.Once);
        }
    }
}