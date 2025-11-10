using Xunit;
using Microsoft.AspNetCore.Mvc.Rendering;
using Moq;
using System.IO;
using Equant.SAV2000.ComponentLibrary.MVC.Components.Image;

namespace Equant.SAV2000.ComponentLibrary.MVC.UnitTests.Components
{
    public class ImageComponentTests
    {
        private readonly Mock<IHtmlHelper> _mockHtmlHelper;

        public ImageComponentTests()
        {
            _mockHtmlHelper = new Mock<IHtmlHelper>();
        }

        [Fact]
        public void Constructor_InitializesWithDefaultValues()
        {
            var component = new ImageComponent(_mockHtmlHelper.Object);

            Assert.NotNull(component);
            Assert.True(component.IsVisible);
        }

        [Fact]
        public void Builder_SetsId()
        {
            var component = new ImageComponent(_mockHtmlHelper.Object);
            var builder = new ImageBuilder(component, null);

            builder.Id("myImage");

            Assert.Equal("myImage", component.Id);
        }

        [Fact]
        public void Builder_SetsName()
        {
            var component = new ImageComponent(_mockHtmlHelper.Object);
            var builder = new ImageBuilder(component, null);

            builder.Name("Image1");

            Assert.Equal("Image1", component.Name);
        }

        [Fact]
        public void Builder_SetsSrc()
        {
            var component = new ImageComponent(_mockHtmlHelper.Object);
            var builder = new ImageBuilder(component, null);

            builder.Src("/images/test.png");

            Assert.Equal("/images/test.png", component.Src);
        }

        [Fact]
        public void Builder_SetsAlt()
        {
            var component = new ImageComponent(_mockHtmlHelper.Object);
            var builder = new ImageBuilder(component, null);

            builder.Alt("Test Image");

            Assert.Equal("Test Image", component.Alt);
        }

        [Fact]
        public void WriteHtml_GeneratesImageMarkup()
        {
            var component = new ImageComponent(_mockHtmlHelper.Object)
            {
                Id = "testImage",
                Src = "/test.png",
                Alt = "Test"
            };

            using var writer = new StringWriter();

            component.WriteHtml(writer);
            var html = writer.ToString();

            Assert.Contains("testImage", html);
        }

        [Fact]
        public void WriteHtml_WhenNotVisible_ShouldNotGenerateHtml()
        {
            var component = new ImageComponent(_mockHtmlHelper.Object);
            component.IsVisible = false;
            var writer = new StringWriter();

            component.WriteHtml(writer);
            var html = writer.ToString();

            Assert.Empty(html);
        }

        [Fact]
        public void JsResources_ReturnsExpectedResources()
        {
            var component = new ImageComponent(_mockHtmlHelper.Object);

            var resources = component.JsResources;

            Assert.NotNull(resources);
        }

        [Fact]
        public void Builder_ReturnsBuilderForFluentApi()
        {
            var component = new ImageComponent(_mockHtmlHelper.Object);
            var builder = new ImageBuilder(component, null);

            var result = builder.Id("test").Src("/image.png").Alt("Test");

            Assert.IsType<ImageBuilder>(result);
            Assert.Equal("test", component.Id);
            Assert.Equal("/image.png", component.Src);
            Assert.Equal("Test", component.Alt);
        }
    }
}
