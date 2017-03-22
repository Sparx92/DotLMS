using System.Reflection;
using DotLms.Web.Infrastructure.Mappings.Profiles;
using NUnit.Framework;

namespace DotLms.Data.Tests.EntityFrameworkRepositoryUnitTests
{
    [TestFixture]
    [Category(Common.TestConstants.UnitTestCategory)]
    public class DataMappingTests
    {
        [Test]
        public void DataMaping_Configure()
        {
            // Arange
            DataMappingsProfile profile = new DataMappingsProfile();

            // Act & Assert
            Assert.DoesNotThrow(() => profile
                .GetType()
                .GetMethod("Configure", BindingFlags.NonPublic | BindingFlags.Instance)
                .Invoke(profile, new object[] { }));
        }
    }
}