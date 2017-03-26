using System;
using AutoMapper;
using DotLms.Data.Contracts;
using DotLms.Data.Models;
using DotLms.Services.Providers.Contracts;
using DotLms.Web.Models;
using Moq;
using NUnit.Framework;

namespace DotLms.Services.Data.Tests.PageRetrivalServiceUnitTests
{
    public class GetPageIntOverload
    {
        private Mock<IProjectableRepository<Page>> mockedPageProjectableRepository;
        private Mock<IMapperProvider> mockedMapperProvider;
        private Mock<IMapper> mockedMapper;

        private PageViewModel pageViewModel;

        [SetUp]
        public void Init()
        {
            this.pageViewModel = new PageViewModel();

            this.mockedPageProjectableRepository = new Mock<IProjectableRepository<Page>>();

            this.mockedMapper = new Mock<IMapper>();
            this.mockedMapper
                .Setup(x => x.Map<PageViewModel>(It.IsAny<Page>()))
                .Returns(this.pageViewModel);

            this.mockedMapperProvider = new Mock<IMapperProvider>();
            this.mockedMapperProvider.Setup(x => x.Instance).Returns(this.mockedMapper.Object);
        }

        [Test]
        public void GetPageIntOverload_ShouldThrowArgumenException_WhenPageIdIsLessThanOrEqualTo0()
        {
            // Arrange
            PageRetrivalService service = this.GetService();

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => service.GetPage(-1));
        }

        [Test]
        public void GetPageIntOverload_ShouldReturnPageViewModel()
        {
            // Arrange
            PageRetrivalService service = this.GetService();

            // Act
            PageViewModel result = service.GetPage(1);

            // Assert
            Assert.AreEqual(typeof(PageViewModel), result.GetType());
        }

        [Test]
        public void GetPageIntOverload_ShouldCallPageProjectableRepositoryAllOnce()
        {
            // Arrange
            PageRetrivalService service = this.GetService();

            // Act
            service.GetPage(1);

            // Assert 
            this.mockedPageProjectableRepository.Verify(x => x.All, Times.Once);
        }

        [Test]
        public void GetPageIntOverload_ShouldCallMapperProviderInstanceOnce()
        {
            // Arrange
            PageRetrivalService service = this.GetService();

            // Act
            service.GetPage(1);

            // Assert 
            this.mockedMapperProvider.Verify(x => x.Instance, Times.Once);
        }

        [Test]
        public void GetPageIntOverload_ShouldCallMapperMapOnce()
        {
            // Arrange
            PageRetrivalService service = this.GetService();

            // Act
            service.GetPage(1);

            // Assert 
            this.mockedMapper.Verify(x => x.Map<PageViewModel>(It.IsAny<Page>()), Times.Once);
        }

        private PageRetrivalService GetService()
        {
            return new PageRetrivalService(
                this.mockedPageProjectableRepository.Object,
                this.mockedMapperProvider.Object
                );
        }
    }
}