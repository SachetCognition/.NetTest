using Xunit;
using Microsoft.AspNetCore.Mvc.Rendering;
using Moq;
using System.IO;
using Equant.SAV2000.ComponentLibrary.MVC.Components.Label;

namespace Equant.SAV2000.ComponentLibrary.MVC.UnitTests.Components
{
    public class LabelComponentTests
    {
        private readonly Mock<IHtmlHelper> _mockHtmlHelper;

        public LabelComponentTests()
        {
            _mockHtmlHelper = new Mock<IHtmlHelper>();
        }

        [Fact]
        public void Constructor_InitializesWithDefaultValues()
        {
            var component = new LabelComponent(_mockHtmlHelper.Object);

            Assert.NotNull(component);
            Assert.True(component.IsVisible);
        }

        [Fact]
        public void Builder_SetsId()
        {
            var component = new LabelComponent(_mockHtmlHelper.Object);
            var builder = new LabelBuilder(component, null);

            builder.Id("myLabel");

            Assert.Equal("myLabel", component.Id);
        }

        [Fact]
        public void Builder_SetsName()
        {
            var component = new LabelComponent(_mockHtmlHelper.Object);
            var builder = new LabelBuilder(component, null);

            builder.Name("Label1");

            Assert.Equal("Label1", component.Name);
        }

        [Fact]
        public void Builder_SetsText()
        {
            var component = new LabelComponent(_mockHtmlHelper.Object);
            var builder = new LabelBuilder(component, null);

            builder.Text("Label Text");

            Assert.Equal("Label Text", component.Text);
        }

        [Fact]
        public void WriteHtml_GeneratesLabelMarkup()
        {
            var component = new LabelComponent(_mockHtmlHelper.Object)
            {
                Id = "testLabel",
                Text = "Test Label"
            };

            using var writer = new StringWriter();

            component.WriteHtml(writer);
            var html = writer.ToString();

            Assert.Contains("testLabel", html);
        }

        [Fact]
        public void WriteHtml_WhenNotVisible_ShouldNotGenerateHtml()
        {
            var component = new LabelComponent(_mockHtmlHelper.Object);
            component.IsVisible = false;
            var writer = new StringWriter();

            component.WriteHtml(writer);
            var html = writer.ToString();

            Assert.Empty(html);
        }

        [Fact]
        public void JsResources_ReturnsExpectedResources()
        {
            var component = new LabelComponent(_mockHtmlHelper.Object);

            var resources = component.JsResources;

            Assert.NotNull(resources);
        }

        [Fact]
        public void Builder_ReturnsBuilderForFluentApi()
        {
            var component = new LabelComponent(_mockHtmlHelper.Object);
            var builder = new LabelBuilder(component, null);

            var result = builder.Id("test").Name("testName").Text("Test");

            Assert.IsType<LabelBuilder>(result);
            Assert.Equal("test", component.Id);
            Assert.Equal("testName", component.Name);
            Assert.Equal("Test", component.Text);
        }
    }
}
