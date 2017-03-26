using System;
using DotLms.Web.Areas.Backoffice.Controllers;
using DotLms.Web.Attributes;
using NUnit.Framework;
using TestStack.FluentMVCTesting;

namespace DotLms.Web.Tests
{
    [TestFixture]
    [Category(Common.TestConstants.UnitTestCategory)]
    public class IndexTests
    {
        private BackOfficeHomeController controller;

        [SetUp]
        public void Init()
        {
            this.controller = new BackOfficeHomeController();
        }

        [Test]
        public void Index_ShouldCallDefaultView()
        {
            this.controller.WithCallTo(x => x.Index()).ShouldRenderDefaultView();
        }

        [Test]
        public void Index_ShouldHaveBackofficeAuthorizatuonAttribute()
        {
            bool backofficeAuthorizatuonAttributeIsDefined = Attribute.IsDefined(
                typeof(BackOfficeHomeController).GetMethod(nameof(BackOfficeHomeController.Index)),
                typeof(BackofficeAuthorizatuonAttribute));

            Assert.IsTrue(backofficeAuthorizatuonAttributeIsDefined);
        }
    }
}
