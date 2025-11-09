using Xunit;
using Microsoft.AspNetCore.Mvc.Rendering;
using Moq;
using System.IO;
using Equant.SAV2000.ComponentLibrary.MVC.Components.ErrorComponent;

namespace Equant.SAV2000.ComponentLibrary.MVC.UnitTests.Components
{
    public class ErrorComponentTests
    {
        private readonly Mock<IHtmlHelper> _mockHtmlHelper;

        public ErrorComponentTests()
        {
            _mockHtmlHelper = new Mock<IHtmlHelper>();
        }

        [Fact]
        public void Constructor_InitializesWithDefaultValues()
        {
            var component = new ErrorComponents(_mockHtmlHelper.Object);

            Assert.NotNull(component);
            Assert.True(component.IsVisible);
        }

        [Fact]
        public void Builder_SetsId()
        {
            var component = new ErrorComponents(_mockHtmlHelper.Object);
            var builder = new ErrorBuilder(component, null);

            builder.Id("myErrorComponent");

            Assert.Equal("myErrorComponent", component.Id);
        }

        [Fact]
        public void Builder_SetsName()
        {
            var component = new ErrorComponents(_mockHtmlHelper.Object);
            var builder = new ErrorBuilder(component, null);

            builder.Name("ErrorComponent1");

            Assert.Equal("ErrorComponent1", component.Name);
        }

        [Fact]
        public void WriteHtml_GeneratesErrorComponentMarkup()
        {
            var component = new ErrorComponents(_mockHtmlHelper.Object)
            {
                Id = "testErrorComponent",
                ErrorMessage = "Test Error"
            };

            using var writer = new StringWriter();

            component.WriteHtml(writer);
            var html = writer.ToString();

            Assert.Contains("testErrorComponent", html);
        }

        [Fact]
        public void WriteHtml_WhenNotVisible_ShouldNotGenerateHtml()
        {
            var component = new ErrorComponents(_mockHtmlHelper.Object);
            component.IsVisible = false;
            var writer = new StringWriter();

            component.WriteHtml(writer);
            var html = writer.ToString();

            Assert.Empty(html);
        }

        [Fact]
        public void JsResources_ReturnsExpectedResources()
        {
            var component = new ErrorComponents(_mockHtmlHelper.Object);

            var resources = component.JsResources;

            Assert.NotNull(resources);
        }

        [Fact]
        public void Builder_ReturnsBuilderForFluentApi()
        {
            var component = new ErrorComponents(_mockHtmlHelper.Object);
            var builder = new ErrorBuilder(component, null);

            var result = builder.Id("test").Name("testName");

            Assert.IsType<ErrorBuilder>(result);
            Assert.Equal("test", component.Id);
            Assert.Equal("testName", component.Name);
        }
    }
}
