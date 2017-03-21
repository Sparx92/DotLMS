using DotLms.Data.Contracts;
using Moq;
using NUnit.Framework;

namespace DotLms.Data.Tests.DotLmsEfDataUnitTests
{
    [TestFixture]
    [Category(Common.Constants.UnitTestCategory)]
    public class SaveChangesTests
    {
        private Mock<IDotLmsEfDbContext> dotLmsEfDbContext;

        [SetUp]
        public void Init()
        {
            this.dotLmsEfDbContext = new Mock<IDotLmsEfDbContext>();
        }

        [Test]
        public void SaveChanges_ShouldCallDbContextsSaveChanges()
        {
            // Arange 
            DotLmsEfData dotLmsEfData = new DotLmsEfData(dotLmsEfDbContext.Object);

            // Act
            dotLmsEfData.SaveChanges();

            // Assert
            this.dotLmsEfDbContext.Verify(x => x.SaveChanges(), Times.Once);
        }

        [Test]
        public void SaveChanges_ShouldReturnInt()
        {
            // Arange 
            DotLmsEfData dotLmsEfData = new DotLmsEfData(dotLmsEfDbContext.Object);

            // Act
            int result = dotLmsEfData.SaveChanges();

            // Assert
            this.dotLmsEfDbContext.Verify(x => x.SaveChanges(), Times.Once);
            Assert.AreEqual(typeof(int),result.GetType());
        }
    }
}