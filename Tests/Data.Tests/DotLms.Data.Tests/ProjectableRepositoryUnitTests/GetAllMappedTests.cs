using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DotLms.Data.Contracts;
using DotLms.Data.Models;
using DotLms.Data.Repositories;
using DotLms.Services.Common.Contracts;
using DotLms.Web.Models;
using Moq;
using NUnit.Framework;

namespace DotLms.Data.Tests.ProjectableRepositoryUnitTests
{
    [TestFixture]
    [Category(Common.Constants.UnitTestCategory)]
    public class GetAllMappedTests
    {
        private Mock<IDbSet<Course>> mockDbSet;
        private Mock<IDotLmsEfDbContext> mockNewsDbContext;
        private Mock<IProjectionService> mockProjectionService;

        [SetUp]
        public void Init()
        {
            IQueryable<Course> data = new List<Course>
            {
                new Course {ShortDescription = "asdasdasd", Id = 1, },
                new Course {ShortDescription = "as12312312d", Id = 2,},
                new Course {ShortDescription = "asaasd as das dd", Id = 3, },
                new Course {ShortDescription = "a123123sd", Id = 5}
            }.AsQueryable();

            this.mockDbSet = new Mock<IDbSet<Course>>();
            this.mockDbSet.As<IQueryable<Course>>().Setup(x => x.Provider).Returns(data.Provider);
            this.mockDbSet.As<IQueryable<Course>>().Setup(x => x.Expression).Returns(data.Expression);
            this.mockDbSet.As<IQueryable<Course>>().Setup(x => x.ElementType).Returns(data.ElementType);
            this.mockDbSet.As<IQueryable<Course>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator());

            this.mockNewsDbContext = new Mock<IDotLmsEfDbContext>();
            this.mockNewsDbContext.Setup(
                x => x.Set<Course>()).Returns(this.mockDbSet.Object);

            this.mockProjectionService = new Mock<IProjectionService>();
            this.mockProjectionService.Setup(
                x => x.ProjectToList<Course, CourseViewModel>(It.IsAny<IQueryable<Course>>()));
        }
       
        [Test]
        public void GetAllMapped_WithNoParameters_ShouldCallProjectionServiceProjectToListOnce()
        {
            // Arrange
            IProjectableRepository<Course> repository = new ProjectableRepository<Course>(
               this.mockNewsDbContext.Object, this.mockProjectionService.Object);

            IQueryable<Course> query = repository.All;

            // Act
            repository.GetAllMapped<CourseViewModel>();

            // Assert
            this.mockProjectionService.Verify(
                x => x.ProjectToList<Course, CourseViewModel>(query), Times.Once);
        }

        [Test]
        public void GetAllMapped_WithParameters_ShouldCallProjectionServiceProjectToListOnceWithCorrectQuery()
        {
            // Arrange
            IProjectableRepository<Course> repository = new ProjectableRepository<Course>(
               this.mockNewsDbContext.Object, this.mockProjectionService.Object);

            IQueryable<Course> query = repository.All.Where(x => x.Id == 1);

            // Act
            repository.GetAllMapped<CourseViewModel>(x => x.Id == 1);

            // Assert
            this.mockProjectionService.Verify(
                x => x.ProjectToList<Course, CourseViewModel>(query), Times.Once);
        }
    }
}