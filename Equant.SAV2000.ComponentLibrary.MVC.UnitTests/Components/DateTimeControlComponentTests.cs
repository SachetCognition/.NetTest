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
            Assert.False(component.IsDisabled);
            Assert.Null(component.Value);
        }

        [Fact]
        public void WriteHtml_GeneratesDateTimeControlMarkup()
        {
            var component = new DateTimeComponent(_mockHtmlHelper.Object)
            {
                ClientId = "testDateTime",
                Name = "testDateTimeName"
            };

            using var writer = new StringWriter();

            component.WriteHtml(writer);
            var html = writer.ToString();

            Assert.Contains("testDateTime", html);
        }

        [Fact]
        public void Builder_SetsClientId()
        {
            var component = new DateTimeComponent(_mockHtmlHelper.Object);
            var builder = new DateTimeBuilder(component, null);

            builder.ClientId("myDateTime");

            Assert.Equal("myDateTime", component.ClientId);
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
        public void Builder_SetsDisabled()
        {
            var component = new DateTimeComponent(_mockHtmlHelper.Object);
            var builder = new DateTimeBuilder(component, null);

            builder.Disabled(true);

            Assert.True(component.IsDisabled);
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

            var result = builder.ClientId("test").Name("testName").Disabled(true);

            Assert.IsType<DateTimeBuilder>(result);
            Assert.Equal("test", component.ClientId);
            Assert.Equal("testName", component.Name);
            Assert.True(component.IsDisabled);
        }

        [Fact]
        public void WriteInitScript_GeneratesJavaScriptInitialization()
        {
            var component = new DateTimeComponent(_mockHtmlHelper.Object)
            {
                ClientId = "testDateTime"
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
