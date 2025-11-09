using Xunit;
using Microsoft.AspNetCore.Mvc.Rendering;
using Moq;
using System.IO;
using Equant.SAV2000.ComponentLibrary.MVC.Components.DateTimeControl;

namespace Equant.SAV2000.ComponentLibrary.MVC.UnitTests.Components
{
    public class DateTimeControlComponentTests
    {
        private readonly Mock<IHtmlHelper> _mockHtmlHelper;

        public DateTimeControlComponentTests()
        {
            _mockHtmlHelper = new Mock<IHtmlHelper>();
        }

        [Fact]
        public void Constructor_InitializesWithDefaultValues()
        {
            var component = new DateTimeComponent(_mockHtmlHelper.Object);

            Assert.NotNull(component);
            Assert.NotNull(component.Value);
            Assert.True(component.IsVisible);
        }

        [Fact]
        public void WriteHtml_GeneratesDateTimeControlMarkup()
        {
            var component = new DateTimeComponent(_mockHtmlHelper.Object)
            {
                Id = "testDateTime",
                Name = "testDateTimeName"
            };

            using var writer = new StringWriter();

            component.WriteHtml(writer);
            var html = writer.ToString();

            Assert.Contains("testDateTime", html);
        }

        [Fact]
        public void Builder_SetsId()
        {
            var component = new DateTimeComponent(_mockHtmlHelper.Object);
            var builder = new DateTimeBuilder(component, null);

            builder.Id("myDateTime");

            Assert.Equal("myDateTime", component.Id);
        }

        [Fact]
        public void Builder_SetsName()
        {
            var component = new DateTimeComponent(_mockHtmlHelper.Object);
            var builder = new DateTimeBuilder(component, null);

            builder.Name("dateField");

            Assert.Equal("dateField", component.Name);
        }

        [Fact]
        public void Builder_SetsDisplayTime()
        {
            var component = new DateTimeComponent(_mockHtmlHelper.Object);
            var builder = new DateTimeBuilder(component, null);

            builder.DisplayTime(true);

            Assert.True(component.DisplayTime);
        }

        [Fact]
        public void Builder_SetsValue()
        {
            var component = new DateTimeComponent(_mockHtmlHelper.Object);
            var builder = new DateTimeBuilder(component, null);
            var dateValue = new DateTimeWithFormat("dd/MM/yyyy", false);

            builder.Value(dateValue);

            Assert.Equal(dateValue, component.Value);
        }

        [Fact]
        public void Builder_ReturnsBuilderForFluentApi()
        {
            var component = new DateTimeComponent(_mockHtmlHelper.Object);
            var builder = new DateTimeBuilder(component, null);

            var result = builder.Id("test").Name("testName");

            Assert.IsType<DateTimeBuilder>(result);
            Assert.Equal("test", component.Id);
            Assert.Equal("testName", component.Name);
        }

        [Fact]
        public void WriteInitScript_GeneratesJavaScriptInitialization()
        {
            var component = new DateTimeComponent(_mockHtmlHelper.Object)
            {
                Id = "testDateTime"
            };

            using var writer = new StringWriter();

            component.WriteInitScript(writer);
            var script = writer.ToString();

            Assert.Contains("testDateTime", script);
        }

        [Fact]
        public void JsResources_ReturnsExpectedResources()
        {
            var component = new DateTimeComponent(_mockHtmlHelper.Object);

            var resources = component.JsResources;

            Assert.NotNull(resources);
        }

        [Fact]
        public void DateTimeWithFormat_InitializesCorrectly()
        {
            var dateTime = new DateTimeWithFormat("dd/MM/yyyy", false);

            Assert.NotNull(dateTime);
            Assert.Equal("dd/MM/yyyy", dateTime.Format);
        }
    }
}
