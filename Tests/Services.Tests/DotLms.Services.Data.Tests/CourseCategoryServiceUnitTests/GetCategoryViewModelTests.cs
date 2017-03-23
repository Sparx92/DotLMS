using System;
using System.Linq.Expressions;
using AutoMapper;
using DotLms.Data.Contracts;
using DotLms.Data.Models;
using DotLms.Services.Providers.Contracts;
using DotLms.Web.Models;
using Moq;
using NUnit.Framework;

namespace DotLms.Services.Data.Tests.CourseCategoryServiceUnitTests
{
    [TestFixture]
    [Category(Common.TestConstants.UnitTestCategory)]
    public class GetCategoryViewModelTests
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
            this.mockedCategoryProjectableRepository.Setup(
                x =>
                    x.GetFirstMapped<CourseCategoryViewModel>(It.IsAny<Expression<Func<CourseCategory, bool>>>()));

            this.mockedCategoryRepository = new Mock<IEntityFrameworkRepository<CourseCategory>>();
            this.mockedCategoryRepository.Setup(x => x.Add(It.IsAny<CourseCategory>()));

            this.mockedDotLmsEfData = new Mock<IDotLmsEfData>();
        }

        [Test]
        public void GetCategoryViewModel_ShouldThrowArgmentNullException_WhenNameIsNull()
        {
            // Arrange
            CourseCategoryService service = this.GetCourseCategoryService();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => service.GetCategoryViewModel(null));
        }

        [Test]
        public void GetCategoryViewModel_ShouldThrowArgmentException_WhenNameIsEmpty()
        {
            // Arrange
            CourseCategoryService service = this.GetCourseCategoryService();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => service.GetCategoryViewModel(string.Empty));
        }

        [Test]
        public void GetCategoryViewModel_ShouldNotThrow_WhenNameIsNotNullOrEmpty()
        {
            // Arrange
            CourseCategoryService service = this.GetCourseCategoryService();

            // Act & Assert
            Assert.DoesNotThrow(() => service.GetCategoryViewModel("test"));
        }

        [Test]
        public void GetCategoryViewModel_ShouldCallCategoryProjectableRepositoryOnce()
        {
            // Arrange
            CourseCategoryService service = this.GetCourseCategoryService();

            // Act & 
            service.GetCategoryViewModel("test");

            // Assert
            this.mockedCategoryProjectableRepository.Verify(x => x.GetFirstMapped<CourseCategoryViewModel>(It.IsAny<Expression<Func<CourseCategory, bool>>>()), Times.Once);
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