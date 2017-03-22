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
    public class AllTests
    {
        private Mock<IDotLmsEfDbContext> context;

        [SetUp]
        public void Init()
        {
            this.context = new Mock<IDotLmsEfDbContext>();
        }
        
        [Test]
        public void All_ReturnsRepositoryDbSet()
        {
            // Arange
            IDbSet<Course> mockSet = new Mock<IDbSet<Course>>().Object;

            this.context.Setup(x => x.Set<Course>()).Returns(mockSet);

            // Act
            EntityFrameworkRepository<Course> repository = this.GetRepository();

            // Assert
            Assert.NotNull(repository.All);
            Assert.IsInstanceOf(typeof(IDbSet<Course>), repository.All);
            Assert.AreSame(repository.All, repository.DbSet);
        }

        private EntityFrameworkRepository<Course> GetRepository()
        {
            return new EntityFrameworkRepository<Course>(this.context.Object);
        }
    }
}