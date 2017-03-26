using DotLms.Data.Contracts;
using DotLms.Data.Models;
using DotLms.Services.Providers.Contracts;
using DotLms.Web.Models;
using Moq;
using NUnit.Framework;

namespace DotLms.Services.Data.Tests.PageCreationServiceUnitTests
{
    [TestFixture]
    [Category(Common.TestConstants.UnitTestCategory)]
    public class GetAllPagesTests
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
        public void GetAllPages_ShouldCallProjectableRepository_GenericFromPageViewModel_GetAllMappedOnce()
        {
            // Arrange
            PageCreationService service = this.GetService();

            // Act
            service.GetAllPages();

            // Assert
            this.mockedPageProjectableRepository.Verify(x=>x.GetAllMapped<PageViewModel>(),Times.Once);
        }

        private PageCreationService GetService()
        {
            return new PageCreationService(
                    this.mockedDotLmsEfData.Object,
                    this.mockedPageProjectableRepository.Object,
                    this.mockedDateTimeProvider.Object,
                    this.mockedMapperProvider.Object,
                    this.mockedUserRepository.Object
                );
        }
    }
}