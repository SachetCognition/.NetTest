using Xunit;
using Microsoft.AspNetCore.Mvc.Rendering;
using Moq;
using System.IO;
using Equant.SAV2000.ComponentLibrary.MVC.Components.SpanLabel;

namespace Equant.SAV2000.ComponentLibrary.MVC.UnitTests.Components
{
    public class SpanLabelComponentTests
    {
        private readonly Mock<IHtmlHelper> _mockHtmlHelper;

        public SpanLabelComponentTests()
        {
            _mockHtmlHelper = new Mock<IHtmlHelper>();
        }

        [Fact]
        public void Constructor_InitializesWithDefaultValues()
        {
            var component = new SpanLabelComponent(_mockHtmlHelper.Object);

            Assert.NotNull(component);
            Assert.True(component.IsVisible);
        }

        [Fact]
        public void Builder_SetsId()
        {
            var component = new SpanLabelComponent(_mockHtmlHelper.Object);
            var builder = new SpanLabelBuilder(component, null);

            builder.Id("mySpanLabel");

            Assert.Equal("mySpanLabel", component.Id);
        }

        [Fact]
        public void Builder_SetsName()
        {
            var component = new SpanLabelComponent(_mockHtmlHelper.Object);
            var builder = new SpanLabelBuilder(component, null);

            builder.Name("SpanLabel1");

            Assert.Equal("SpanLabel1", component.Name);
        }

        [Fact]
        public void Builder_SetsText()
        {
            var component = new SpanLabelComponent(_mockHtmlHelper.Object);
            var builder = new SpanLabelBuilder(component, null);

            builder.Text("Span Label Text");

            Assert.Equal("Span Label Text", component.Text);
        }

        [Fact]
        public void WriteHtml_GeneratesSpanLabelMarkup()
        {
            var component = new SpanLabelComponent(_mockHtmlHelper.Object)
            {
                Id = "testSpanLabel",
                Text = "Test Span"
            };

            using var writer = new StringWriter();

            component.WriteHtml(writer);
            var html = writer.ToString();

            Assert.Contains("testSpanLabel", html);
        }

        [Fact]
        public void WriteHtml_WhenNotVisible_ShouldNotGenerateHtml()
        {
            var component = new SpanLabelComponent(_mockHtmlHelper.Object);
            component.IsVisible = false;
            var writer = new StringWriter();

            component.WriteHtml(writer);
            var html = writer.ToString();

            Assert.Empty(html);
        }

        [Fact]
        public void JsResources_ReturnsExpectedResources()
        {
            var component = new SpanLabelComponent(_mockHtmlHelper.Object);

            var resources = component.JsResources;

            Assert.NotNull(resources);
        }

        [Fact]
        public void Builder_ReturnsBuilderForFluentApi()
        {
            var component = new SpanLabelComponent(_mockHtmlHelper.Object);
            var builder = new SpanLabelBuilder(component, null);

            var result = builder.Id("test").Name("testName").Text("Test");

            Assert.IsType<SpanLabelBuilder>(result);
            Assert.Equal("test", component.Id);
            Assert.Equal("testName", component.Name);
            Assert.Equal("Test", component.Text);
        }
    }
}
