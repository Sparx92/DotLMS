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
    public class GetAllCategoriesTests
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
            this.mockedCategoryProjectableRepository
                .Setup(x => x.GetFirstMapped<CourseCategoryViewModel>(It.IsAny<Expression<Func<CourseCategory, bool>>>()));
            this.mockedCategoryProjectableRepository
                .Setup(x => x.GetAllMapped<CourseCategoryViewModel>());

            this.mockedCategoryRepository = new Mock<IEntityFrameworkRepository<CourseCategory>>();
            this.mockedCategoryRepository.Setup(x => x.Add(It.IsAny<CourseCategory>()));

            this.mockedDotLmsEfData = new Mock<IDotLmsEfData>();
        }

        [Test]
        public void GetAllCategories_ShouldCallCategoryProjectableRepositoryGetAllMapped()
        {
            // Arrange
            CourseCategoryService service = this.GetCourseCategoryService();

            // Act
            service.GetAllCategories();
            
            // Assert
            this.mockedCategoryProjectableRepository.Verify(x=>x.GetAllMapped<CourseCategoryViewModel>(), Times.Once);
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