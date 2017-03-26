using System.Web;
using Moq;
using NUnit.Framework;

namespace DotLms.Services.Http.Tests.HttpContextProviderUnitTests
{
    [TestFixture]
    [Category(Common.TestConstants.UnitTestCategory)]
    public class HttpContextGetterTests
    {
        [Test]
        public void HttpContextGetter_ShouldReturnHttpContextBaseSetByConstructor()
        {
            // Arrange
            Mock<HttpContextBase> mockedHttpContextBase = new Mock<HttpContextBase>();

            HttpContextProvider provider = new HttpContextProvider(mockedHttpContextBase.Object);

            // Act & Assert
            Assert.AreSame(mockedHttpContextBase.Object, provider.HttpContext);
        }
    }
}