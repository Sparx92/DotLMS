using System;
using System.Linq.Expressions;
using AutoMapper;
using DotLms.Common;
using DotLms.Data.Contracts;
using DotLms.Data.Models;
using DotLms.Services.Providers.Contracts;
using DotLms.Web.Models;
using Moq;
using NUnit.Framework;

namespace DotLms.Services.Data.Tests.CourseServiceUnitTests
{
    [TestFixture]
    [Category(TestConstants.UnitTestCategory)]
    public class ConstructorTests
    {
        private Mock<IEntityFrameworkRepository<Course>> mockedCourseRepository;
        private Mock<IDotLmsEfData> mockedDotLmsEfData;
        private Mock<IMapperProvider> mockedMapperProvider;
        private Mock<IMapper> mockedMapper;


        [SetUp]
        public void Init()
        {
            this.mockedMapper = new Mock<IMapper>();
            this.mockedMapperProvider = new Mock<IMapperProvider>();
            this.mockedMapperProvider.SetupGet(x => x.Instance).Returns(this.mockedMapper.Object);
            
            this.mockedCourseRepository = new Mock<IEntityFrameworkRepository<Course>>();


            this.mockedDotLmsEfData = new Mock<IDotLmsEfData>();

        }

        [Test]
        public void Constructor_ShouldThrowArgumentNullException_WhenAllParametersAreNull()
        {
            // Arrange, Act & Assert
            Assert.Throws<ArgumentNullException>(() => new CourseService(null, null, null));
        }

        [Test]
        public void Constructor_ShouldThrowArgumentNullException_WhenCourseEfRepositoryIsNull()
        {
            // Arrange, Act & Assert
            Assert.Throws<ArgumentNullException>(() => new CourseService(null, this.mockedDotLmsEfData.Object,this.mockedMapperProvider.Object));
        }

        [Test]
        public void Constructor_ShouldThrowArgumentNullException_WhenDotLmsEfDataIsNull()
        {
            // Arrange, Act & Assert
            Assert.Throws<ArgumentNullException>(() => new CourseService(this.mockedCourseRepository.Object,  null, this.mockedMapperProvider.Object));
        }

        [Test]
        public void Constructor_ShouldThrowArgumentNullException_WhenMapperProviderIsNull()
        {
            // Arrange, Act & Assert
            Assert.Throws<ArgumentNullException>(() => new CourseService(this.mockedCourseRepository.Object, this.mockedDotLmsEfData.Object, null));
        }

        [Test]
        public void Constructor_ShouldNotThrow_WhenAllParametersarePassed()
        {
            // Arrange, Act & Assert
            Assert.DoesNotThrow(()=>
            {
                CourseService service = this.GetCourseService();
            });
        }

        private CourseService GetCourseService()
        {
            return new CourseService(
                this.mockedCourseRepository.Object,
                this.mockedDotLmsEfData.Object,
                this.mockedMapperProvider.Object);
        }
    }
}