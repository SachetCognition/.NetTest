using Xunit;
using Microsoft.AspNetCore.Mvc.Rendering;
using Moq;
using System.IO;
using Equant.SAV2000.ComponentLibrary.MVC.Components.HyperLink;

namespace Equant.SAV2000.ComponentLibrary.MVC.UnitTests.Components
{
    public class HyperLinkComponentTests
    {
        private readonly Mock<IHtmlHelper> _mockHtmlHelper;

        public HyperLinkComponentTests()
        {
            _mockHtmlHelper = new Mock<IHtmlHelper>();
        }

        [Fact]
        public void Constructor_InitializesWithDefaultValues()
        {
            var component = new HyperLinkComponent(_mockHtmlHelper.Object);

            Assert.NotNull(component);
            Assert.True(component.IsVisible);
        }

        [Fact]
        public void Builder_SetsId()
        {
            var component = new HyperLinkComponent(_mockHtmlHelper.Object);
            var builder = new HyperLinkBuilder(component, null);

            builder.Id("myHyperLink");

            Assert.Equal("myHyperLink", component.Id);
        }

        [Fact]
        public void Builder_SetsName()
        {
            var component = new HyperLinkComponent(_mockHtmlHelper.Object);
            var builder = new HyperLinkBuilder(component, null);

            builder.Name("HyperLink1");

            Assert.Equal("HyperLink1", component.Name);
        }

        [Fact]
        public void Builder_SetsUrl()
        {
            var component = new HyperLinkComponent(_mockHtmlHelper.Object);
            var builder = new HyperLinkBuilder(component, null);

            builder.Url("/test/url");

            Assert.Equal("/test/url", component.Url);
        }

        [Fact]
        public void Builder_SetsText()
        {
            var component = new HyperLinkComponent(_mockHtmlHelper.Object);
            var builder = new HyperLinkBuilder(component, null);

            builder.Text("Click Here");

            Assert.Equal("Click Here", component.Text);
        }

        [Fact]
        public void WriteHtml_GeneratesHyperLinkMarkup()
        {
            var component = new HyperLinkComponent(_mockHtmlHelper.Object)
            {
                Id = "testHyperLink",
                Url = "/test",
                Text = "Test Link"
            };

            using var writer = new StringWriter();

            component.WriteHtml(writer);
            var html = writer.ToString();

            Assert.Contains("testHyperLink", html);
        }

        [Fact]
        public void WriteHtml_WhenNotVisible_ShouldNotGenerateHtml()
        {
            var component = new HyperLinkComponent(_mockHtmlHelper.Object);
            component.IsVisible = false;
            var writer = new StringWriter();

            component.WriteHtml(writer);
            var html = writer.ToString();

            Assert.Empty(html);
        }

        [Fact]
        public void JsResources_ReturnsExpectedResources()
        {
            var component = new HyperLinkComponent(_mockHtmlHelper.Object);

            var resources = component.JsResources;

            Assert.NotNull(resources);
        }

        [Fact]
        public void Builder_ReturnsBuilderForFluentApi()
        {
            var component = new HyperLinkComponent(_mockHtmlHelper.Object);
            var builder = new HyperLinkBuilder(component, null);

            var result = builder.Id("test").Name("testName").Url("/url");

            Assert.IsType<HyperLinkBuilder>(result);
            Assert.Equal("test", component.Id);
            Assert.Equal("testName", component.Name);
            Assert.Equal("/url", component.Url);
        }
    }
}
