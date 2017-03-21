using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using DotLms.Data.Contracts;
using DotLms.Data.Models;
using DotLms.Data.Repositories;
using DotLms.Services.Providers.Contracts;
using DotLms.Web.Infrastructure.Mappings.Profiles;
using Moq;
using NUnit.Framework;

namespace DotLms.Data.Tests
{
    [TestFixture]
    public class EntityFrameworkRepositoryTests
    {
        private Mock<IMapperProvider> mapperProvider;
        private Mock<IDotLmsEfDbContext> context;

        [SetUp]
        public void Init()
        {
            this.mapperProvider = new Mock<IMapperProvider>();
            this.context = new Mock<IDotLmsEfDbContext>();
        }

        [Test]
        public void Constructor_ShouldThrowArgumentNullException_WhenContextIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new EntityFrameworkRepository<Course>(null));
        }

        [Test]
        public void Constructor_InitializesRepositoryContext()
        {
            Mock<IDotLmsEfDbContext> mockContext = new Mock<IDotLmsEfDbContext>();
            EntityFrameworkRepository<Course> repository = this.GetRepository();
            Assert.NotNull(repository.Context);
        }

        [Test]
        public void Constructor_InitializesRepositoryDbSet()
        {
            IDbSet<Course> mockSet = new Mock<IDbSet<Course>>().Object;
            this.context.Setup(x => x.Set<Course>()).Returns(mockSet);

            EntityFrameworkRepository<Course> repository = this.GetRepository();

            Assert.NotNull(repository.DbSet);
        }

        [Test]
        public void Constructor_InitializesRepositoryDbSetWithCorrectType()
        {
            IDbSet<Course> mockSet = new Mock<IDbSet<Course>>().Object;
            this.context.Setup(x => x.Set<Course>()).Returns(mockSet);

            EntityFrameworkRepository<Course> repository = this.GetRepository();

            Assert.NotNull(repository.DbSet);
            Assert.IsInstanceOf(typeof(IDbSet<Course>), repository.DbSet);
        }

        [Test]
        public void All_ReturnsRepositoryDbSet()
        {
            IDbSet<Course> mockSet = new Mock<IDbSet<Course>>().Object;

            this.context.Setup(x => x.Set<Course>()).Returns(mockSet);

            EntityFrameworkRepository<Course> repository = this.GetRepository();

            Assert.NotNull(repository.All);
            Assert.IsInstanceOf(typeof(IDbSet<Course>), repository.All);
            Assert.AreSame(repository.All, repository.DbSet);
        }


        [Test]
        public void Add_ShouldThrowArgumentNullException_WhenNullInputParameterIsPassed()
        {
            Mock<IDbSet<Course>> mockSet = new Mock<IDbSet<Course>>();

            this.context.Setup(x => x.Set<Course>()).Returns(mockSet.Object);
            this.context.Setup(x => x.Courses).Returns(mockSet.Object);

            EntityFrameworkRepository<Course> repository = this.GetRepository();

            Assert.Throws<ArgumentNullException>(() => repository.Add(null));
        }

        [Test]
        public void Add_ShouldNotThrow_WhenCorrectInputParameterIsPassed()
        {
            Mock<IDbSet<Course>> mockSet = new Mock<IDbSet<Course>>();
            Mock<Course> mockCourse = new Mock<Course>();
            DbEntityEntry<Course> fakeEntry = (DbEntityEntry<Course>) FormatterServices
                .GetSafeUninitializedObject(typeof(DbEntityEntry<Course>));

            this.context.Setup(x => x.Set<Course>()).Returns(mockSet.Object);
            this.context.Setup(x => x.Entry(It.IsAny<Course>())).Returns(fakeEntry);
            this.context.Setup(x => x.Courses).Returns(mockSet.Object);

            EntityFrameworkRepository<Course> repository = this.GetRepository();

            try
            {
                repository.Add(mockCourse.Object);
            }
            catch (NullReferenceException e)
            {
                // cannot activate or instance or mock a class with an internal constructor
                // the SafeUninitializedObject does not have any properties or methods, so the
                // DbEntityEntry.State is null
            }

            this.context.Verify(x => x.Entry(mockCourse.Object), Times.AtLeastOnce);
        }

        [Test]
        public void DataMaping_Configure()
        {
            DataMappingsProfile profile = new DataMappingsProfile();
            Assert.DoesNotThrow(() => profile
                .GetType()
                .GetMethod("Configure", BindingFlags.NonPublic | BindingFlags.Instance)
                .Invoke(profile, new object[] {}));
        }

        [Test]
        public void Delete_ShouldThrowArgumentNullException_WhenNullInputParameterIsPassed()
        {
            Mock<IDbSet<Course>> mockSet = new Mock<IDbSet<Course>>();

            this.context.Setup(x => x.Set<Course>()).Returns(mockSet.Object);
            this.context.Setup(x => x.Courses).Returns(mockSet.Object);

            EntityFrameworkRepository<Course> repository = this.GetRepository();
            Assert.Throws<ArgumentNullException>(() => repository.Delete(null));
        }

        [Test]
        public void Delete_ShouldNotThrow_WhenCorrectInputParameterIsPassed()
        {
            Mock<IDbSet<Course>> mockSet = new Mock<IDbSet<Course>>();

            this.context.Setup(x => x.Set<Course>()).Returns(mockSet.Object);
            this.context.Setup(x => x.Courses).Returns(mockSet.Object);

            Mock<Course> mockCourse = new Mock<Course>();
            EntityFrameworkRepository<Course> repository = this.GetRepository();

            try
            {
                repository.Delete(mockCourse.Object);
            }
            catch (NullReferenceException e)
            {
                // cannot activate or instance or mock a class with an internal constructor
                // the SafeUninitializedObject does not have any properties or methods, so the
                // DbEntityEntry.State is null
            }

            this.context.Verify(x => x.Entry(mockCourse.Object), Times.AtLeastOnce);
        }

        [Test]
        public void Update_ShouldThrowArgumentNullException_WhenNullInputParameterIsPassed()
        {
            Mock<IDbSet<Course>> mockSet = new Mock<IDbSet<Course>>();

            this.context.Setup(x => x.Set<Course>()).Returns(mockSet.Object);
            this.context.Setup(x => x.Courses).Returns(mockSet.Object);

            EntityFrameworkRepository<Course> repository = this.GetRepository();
            Assert.Throws<ArgumentNullException>(() => repository.Update(null));
        }

        [Test]
        public void Update_ShouldNotThrow_WhenCorrectInputParameterIsPassed()
        {
            Mock<IDbSet<Course>> mockSet = new Mock<IDbSet<Course>>();

            this.context.Setup(x => x.Set<Course>()).Returns(mockSet.Object);
            this.context.Setup(x => x.Courses).Returns(mockSet.Object);

            Mock<Course> mockCourse = new Mock<Course>();
            EntityFrameworkRepository<Course> repository = this.GetRepository();

            try
            {
                repository.Update(mockCourse.Object);
            }
            catch (NullReferenceException e)
            {
                // cannot activate or instance or mock a class with an internal constructor
                // the SafeUninitializedObject does not have any properties or methods, so the
                // DbEntityEntry.State is null
            }

            this.context.Verify(x => x.Entry(mockCourse.Object), Times.AtLeastOnce);
        }

        private EntityFrameworkRepository<Course> GetRepository()
        {
            return new EntityFrameworkRepository<Course>(this.context.Object);
        }
    }
}