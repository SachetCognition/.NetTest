using Xunit;
using Microsoft.AspNetCore.Mvc.Rendering;
using Moq;
using System.IO;
using Equant.SAV2000.ComponentLibrary.MVC.Components.ImageToolTip;

namespace Equant.SAV2000.ComponentLibrary.MVC.UnitTests.Components
{
    public class ImageToolTipComponentTests
    {
        private readonly Mock<IHtmlHelper> _mockHtmlHelper;

        public ImageToolTipComponentTests()
        {
            _mockHtmlHelper = new Mock<IHtmlHelper>();
        }

        [Fact]
        public void Constructor_InitializesWithDefaultValues()
        {
            var component = new ImageToolTipComponent(_mockHtmlHelper.Object);

            Assert.NotNull(component);
            Assert.True(component.IsVisible);
        }

        [Fact]
        public void Builder_SetsId()
        {
            var component = new ImageToolTipComponent(_mockHtmlHelper.Object);
            var builder = new ImageToolTipBuilder(component, null);

            builder.Id("myImageToolTip");

            Assert.Equal("myImageToolTip", component.Id);
        }

        [Fact]
        public void Builder_SetsName()
        {
            var component = new ImageToolTipComponent(_mockHtmlHelper.Object);
            var builder = new ImageToolTipBuilder(component, null);

            builder.Name("ImageToolTip1");

            Assert.Equal("ImageToolTip1", component.Name);
        }

        [Fact]
        public void Builder_SetsToolTipText()
        {
            var component = new ImageToolTipComponent(_mockHtmlHelper.Object);
            var builder = new ImageToolTipBuilder(component, null);

            builder.ToolTipText("Tooltip content");

            Assert.Equal("Tooltip content", component.ToolTipText);
        }

        [Fact]
        public void Builder_SetsImageUrl()
        {
            var component = new ImageToolTipComponent(_mockHtmlHelper.Object);
            var builder = new ImageToolTipBuilder(component, null);

            builder.ImageUrl("/images/info.png");

            Assert.Equal("/images/info.png", component.ImageUrl);
        }

        [Fact]
        public void WriteHtml_GeneratesImageToolTipMarkup()
        {
            var component = new ImageToolTipComponent(_mockHtmlHelper.Object)
            {
                Id = "testImageToolTip",
                ToolTipText = "Test tooltip",
                ImageUrl = "/info.png"
            };

            using var writer = new StringWriter();

            component.WriteHtml(writer);
            var html = writer.ToString();

            Assert.Contains("testImageToolTip", html);
        }

        [Fact]
        public void WriteHtml_WhenNotVisible_ShouldNotGenerateHtml()
        {
            var component = new ImageToolTipComponent(_mockHtmlHelper.Object);
            component.IsVisible = false;
            var writer = new StringWriter();

            component.WriteHtml(writer);
            var html = writer.ToString();

            Assert.Empty(html);
        }

        [Fact]
        public void JsResources_ReturnsExpectedResources()
        {
            var component = new ImageToolTipComponent(_mockHtmlHelper.Object);

            var resources = component.JsResources;

            Assert.NotNull(resources);
        }

        [Fact]
        public void WriteInitScript_GeneratesJavaScriptInitialization()
        {
            var component = new ImageToolTipComponent(_mockHtmlHelper.Object)
            {
                Id = "testImageToolTip"
            };

            using var writer = new StringWriter();

            component.WriteInitScript(writer);
            var script = writer.ToString();

            Assert.Contains("testImageToolTip", script);
        }

        [Fact]
        public void Builder_ReturnsBuilderForFluentApi()
        {
            var component = new ImageToolTipComponent(_mockHtmlHelper.Object);
            var builder = new ImageToolTipBuilder(component, null);

            var result = builder.Id("test").ToolTipText("Tooltip").ImageUrl("/img.png");

            Assert.IsType<ImageToolTipBuilder>(result);
            Assert.Equal("test", component.Id);
            Assert.Equal("Tooltip", component.ToolTipText);
            Assert.Equal("/img.png", component.ImageUrl);
        }
    }
}
