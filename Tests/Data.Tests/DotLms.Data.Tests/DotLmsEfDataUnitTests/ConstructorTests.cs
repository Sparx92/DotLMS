using System;
using DotLms.Data.Contracts;
using Moq;
using NUnit.Framework;

namespace DotLms.Data.Tests.DotLmsEfDataUnitTests
{
    

    [TestFixture]
    [Category(Common.TestConstants.UnitTestCategory)]
    public class ConstructorTests
    {
        private Mock<IDotLmsEfDbContext> dotLmsEfDbContext;

        [SetUp]
        public void Init()
        {
            this.dotLmsEfDbContext = new Mock<IDotLmsEfDbContext>();
        }

        [Test]
        public void Constructor_ShouldThrowArgumentNullException_WhenIDotLmsEfDbContextIsNull()
        {
            // Arrange & Act & Assert
            Assert.Throws<ArgumentNullException>(() => new DotLmsEfData(null));
        }

        [Test]
        public void Constructor_ShouldNotThrow_WhenIDotLmsEfDbContextIsNotNull()
        {
            // Arrange & Act & Assert
            Assert.DoesNotThrow(() => new DotLmsEfData(this.dotLmsEfDbContext.Object));
        }
    }
}