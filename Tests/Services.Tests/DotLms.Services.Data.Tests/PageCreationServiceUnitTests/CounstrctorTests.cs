using System;
using DotLms.Data.Contracts;
using DotLms.Data.Models;
using DotLms.Services.Providers.Contracts;
using Moq;
using NUnit.Framework;

namespace DotLms.Services.Data.Tests.PageCreationServiceUnitTests
{
    [TestFixture]
    [Category(Common.TestConstants.UnitTestCategory)]
    public class CounstrctorTests
    {
        private Mock<IDotLmsEfData> mockedDotLmsEfData;
        private Mock<IProjectableRepository<Page>> mockedPageProjectableRepository;
        private Mock<IEntityFrameworkRepository<User>> mockedUserRepository;
        private Mock<IDateTimeProvider> mockedDateTimeProvider;
        private Mock<IMapperProvider> mockedMapperProvider;

        [SetUp]
        public void Init()
        {
            this.mockedDotLmsEfData = new Mock<IDotLmsEfData>();
            this.mockedPageProjectableRepository = new Mock<IProjectableRepository<Page>>();
            this.mockedUserRepository = new Mock<IEntityFrameworkRepository<User>>();
            this.mockedDateTimeProvider = new Mock<IDateTimeProvider>();
            this.mockedMapperProvider = new Mock<IMapperProvider>();
        }

        [Test]
        public void Constructor_ShouldThrowArgumentNullException_WhenAllParametersAreNull()
        {
            // Arrange, Act & Assert
            Assert.Throws<ArgumentNullException>(() => new PageCreationService(null, null, null, null, null));
        }

        [Test]
        public void Constructor_ShouldThrowArgumentNullException_WhenDotLmsEfDataIsNull()
        {
            // Arrange, Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                new PageCreationService(
                    null,
                    this.mockedPageProjectableRepository.Object,
                    this.mockedDateTimeProvider.Object,
                    this.mockedMapperProvider.Object,
                    this.mockedUserRepository.Object
                );
            });
        }

        [Test]
        public void Constructor_ShouldThrowArgumentNullException_WhenPageProjectableRepositoryIsNull()
        {
            // Arrange, Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                new PageCreationService(
                    this.mockedDotLmsEfData.Object,
                    null,
                    this.mockedDateTimeProvider.Object,
                    this.mockedMapperProvider.Object,
                    this.mockedUserRepository.Object
                );
            });
        }

        [Test]
        public void Constructor_ShouldThrowArgumentNullException_WhenDateTimeProviderIsNull()
        {
            // Arrange, Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                new PageCreationService(
                    this.mockedDotLmsEfData.Object,
                    this.mockedPageProjectableRepository.Object,
                    null,
                    this.mockedMapperProvider.Object,
                    this.mockedUserRepository.Object
                );
            });
        }

        [Test]
        public void Constructor_ShouldThrowArgumentNullException_WhenMapperProviderIsNull()
        {
            // Arrange, Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                new PageCreationService(
                    this.mockedDotLmsEfData.Object,
                    this.mockedPageProjectableRepository.Object,
                    this.mockedDateTimeProvider.Object,
                    null,
                    this.mockedUserRepository.Object
                );
            });
        }

        [Test]
        public void Constructor_ShouldThrowArgumentNullException_WhenUserRepositoryIsNull()
        {
            // Arrange, Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                new PageCreationService(
                    this.mockedDotLmsEfData.Object,
                    this.mockedPageProjectableRepository.Object,
                    this.mockedDateTimeProvider.Object,
                    this.mockedMapperProvider.Object,
                    null
                );
            });
        }

        [Test]
        public void Constructor_ShouldNotThrow_WhenAllArgumentsArePassed()
        {
            // Arrange, Act & Assert
            Assert.DoesNotThrow(() =>
            {
                new PageCreationService(
                    this.mockedDotLmsEfData.Object,
                    this.mockedPageProjectableRepository.Object,
                    this.mockedDateTimeProvider.Object,
                    this.mockedMapperProvider.Object,
                    this.mockedUserRepository.Object
                );
            });
        }
    }
}