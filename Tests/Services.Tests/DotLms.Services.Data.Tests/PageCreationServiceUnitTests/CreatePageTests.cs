using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using AutoMapper;
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
    public class CreatePageTests
    {
        private Mock<IDotLmsEfData> mockedDotLmsEfData;
        private Mock<IProjectableRepository<Page>> mockedPageProjectableRepository;
        private Mock<IEntityFrameworkRepository<User>> mockedUserRepository;
        private Mock<IDateTimeProvider> mockedDateTimeProvider;
        private Mock<IMapper> mockedMapper;
        private Mock<IMapperProvider> mockedMapperProvider;
        private Mock<IDbSet<User>> mockedSet;

        private User testUser = new User { UserName = "testUsername" };
        private IList<User> testUsers = new List<User>();
        private static Page testPage = new Page { Name = "Name" };

        [SetUp]
        public void Init()
        {
            this.testUsers.Add(testUser);

            this.mockedSet = new Mock<IDbSet<User>>();
            this.mockedSet.Setup(x => x.Attach(testUser));
            this.mockedSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(testUsers.AsQueryable().Provider);
            this.mockedSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(testUsers.AsQueryable().Expression);
            this.mockedSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(testUsers.AsQueryable().ElementType);
            this.mockedSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(testUsers.AsQueryable().GetEnumerator);

            this.mockedDotLmsEfData = new Mock<IDotLmsEfData>();

            this.mockedPageProjectableRepository = new Mock<IProjectableRepository<Page>>();

            this.mockedUserRepository = new Mock<IEntityFrameworkRepository<User>>();
            this.mockedUserRepository.Setup(x => x.DbSet).Returns(mockedSet.Object);
            this.mockedUserRepository.Setup(x => x.All).Returns(mockedSet.Object);

            this.mockedDateTimeProvider = new Mock<IDateTimeProvider>();

            this.mockedMapper = new Mock<IMapper>();
            this.mockedMapper
                .Setup(x => x.Map<Page>(It.IsAny<PageViewModel>()))
                .Returns(testPage);

            this.mockedMapperProvider = new Mock<IMapperProvider>();
            this.mockedMapperProvider.Setup(x => x.Instance).Returns(this.mockedMapper.Object);
        }

        [Test]
        public void CreatePage_ShouldThrow_ArgumentNullException_WhenAllParametersAreNull()
        {
            // Arrange
            PageCreationService service = this.GetService();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                service.CreatePage(null, null);
            });
        }

        [Test]
        public void CreatePage_ShouldThrow_ArgumentNullException_WhenPageViewModelIsNull()
        {
            // Arrange
            PageCreationService service = this.GetService();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                service.CreatePage(null, "test");
            });
        }

        [Test]
        public void CreatePage_ShouldThrow_ArgumentNullException_WhenUsernameIsNull()
        {
            // Arrange
            PageCreationService service = this.GetService();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                service.CreatePage(new PageViewModel(), null);
            });
        }

        [Test]
        public void CreatePage_ShouldThrow_ArgumentException_WhenUsernameIsEmpty()
        {
            // Arrange
            PageCreationService service = this.GetService();

            // Act & Assert
            Assert.Throws<ArgumentException>(() =>
            {
                service.CreatePage(new PageViewModel(), string.Empty);
            });
        }

        [Test]
        public void CreatePage_ShouldCallUserEfRepoSitoryAllToBuildQuery()
        {
            // Arrange 
            PageCreationService service = this.GetService();
            PageViewModel model = new PageViewModel
            {
                Id = 1,
                Name = "Name",
                HtmlContent = "HtmlContent",
                ParentCourse = new CourseViewModel { Id = 1, Name = "Course Name" },
            };

            // Act 
            service.CreatePage(model, "testUsername");

            // Assert
            this.mockedUserRepository.Verify(x => x.All, Times.Once);
        }

        [Test]
        public void CreatePage_ShouldCallMapperProviderInstanceOnce()
        {
            // Arrange 
            PageCreationService service = this.GetService();
            PageViewModel model = new PageViewModel
            {
                Id = 1,
                Name = "Name",
                HtmlContent = "HtmlContent",
                ParentCourse = new CourseViewModel { Id = 1, Name = "Course Name" },
            };

            // Act 
            service.CreatePage(model, "testUsername");

            // Assert
            this.mockedMapperProvider.Verify(x => x.Instance, Times.Once);
        }

        [Test]
        public void CreatePage_ShouldCallMapperMap_GenericFromPage_Once()
        {
            // Arrange 
            PageCreationService service = this.GetService();
            PageViewModel model = new PageViewModel
            {
                Id = 1,
                Name = "Name",
                HtmlContent = "HtmlContent",
                ParentCourse = new CourseViewModel { Id = 1, Name = "Course Name" },
            };

            // Act 
            service.CreatePage(model, "testUsername");

            // Assert
            this.mockedMapper.Verify(x => x.Map<Page>(It.IsAny<PageViewModel>()), Times.Once);
        }

        [Test]
        public void CreatePage_ShouldRemoveScriptTagsFromHtmlConetnt()
        {
            // Arrange 
            PageCreationService service = this.GetService();
            PageViewModel model = new PageViewModel
            {
                Id = 1,
                Name = "Name",
                HtmlContent = "HtmlContent<script>nasty</script>",
                ParentCourse = new CourseViewModel { Id = 1, Name = "Course Name" },
            };

            // Act 
            service.CreatePage(model, "testUsername");

            // Assert
            Assert.AreEqual("HtmlContent", model.HtmlContent);
        }

        [Test]
        public void CreatePage_ShouldCallProjectableRepoAdd_Once()
        {
            // Arrange 
            PageCreationService service = this.GetService();
            PageViewModel model = new PageViewModel
            {
                Id = 1,
                Name = "Name",
                HtmlContent = "HtmlContent",
                ParentCourse = new CourseViewModel { Id = 1, Name = "Course Name" },
            };

            // Act 
            service.CreatePage(model, "testUsername");

            // Assert
            this.mockedPageProjectableRepository.Verify(x => x.Add(testPage), Times.Once);
        }

        [Test]
        public void CreatePage_ShouldCallDotLmsDataSaveChanges_Once()
        {
            // Arrange 
            PageCreationService service = this.GetService();
            PageViewModel model = new PageViewModel
            {
                Id = 1,
                Name = "Name",
                HtmlContent = "HtmlContent",
                ParentCourse = new CourseViewModel { Id = 1, Name = "Course Name" },
            };

            // Act 
            service.CreatePage(model, "testUsername");

            // Assert
            this.mockedDotLmsEfData.Verify(x => x.SaveChanges(), Times.Once);
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