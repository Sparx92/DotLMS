using System.Runtime.Caching;
using NUnit.Framework;

namespace DotLms.Services.Providers.Tests.MemoryCacheProviderUnitTests
{
    [TestFixture]
    [Category(Common.TestConstants.UnitTestCategory)]
    public class MemoryCacheGetterTests
    {
        [Test]
        public void MemoryCacheGetter_ShouldReturnValueSetByConstructor()
        {
            // Arrange
            MemoryCache memoryCache = new MemoryCache("test");
            MemoryCacheProvider memoryCacheProvider = new MemoryCacheProvider(memoryCache);

            // Act & Assert
            Assert.AreSame(memoryCache, memoryCacheProvider.MemoryCache);
        }
    }
}