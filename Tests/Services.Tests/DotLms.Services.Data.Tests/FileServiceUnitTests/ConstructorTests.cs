using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotLms.Common;
using DotLms.Data.Contracts;
using DotLms.Data.Models;
using DotLms.Services.Http.Contracts;
using DotLms.Services.Providers.Contracts;
using Moq;
using NUnit.Framework;

namespace DotLms.Services.Data.Tests.FileServiceUnitTests
{
    [TestFixture]
    [Category(TestConstants.UnitTestCategory)]
    public class ConstructorTests
    {
        private Mock<IDotLmsEfData> mockedDotLmsEfData;
        private Mock<IEntityFrameworkRepository<MediaItem>> mockedMediaItemEfRepository;
        private Mock<IMapperProvider> mockedMapperProvider;
        private Mock<IHttpContextProvider> mockedHttpContextProvider;

        [SetUp]
        public void Init()
        {
            this.mockedDotLmsEfData = new Mock<IDotLmsEfData>();
            this.mockedMapperProvider = new Mock<IMapperProvider>();
            this.mockedMediaItemEfRepository = new Mock<IEntityFrameworkRepository<MediaItem>>();
            this.mockedHttpContextProvider = new Mock<IHttpContextProvider>();
        }

        [Test]
        public void Constructor_ShouldThrowArgumentNullException_WhenAllParametersAreNull()
        {
            // Arrange, Act & Assert
            Assert.Throws<ArgumentNullException>(() => new FileService(null, null, null,null));
        }

        [Test]
        public void Constructor_ShouldThrowArgumentNullException_WhenMediaItemEfRepositoryIsNull()
        {
            // Arrange, Act & Assert
            Assert.Throws<ArgumentNullException>(() => new FileService(null, this.mockedDotLmsEfData.Object, this.mockedMapperProvider.Object, this.mockedHttpContextProvider.Object));
        }

        [Test]
        public void Constructor_ShouldThrowArgumentNullException_WhenDotLmsEfDataIsNull()
        {
            // Arrange, Act & Assert
            Assert.Throws<ArgumentNullException>(() => new FileService(this.mockedMediaItemEfRepository.Object,null, this.mockedMapperProvider.Object, this.mockedHttpContextProvider.Object));
        }

        [Test]
        public void Constructor_ShouldThrowArgumentNullException_WhenMapperProviderIsNull()
        {
            // Arrange, Act & Assert
            Assert.Throws<ArgumentNullException>(() => new FileService(this.mockedMediaItemEfRepository.Object, this.mockedDotLmsEfData.Object, null, this.mockedHttpContextProvider.Object));
        }

        [Test]
        public void Constructor_ShouldThrowArgumentNullException_WhenHttpContextProviderIsNull()
        {
            // Arrange, Act & Assert
            Assert.Throws<ArgumentNullException>(() => new FileService(this.mockedMediaItemEfRepository.Object, this.mockedDotLmsEfData.Object,this.mockedMapperProvider.Object, null));
        }

        [Test]
        public void Constructor_ShouldNotThrow_WhenAllArgumentsArePassed()
        {
            // Arrange, Act & Assert
            Assert.DoesNotThrow(() =>
            {
                FileService service = this.GetFileService();
            });
        }

        private FileService GetFileService()
        {
            return new FileService(
                this.mockedMediaItemEfRepository.Object,
                this.mockedDotLmsEfData.Object,
                this.mockedMapperProvider.Object,
                this.mockedHttpContextProvider.Object);
        }
    }
}
