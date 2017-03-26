using System;
using System.Data.Entity;
using System.Net.Mime;
using System.Reflection;
using DotLms.Data.Models;
using Moq;
using NUnit.Framework;

namespace DotLms.Data.Tests.DotLmsEfDbContextUnitTests
{
    [TestFixture]
    [Category(Common.TestConstants.UnitTestCategory)]
    public class DotLmsEfDbContextTests
    {
        private Mock<DotLmsEfDbContext> mockedContext;
        private DotLmsEfDbContext context;

        [SetUp]
        public void Init()
        {
            this.mockedContext = new Mock<DotLmsEfDbContext>();
            this.context = this.mockedContext.Object;
        }

        [Test]
        public void Courses_GetShouldReturnSetValue()
        {
            IDbSet<Course> set = (IDbSet<Course>)typeof(DbSet<Course>)
                .GetConstructor(
                    BindingFlags.NonPublic | BindingFlags.CreateInstance | BindingFlags.Instance,
                    null,
                    new Type[] { },
                    null)
                .Invoke(new object[] { });

            this.context.Courses = set;
            Assert.AreEqual(set, this.context.Courses);
        }

        [Test]
        public void CourseCategories_GetShouldReturnSetValue()
        {
            IDbSet<CourseCategory> set = (IDbSet<CourseCategory>)typeof(DbSet<CourseCategory>)
                .GetConstructor(
                    BindingFlags.NonPublic | BindingFlags.CreateInstance | BindingFlags.Instance,
                    null,
                    new Type[] { },
                    null)
                .Invoke(new object[] { });

            this.context.CourseCategories = set;
            Assert.AreEqual(set, this.context.CourseCategories);
        }

        [Test]
        public void MediaItems_GetShouldReturnSetValue()
        {
            IDbSet<MediaItem> set = (IDbSet<MediaItem>)typeof(DbSet<MediaItem>)
                .GetConstructor(
                    BindingFlags.NonPublic | BindingFlags.CreateInstance | BindingFlags.Instance,
                    null,
                    new Type[] { },
                    null)
                .Invoke(new object[] { });

            this.context.MediaItems = set;
            Assert.AreEqual(set, this.context.MediaItems);
        }

        [Test]
        public void Pages_GetShouldReturnSetValue()
        {
            IDbSet<Page> set = (IDbSet<Page>)typeof(DbSet<Page>)
                .GetConstructor(
                    BindingFlags.NonPublic | BindingFlags.CreateInstance | BindingFlags.Instance,
                    null,
                    new Type[] { },
                    null)
                .Invoke(new object[] { });

            this.context.Pages = set;
            Assert.AreEqual(set, this.context.Pages);
        }

        [Test]
        public void Set_ShouldReturnIDbSetInstanceOfTheRequiredType()
        {
            string expected = typeof(DbSet<User>).Name;
            string actual = this.context.Set<User>().GetType().Name;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void SaveChanges_ShouldReturn0WhenEverythingIsOk()
        {
            int expected = 0;
            int actual = this.context.SaveChanges();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void StaticMethodCreateShouldReturnNewDotLmsEfDbContext()
        {
            // Arrange, Act
            DotLmsEfDbContext context = DotLmsEfDbContext.Create();

            // Assert
            Assert.AreEqual(typeof(DotLmsEfDbContext), context.GetType());
        }
    }
}