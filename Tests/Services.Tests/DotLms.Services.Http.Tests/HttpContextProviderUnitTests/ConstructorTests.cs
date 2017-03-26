using System;
using System.Web;
using Moq;
using NUnit.Framework;

namespace DotLms.Services.Http.Tests.HttpContextProviderUnitTests
{
    [TestFixture]
    [Category(Common.TestConstants.UnitTestCategory)]
    public class ConstructorTests
    {
        [Test]
        public void Constructor_ShouldThrowArgumentNullException_WhenHttpContextBaseIsNull()
        {
            // Arrange, Act & Assert
            Assert.Throws<ArgumentNullException>(() => new HttpContextProvider(null));
        }

        [Test]
        public void Constructor_ShouldNotThrow_WhenHttpContextBaseIsNotNull()
        {
            // Arrange
            Mock<HttpContextBase> mockedHttpContextBase = new Mock<HttpContextBase>();

            // Act & Assert
            Assert.DoesNotThrow(() => new HttpContextProvider(mockedHttpContextBase.Object));
        }
    }
}