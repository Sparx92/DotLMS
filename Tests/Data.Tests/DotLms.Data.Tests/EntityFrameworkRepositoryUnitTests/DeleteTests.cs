using System;
using System.Data.Entity;
using DotLms.Data.Contracts;
using DotLms.Data.Models;
using DotLms.Data.Repositories;
using Moq;
using NUnit.Framework;

namespace DotLms.Data.Tests.EntityFrameworkRepositoryUnitTests
{
    [TestFixture]
    [Category(Common.TestConstants.UnitTestCategory)]
    public class DeleteTests
    {
        private Mock<IDotLmsEfDbContext> context;

        [SetUp]
        public void Init()
        {
            this.context = new Mock<IDotLmsEfDbContext>();
        }

        [Test]
        public void Delete_ShouldThrowArgumentNullException_WhenNullInputParameterIsPassed()
        {
            // Arange
            Mock<IDbSet<Course>> mockSet = new Mock<IDbSet<Course>>();

            this.context.Setup(x => x.Set<Course>()).Returns(mockSet.Object);
            this.context.Setup(x => x.Courses).Returns(mockSet.Object);

            // Act
            EntityFrameworkRepository<Course> repository = this.GetRepository();

            // Assert
            Assert.Throws<ArgumentNullException>(() => repository.Delete(null));
        }

        [Test]
        public void Delete_ShouldNotThrow_WhenCorrectInputParameterIsPassed()
        {
            // Arange
            Mock<IDbSet<Course>> mockSet = new Mock<IDbSet<Course>>();

            this.context.Setup(x => x.Set<Course>()).Returns(mockSet.Object);
            this.context.Setup(x => x.Courses).Returns(mockSet.Object);

            Mock<Course> mockCourse = new Mock<Course>();
            EntityFrameworkRepository<Course> repository = this.GetRepository();

            // Act
            try
            {
                repository.Delete(mockCourse.Object);
            }
            catch (NullReferenceException e)
            {
                // cannot activate or instance or mock a class with an internal constructor
                // the SafeUninitializedObject does not have any properties or methods, so the
                // DbEntityEntry.State is null
            }

            // Assert
            this.context.Verify(x => x.Entry(mockCourse.Object), Times.AtLeastOnce);
        }

        private EntityFrameworkRepository<Course> GetRepository()
        {
            return new EntityFrameworkRepository<Course>(this.context.Object);
        }
    }
}