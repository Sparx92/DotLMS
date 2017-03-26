using System;
using System.Collections.Generic;
using AutoMapper;
using DotLms.Data.Contracts;
using DotLms.Data.Models;
using DotLms.Services.Providers.Contracts;
using DotLms.Web.Models;
using Moq;
using NUnit.Framework;

namespace DotLms.Services.Data.Tests.PageRetrivalServiceUnitTests
{
    [TestFixture]
    [Category(Common.TestConstants.UnitTestCategory)]
    public class GetPageStringOverloadTests
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
        public void GetPageStringOverload_ShouldThrowArgumentNullException_WhenPageNameIsNull()
        {
            // Arrange
            PageRetrivalService service = this.GetService();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => service.GetPage(pageName: null));
        }

        [Test]
        public void GetPageStringOverload_ShouldThrowArgumentException_WhenPageNameIsEmptyString()
        {
            // Arrange
            PageRetrivalService service = this.GetService();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => service.GetPage(pageName: string.Empty));
        }

        [Test]
        public void GetPageStringOverload_ShouldReturnPageViewModel()
        {
            // Arrange
            PageRetrivalService service = this.GetService();

            // Act
            PageViewModel result = service.GetPage("test");

            // Assert
            Assert.AreEqual(typeof(PageViewModel),result.GetType());
        }

        [Test]
        public void GetPageStringOverload_ShouldCallPageProjectableRepositoryAllOnce()
        {
            // Arrange
            PageRetrivalService service = this.GetService();

            // Act
            service.GetPage("test");

            // Assert 
            this.mockedPageProjectableRepository.Verify(x=>x.All,Times.Once);
        }

        [Test]
        public void GetPageStringOverload_ShouldCallMapperProviderInstanceOnce()
        {
            // Arrange
            PageRetrivalService service = this.GetService();

            // Act
            service.GetPage("test");

            // Assert 
            this.mockedMapperProvider.Verify(x => x.Instance, Times.Once);
        }

        [Test]
        public void GetPageStringOverload_ShouldCallMapperMapOnce()
        {
            // Arrange
            PageRetrivalService service = this.GetService();

            // Act
            service.GetPage("test");

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