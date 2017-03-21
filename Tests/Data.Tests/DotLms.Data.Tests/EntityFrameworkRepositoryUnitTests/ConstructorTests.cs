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
    [Category(Common.Constants.UnitTestCategory)]
    public class ConstructorTests
    {
        private Mock<IDotLmsEfDbContext> context;

        [SetUp]
        public void Init()
        {
            this.context = new Mock<IDotLmsEfDbContext>();
        }

        [Test]
        public void Constructor_ShouldThrowArgumentNullException_WhenContextIsNull()
        {
            // Arrange & Act & Assert
            Assert.Throws<ArgumentNullException>(() => new EntityFrameworkRepository<Course>(null));
        }

        [Test]
        public void Constructor_InitializesRepositoryContext()
        {
            // Arange
            Mock<IDotLmsEfDbContext> mockContext = new Mock<IDotLmsEfDbContext>();

            // Act
            EntityFrameworkRepository<Course> repository = this.GetRepository();

            // Assert  
            Assert.NotNull(repository.Context);
        }

        [Test]
        public void Constructor_InitializesRepositoryDbSet()
        {
            // Arange 
            IDbSet<Course> mockSet = new Mock<IDbSet<Course>>().Object;
            this.context.Setup(x => x.Set<Course>()).Returns(mockSet);

            // Act   
            EntityFrameworkRepository<Course> repository = this.GetRepository();

            // Assert
            Assert.NotNull(repository.DbSet);
        }

        [Test]
        public void Constructor_InitializesRepositoryDbSetWithCorrectType()
        {
            // Arange
            IDbSet<Course> mockSet = new Mock<IDbSet<Course>>().Object;
            this.context.Setup(x => x.Set<Course>()).Returns(mockSet);

            // Act
            EntityFrameworkRepository<Course> repository = this.GetRepository();

            // Assert
            Assert.NotNull(repository.DbSet);
            Assert.IsInstanceOf(typeof(IDbSet<Course>), repository.DbSet);
        }

        private EntityFrameworkRepository<Course> GetRepository()
        {
            return new EntityFrameworkRepository<Course>(this.context.Object);
        }
    }
}