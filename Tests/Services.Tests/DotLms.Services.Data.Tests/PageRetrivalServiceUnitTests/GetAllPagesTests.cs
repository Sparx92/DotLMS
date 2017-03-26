using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DotLms.Data.Contracts;
using DotLms.Data.Models;
using DotLms.Services.Providers.Contracts;
using DotLms.Web.Models;
using DotLms.Web.Models.Backoffice;
using Moq;
using NUnit.Framework;

namespace DotLms.Services.Data.Tests.PageRetrivalServiceUnitTests
{
    [TestFixture]
    [Category(Common.TestConstants.UnitTestCategory)]
    public class GetAllPagesTests
    {
        private Mock<IProjectableRepository<Page>> mockedPageProjectableRepository;
        private Mock<IMapperProvider> mockedMapperProvider;
        private Mock<IMapper> mockedMapper;

        private IEnumerable<PageViewModel> pageViewModels;

        [SetUp]
        public void Init()
        {
            pageViewModels = new List<PageViewModel>();

            this.mockedPageProjectableRepository = new Mock<IProjectableRepository<Page>>();

            this.mockedMapper = new Mock<IMapper>();
            this.mockedMapper
                .Setup(x => x.Map<IEnumerable<PageViewModel>>(It.IsAny<IEnumerable<Page>>()))
                .Returns(pageViewModels);

            this.mockedMapperProvider = new Mock<IMapperProvider>();
            this.mockedMapperProvider.Setup(x => x.Instance).Returns(this.mockedMapper.Object);
        }

        [Test]
        public void GetAllPages_ShouldReturnBackOfficeIndexViewModelHoldingAListOfPageViewModels()
        {
            // Arrange
            PageRetrivalService service = this.GetService();

            // Act
            BackOfficeIndexViewModel result = service.GetAllPages();

            // Assert
            Assert.AreEqual(typeof(BackOfficeIndexViewModel), result.GetType());
            Assert.AreSame(pageViewModels, result.Models);
        }

        [Test]
        public void GetAllPages_ShouldCallPageProjectableRepositoryAll()
        {
            // Arrange
            PageRetrivalService service = this.GetService();

            // Act
              service.GetAllPages();

            // Assert
            this.mockedPageProjectableRepository.Verify(x => x.All, Times.Once);
        }

        [Test]
        public void GetAllPages_ShouldCallMapperProviderInstanceOnce()
        {
            // Arrange
            PageRetrivalService service = this.GetService();

            // Act
            service.GetAllPages();

            // Assert
            this.mockedMapperProvider.Verify(x => x.Instance, Times.Once);
        }

        [Test]
        public void GetAllPages_ShouldCallMapperMapOnce()
        {
            // Arrange
            PageRetrivalService service = this.GetService();

            // Act
            service.GetAllPages();

            // Assert
            this.mockedMapper.Verify(x => x.Map<IEnumerable<PageViewModel>>(It.IsAny<IEnumerable<Page>>()), Times.Once);
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