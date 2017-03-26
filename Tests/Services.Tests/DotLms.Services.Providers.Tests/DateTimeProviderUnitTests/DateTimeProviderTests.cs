using System;
using NUnit.Framework;

namespace DotLms.Services.Providers.Tests.DateTimeProviderUnitTests
{
    [TestFixture]
    [Category(Common.TestConstants.UnitTestCategory)]
    public class DateTimeProviderTests
    {
        [Test]
        public void Now_ShouldReturnDateTimeType()
        {
            // Arrange
            DateTimeProvider dateTimeProvider = new DateTimeProvider();

            // Act 
            DateTime result = dateTimeProvider.UtcNow();

            // Assert
            Assert.AreEqual(typeof(DateTime), result.GetType());
        }

        [Test]
        public void Now_ShouldReturnDateTimeUTCNow()
        {
            // Arrange
            DateTimeProvider dateTimeProvider = new DateTimeProvider();

            // Act
            DateTime result = dateTimeProvider.UtcNow();

            // Assert
            Assert.AreEqual(typeof(DateTime), result.GetType());
            Assert.AreEqual(result.Year, DateTime.UtcNow.Year);
            Assert.AreEqual(result.Month, DateTime.UtcNow.Month);
            Assert.AreEqual(result.Day, DateTime.UtcNow.Day);
            Assert.AreEqual(result.Hour, DateTime.UtcNow.Hour);
            Assert.AreEqual(result.Minute, DateTime.UtcNow.Minute);
            Assert.AreEqual(result.Second, DateTime.UtcNow.Second);
        }
    }
}