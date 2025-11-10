using Xunit;
using Microsoft.AspNetCore.Mvc.Rendering;
using Moq;
using System.IO;
using Equant.SAV2000.ComponentLibrary.MVC.Components.WeekYear;

namespace Equant.SAV2000.ComponentLibrary.MVC.UnitTests.Components
{
    public class WeekYearComponentTests
    {
        private readonly Mock<IHtmlHelper> _mockHtmlHelper;

        public WeekYearComponentTests()
        {
            _mockHtmlHelper = new Mock<IHtmlHelper>();
        }

        [Fact]
        public void Constructor_InitializesWithDefaultValues()
        {
            var component = new WeekYearComponent(_mockHtmlHelper.Object);

            Assert.NotNull(component);
            Assert.True(component.IsVisible);
            Assert.NotNull(component.Value);
        }

        [Fact]
        public void Builder_SetsId()
        {
            var component = new WeekYearComponent(_mockHtmlHelper.Object);
            var builder = new WeekYearBuilder(component, null);

            builder.Id("myWeekYear");

            Assert.Equal("myWeekYear", component.Id);
        }

        [Fact]
        public void Builder_SetsName()
        {
            var component = new WeekYearComponent(_mockHtmlHelper.Object);
            var builder = new WeekYearBuilder(component, null);

            builder.Name("WeekYear1");

            Assert.Equal("WeekYear1", component.Name);
        }

        [Fact]
        public void Builder_SetsValue()
        {
            var component = new WeekYearComponent(_mockHtmlHelper.Object);
            var builder = new WeekYearBuilder(component, null);
            var weekYearValue = new WeekYearWithFormat();

            builder.Value(weekYearValue);

            Assert.Equal(weekYearValue, component.Value);
        }

        [Fact]
        public void WriteHtml_GeneratesWeekYearMarkup()
        {
            var component = new WeekYearComponent(_mockHtmlHelper.Object)
            {
                Id = "testWeekYear",
                Name = "testWeekYearName"
            };

            using var writer = new StringWriter();

            component.WriteHtml(writer);
            var html = writer.ToString();

            Assert.Contains("testWeekYear", html);
        }

        [Fact]
        public void WriteHtml_WhenNotVisible_ShouldNotGenerateHtml()
        {
            var component = new WeekYearComponent(_mockHtmlHelper.Object);
            component.IsVisible = false;
            var writer = new StringWriter();

            component.WriteHtml(writer);
            var html = writer.ToString();

            Assert.Empty(html);
        }

        [Fact]
        public void JsResources_ReturnsExpectedResources()
        {
            var component = new WeekYearComponent(_mockHtmlHelper.Object);

            var resources = component.JsResources;

            Assert.NotNull(resources);
        }

        [Fact]
        public void WriteInitScript_GeneratesJavaScriptInitialization()
        {
            var component = new WeekYearComponent(_mockHtmlHelper.Object)
            {
                Id = "testWeekYear"
            };

            using var writer = new StringWriter();

            component.WriteInitScript(writer);
            var script = writer.ToString();

            Assert.Contains("testWeekYear", script);
        }

        [Fact]
        public void Builder_ReturnsBuilderForFluentApi()
        {
            var component = new WeekYearComponent(_mockHtmlHelper.Object);
            var builder = new WeekYearBuilder(component, null);

            var result = builder.Id("test").Name("testName");

            Assert.IsType<WeekYearBuilder>(result);
            Assert.Equal("test", component.Id);
            Assert.Equal("testName", component.Name);
        }

        [Fact]
        public void WeekYearWithFormat_InitializesCorrectly()
        {
            var weekYear = new WeekYearWithFormat();

            Assert.NotNull(weekYear);
        }
    }
}
