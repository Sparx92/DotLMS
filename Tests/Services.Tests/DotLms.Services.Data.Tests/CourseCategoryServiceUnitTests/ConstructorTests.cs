using System;
using NUnit.Framework;
using DotLms.Common;
using DotLms.Data.Contracts;
using DotLms.Data.Models;
using DotLms.Services.Providers.Contracts;
using Moq;

namespace DotLms.Services.Data.Tests.CourseCategoryServiceUnitTests
{
    [TestFixture]
    [Category(TestConstants.UnitTestCategory)]
    public class ConstructorTests
    {
        private Mock<IEntityFrameworkRepository<CourseCategory>> mockedCategoryRepository;
        private Mock<IProjectableRepository<CourseCategory>> mockedCategoryProjectableRepository;
        private Mock<IDotLmsEfData> mockedDotLmsEfData;
        private Mock<IMapperProvider> mockedMapperProvider;

        [SetUp]
        public void Init()
        {
            this.mockedMapperProvider = new Mock<IMapperProvider>();
            this.mockedCategoryProjectableRepository = new Mock<IProjectableRepository<CourseCategory>>();
            this.mockedCategoryRepository = new Mock<IEntityFrameworkRepository<CourseCategory>>();
            this.mockedDotLmsEfData = new Mock<IDotLmsEfData>();
        }

        [Test]
        public void Constructor_ShouldThrowArgumentNullException_WhenAllParametersAreNull()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new CourseCategoryService(null, null, null, null));
        }

        [Test]
        public void Constructor_ShouldThrowArgumentNullException_WhenCategoryRepositoryIsNull()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new CourseCategoryService(
                    null,
                    this.mockedDotLmsEfData.Object,
                    this.mockedMapperProvider.Object,
                    this.mockedCategoryProjectableRepository.Object));
        }


        [Test]
        public void Constructor_ShouldThrowArgumentNullException_WhenDotLmsEfDataIsNull()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new CourseCategoryService(
                    this.mockedCategoryRepository.Object,
                    null,
                    this.mockedMapperProvider.Object,
                    this.mockedCategoryProjectableRepository.Object));
        }

        [Test]
        public void Constructor_ShouldThrowArgumentNullException_WhenMapperProviderIsNull()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new CourseCategoryService(
                    this.mockedCategoryRepository.Object,
                    this.mockedDotLmsEfData.Object,
                    null,
                    this.mockedCategoryProjectableRepository.Object));
        }


        [Test]
        public void Constructor_ShouldThrowArgumentNullException_WhenCategoryProjectableRepositoryIsNull()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new CourseCategoryService(
                    this.mockedCategoryRepository.Object,
                    this.mockedDotLmsEfData.Object,
                    this.mockedMapperProvider.Object,
                    null));
        }

        [Test]
        public void Constructor_ShouldNotThrow_WhenAllParametersAreNotNull()
        {
            Assert.DoesNotThrow(() =>
                new CourseCategoryService(
                    this.mockedCategoryRepository.Object,
                    this.mockedDotLmsEfData.Object,
                    this.mockedMapperProvider.Object,
                    this.mockedCategoryProjectableRepository.Object));
        }
    }
}