using Xunit;
using Microsoft.AspNetCore.Mvc.Rendering;
using Moq;
using System.IO;
using System.Collections.Generic;
using Equant.SAV2000.ComponentLibrary.MVC.Components.CheckBoxList;

namespace Equant.SAV2000.ComponentLibrary.MVC.UnitTests.Components
{
    public class CheckBoxListComponentTests
    {
        private readonly Mock<IHtmlHelper> _mockHtmlHelper;

        public CheckBoxListComponentTests()
        {
            _mockHtmlHelper = new Mock<IHtmlHelper>();
        }

        [Fact]
        public void Constructor_InitializesWithDefaultValues()
        {
            var component = new CheckBoxListComponent(_mockHtmlHelper.Object);

            Assert.NotNull(component);
            Assert.Empty(component.SourceItems);
            Assert.False(component.IsDisabled);
            Assert.Equal("null", component.OnChange);
        }

        [Fact]
        public void WriteHtml_GeneratesCheckBoxListMarkup()
        {
            var component = new CheckBoxListComponent(_mockHtmlHelper.Object)
            {
                Id = "testCheckBoxList",
                SourceItems = new List<CheckBoxListItem>
                {
                    new CheckBoxListItem { Value = "1", Text = "Option 1", Selected = true },
                    new CheckBoxListItem { Value = "2", Text = "Option 2", Selected = false }
                }
            };

            using var writer = new StringWriter();

            component.WriteHtml(writer);
            var html = writer.ToString();

            Assert.Contains("testCheckBoxList", html);
        }

        [Fact]
        public void Builder_SetsId()
        {
            var component = new CheckBoxListComponent(_mockHtmlHelper.Object);
            var builder = new CheckBoxListBuilder(component, null);

            builder.Id("myCheckBoxList");

            Assert.Equal("myCheckBoxList", component.Id);
        }

        [Fact]
        public void Builder_SetsName()
        {
            var component = new CheckBoxListComponent(_mockHtmlHelper.Object);
            var builder = new CheckBoxListBuilder(component, null);

            builder.Name("CheckBoxList1");

            Assert.Equal("CheckBoxList1", component.Name);
        }

        [Fact]
        public void Builder_SetsDisabled()
        {
            var component = new CheckBoxListComponent(_mockHtmlHelper.Object);
            var builder = new CheckBoxListBuilder(component, null);

            builder.Disabled(true);

            Assert.True(component.IsDisabled);
        }

        [Fact]
        public void Builder_SetsOnChange()
        {
            var component = new CheckBoxListComponent(_mockHtmlHelper.Object);
            var builder = new CheckBoxListBuilder(component, null);

            builder.OnChange("handleChange()");

            Assert.Equal("handleChange()", component.OnChange);
        }

        [Fact]
        public void Builder_ReturnsBuilderForFluentApi()
        {
            var component = new CheckBoxListComponent(_mockHtmlHelper.Object);
            var builder = new CheckBoxListBuilder(component, null);

            var result = builder.Id("test").Disabled(true);

            Assert.IsType<CheckBoxListBuilder>(result);
            Assert.Equal("test", component.Id);
            Assert.True(component.IsDisabled);
        }

        [Fact]
        public void WriteInitScript_GeneratesJavaScriptInitialization()
        {
            var component = new CheckBoxListComponent(_mockHtmlHelper.Object)
            {
                Id = "testCheckBoxList",
                OnChange = "myChangeHandler()"
            };

            using var writer = new StringWriter();

            component.WriteInitScript(writer);
            var script = writer.ToString();

            Assert.Contains("testCheckBoxList", script);
        }

        [Fact]
        public void JsResources_ReturnsExpectedResources()
        {
            var component = new CheckBoxListComponent(_mockHtmlHelper.Object);

            var resources = component.JsResources;

            Assert.NotNull(resources);
        }
    }
}
