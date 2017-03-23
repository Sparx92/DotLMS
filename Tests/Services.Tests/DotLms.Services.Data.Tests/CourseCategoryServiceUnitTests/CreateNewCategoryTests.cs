using System;
using AutoMapper;
using DotLms.Data.Contracts;
using DotLms.Data.Models;
using DotLms.Services.Providers.Contracts;
using DotLms.Web.Models;
using Moq;
using NUnit.Framework;

namespace DotLms.Services.Data.Tests.CourseCategoryServiceUnitTests
{
    public class CreateNewCategoryTests
    {
        private Mock<IEntityFrameworkRepository<CourseCategory>> mockedCategoryRepository;
        private Mock<IProjectableRepository<CourseCategory>> mockedCategoryProjectableRepository;
        private Mock<IDotLmsEfData> mockedDotLmsEfData;
        private Mock<IMapperProvider> mockedMapperProvider;
        private Mock<IMapper> mockedMapper;

        [SetUp]
        public void Init()
        {
            this.mockedMapper = new Mock<IMapper>();
            this.mockedMapperProvider = new Mock<IMapperProvider>();
            this.mockedMapperProvider.SetupGet(x => x.Instance).Returns(this.mockedMapper.Object);

            this.mockedCategoryProjectableRepository = new Mock<IProjectableRepository<CourseCategory>>();

            this.mockedCategoryRepository = new Mock<IEntityFrameworkRepository<CourseCategory>>();
            this.mockedCategoryRepository.Setup(x => x.Add(It.IsAny<CourseCategory>()));

            this.mockedDotLmsEfData = new Mock<IDotLmsEfData>();
        }

        [Test]
        public void CreateNewCategory_ShouldThrowArgmentNullException_WhenCourseCategoryViewModelIsNull()
        {
            // Arrange
            CourseCategoryService service = this.GetCourseCategoryService();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => service.CreateNewCategory(null));
        }

        [Test]
        public void CreateNewCategory_ShouldNotThrow_WhenCourseCategoryViewModelIsNotNull()
        {
            // Arrange
            CourseCategoryService service = this.GetCourseCategoryService();
            CourseCategoryViewModel model = new CourseCategoryViewModel();

            // Act & Assert
            Assert.DoesNotThrow(() => service.CreateNewCategory(model));
        }

        [Test]
        public void CreateNewCategory_ShouldCallMapperProviderInstanceOnce()
        {
            // Arrange
            CourseCategoryService service = this.GetCourseCategoryService();
            CourseCategoryViewModel model = new CourseCategoryViewModel();

            // Act
            service.CreateNewCategory(model);

            // Assert
            this.mockedMapperProvider.Verify(x=>x.Instance, Times.Once);
        }

        [Test]
        public void CreateNewCategory_ShouldCallCategoryRepositoryAddOnce()
        {
            // Arrange
            CourseCategoryService service = this.GetCourseCategoryService();
            CourseCategoryViewModel model = new CourseCategoryViewModel();

            // Act
            service.CreateNewCategory(model);

            // Assert
            this.mockedCategoryRepository.Verify(x => x.Add(It.IsAny<CourseCategory>()), Times.Once);
        }

        [Test]
        public void CreateNewCategory_ShouldCallDotLmsEfDataSaveChangesOnce()
        {
            // Arrange
            CourseCategoryService service = this.GetCourseCategoryService();
            CourseCategoryViewModel model = new CourseCategoryViewModel();

            // Act
            service.CreateNewCategory(model);

            // Assert
            this.mockedDotLmsEfData.Verify(x => x.SaveChanges(), Times.Once);
        }

        private CourseCategoryService GetCourseCategoryService()
        {
            return new CourseCategoryService(
                this.mockedCategoryRepository.Object,
                this.mockedDotLmsEfData.Object,
                this.mockedMapperProvider.Object,
                this.mockedCategoryProjectableRepository.Object);
        }
    }
}