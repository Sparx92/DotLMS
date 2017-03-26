using System;
using System.Runtime.Caching;
using Moq;
using NUnit.Framework;

namespace DotLms.Services.Providers.Tests.MemoryCacheProviderUnitTests
{
    [TestFixture]
    [Category(Common.TestConstants.UnitTestCategory)]
    public class Constructor
    {
        [Test]
        public void Constructor_ShouldThrowArgumentNullException_WhenMemorCacheParamIsNull()
        {
            // Arrange, Act & Assert
            Assert.Throws<ArgumentNullException>(() => new MemoryCacheProvider(null));
        }

        [Test]
        public void Constructor_ShouldNotThrow_WhenMemorCacheParamIsNotNull()
        {
            // Arrange
            MemoryCache mempryCache = new MemoryCache("test");

            // Act & Assert
            Assert.DoesNotThrow(() => new MemoryCacheProvider(mempryCache));
        }
    }
}