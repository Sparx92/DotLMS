using NUnit.Framework;

namespace DotLms.Data.Models.Tests.CourseCategory
{
    [TestFixture]
    [Category(DotLms.Common.TestConstants.UnitTestCategory)]
    public class Id
    {
        [TestCase(1)]
        [TestCase(2)]
        public void Id_GetShouldReturnSetValue(int value)
        {
            // Arange 
            Models.CourseCategory category = new Models.CourseCategory();

            // Act
            category.Id = value;

            // Assert
            Assert.AreEqual(category.Id,value);
        }
    }
}