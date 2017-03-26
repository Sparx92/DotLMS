using System;
using DotLms.Data.Contracts;
using DotLms.Data.Models;
using DotLms.Services.Providers.Contracts;
using Moq;
using NUnit.Framework;

namespace DotLms.Services.Data.Tests.PageRetrivalServiceUnitTests
{
    [TestFixture]
    [Category(Common.TestConstants.UnitTestCategory)]
    public class CounstrctorTests
    {
        private Mock<IProjectableRepository<Page>> mockedPageProjectableRepository;
        private Mock<IMapperProvider> mockedMapperProvider;

        [SetUp]
        public void Init()
        {
            this.mockedPageProjectableRepository = new Mock<IProjectableRepository<Page>>();
            this.mockedMapperProvider = new Mock<IMapperProvider>();
        }

        [Test]
        public void Constructor_ShouldThrowArgumentNullException_WhenAllParametersAreNull()
        {
            // Arrange, Act & Assert
            Assert.Throws<ArgumentNullException>(() => new PageRetrivalService(null, null));
        }

        [Test]
        public void Constructor_ShouldThrowArgumentNullException_WhenPageProjectableRepositoryIsNull()
        {
            // Arrange, Act & Assert
            Assert.Throws<ArgumentNullException>(() => new PageRetrivalService(null, this.mockedMapperProvider.Object));
        }

        [Test]
        public void Constructor_ShouldThrowArgumentNullException_WhenMapperProviderIsNull()
        {
            // Arrange, Act & Assert
            Assert.Throws<ArgumentNullException>(() => new PageRetrivalService(this.mockedPageProjectableRepository.Object, null));
        }


        [Test]
        public void Constructor_ShouldNotThrow_WhenAllArgumentsArePassed()
        {
            // Arrange, Act & Assert
            Assert.DoesNotThrow(() =>
            {
                new PageRetrivalService(
                    this.mockedPageProjectableRepository.Object,
                    this.mockedMapperProvider.Object
                );
            });
        }
    }
}