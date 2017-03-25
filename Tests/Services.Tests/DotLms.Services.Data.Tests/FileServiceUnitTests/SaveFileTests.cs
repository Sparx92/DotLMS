using System;
using System.Collections.Generic;
using System.Web;
using AutoMapper;
using DotLms.Common;
using DotLms.Data.Contracts;
using DotLms.Data.Models;
using DotLms.Services.Http.Contracts;
using DotLms.Services.Providers.Contracts;
using DotLms.Web.Models;
using Moq;
using NUnit.Framework;

namespace DotLms.Services.Data.Tests.FileServiceUnitTests
{
    [TestFixture]
    [Category(TestConstants.UnitTestCategory)]
    public class SaveFileTests
    {
        private Mock<IDotLmsEfData> mockedDotLmsEfData;
        private Mock<IEntityFrameworkRepository<MediaItem>> mockedMediaItemEfRepository;
        private Mock<IMapperProvider> mockedMapperProvider;
        private Mock<IHttpContextProvider> mockedHttpContextProvider;

        private Mock<HttpPostedFileBase> mockedHttpPostedFileBase;
        private Mock<HttpContextBase> mockedHttpContextBase;
        private Mock<HttpServerUtilityBase> mockedHttpServerUtilityBase;
        private Mock<IMapper> mockedMapper;

        [SetUp]
        public void Init()
        {

            this.mockedDotLmsEfData = new Mock<IDotLmsEfData>();

            this.mockedMapper = new Mock<IMapper>();
            this.mockedMapper
                .Setup(x => x.Map<MediaItemViewModel>(It.IsAny<HttpPostedFileBase>()))
                .Returns(new MediaItemViewModel());

            this.mockedMapperProvider = new Mock<IMapperProvider>();
            this.mockedMapperProvider.Setup(x => x.Instance).Returns(mockedMapper.Object);

            this.mockedMediaItemEfRepository = new Mock<IEntityFrameworkRepository<MediaItem>>();

            this.mockedHttpServerUtilityBase = new Mock<HttpServerUtilityBase>();
            this.mockedHttpServerUtilityBase.Setup(x => x.MapPath(It.IsAny<string>())).Returns("somestring");

            this.mockedHttpContextBase = new Mock<HttpContextBase>();
            this.mockedHttpContextBase.Setup(x => x.Server).Returns(this.mockedHttpServerUtilityBase.Object);

            this.mockedHttpContextProvider = new Mock<IHttpContextProvider>();
            this.mockedHttpContextProvider.Setup(x => x.HttpContext)
                .Returns(mockedHttpContextBase.Object);

            this.mockedHttpPostedFileBase = new Mock<HttpPostedFileBase>();
            this.mockedHttpPostedFileBase.Setup(x => x.SaveAs(It.IsAny<string>()));
        }

        [Test]
        public void SaveFile_ShouldThrowArgumentNullException_WhenFileBaseParameterIsNull()
        {
            // Arrange 
            FileService service = this.GetFileService();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => service.SaveFile(null));
        }

        [Test]
        public void SaveFile_ShouldCall_HttpPostedFileBase_SaveAsOnce()
        {
            // Arrange 
            this.mockedDotLmsEfData
                .Setup(x => x.SaveChanges()).Returns(1);

            FileService service = this.GetFileService();

            // Act
            service.SaveFile(this.mockedHttpPostedFileBase.Object);

            // Assert
            this.mockedHttpPostedFileBase.Verify(x=>x.SaveAs(It.IsAny<string>()), Times.Once);
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