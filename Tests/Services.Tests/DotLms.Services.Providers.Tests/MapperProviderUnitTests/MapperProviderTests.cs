using AutoMapper;
using NUnit.Framework;

namespace DotLms.Services.Providers.Tests.MapperProviderUnitTests
{
    [TestFixture]
    [Category(Common.TestConstants.UnitTestCategory)]
    public class MapperProviderTests
    {
        [Test]
        public void Instance_ShouldBeOfTypeMapper()
        {
            // Arrange
            MapperProvider mapperProvider = new MapperProvider
            {
                Instance = new Mapper(new MapperConfiguration(delegate (IMapperConfigurationExpression expression) { }))
            };

            // Act
            IMapper mapperProviderInstance = mapperProvider.Instance;

            // Assert
            Assert.AreEqual(mapperProviderInstance.GetType(), typeof(Mapper));
        }

        [Test]
        public void Configuration_ShouldBeSet()
        {
            // Arrange
            MapperProvider mapperProvider = new MapperProvider();

            mapperProvider.Configuration = new MapperConfiguration(expression => {} );

            Assert.NotNull(mapperProvider.Configuration);
        }
    }
}
