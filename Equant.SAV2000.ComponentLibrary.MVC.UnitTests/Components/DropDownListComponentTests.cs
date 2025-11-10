using Xunit;
using Microsoft.AspNetCore.Mvc.Rendering;
using Moq;
using System.IO;
using System.Collections.Generic;
using Equant.SAV2000.ComponentLibrary.MVC.Components.DropDownList;

namespace Equant.SAV2000.ComponentLibrary.MVC.UnitTests.Components
{
    public class DropDownListComponentTests
    {
        private readonly Mock<IHtmlHelper> _mockHtmlHelper;

        public DropDownListComponentTests()
        {
            _mockHtmlHelper = new Mock<IHtmlHelper>();
        }

        [Fact]
        public void Constructor_InitializesWithDefaultValues()
        {
            var component = new DropDownListComponent(_mockHtmlHelper.Object);

            Assert.NotNull(component);
            Assert.True(component.IsVisible);
        }

        [Fact]
        public void Builder_SetsId()
        {
            var component = new DropDownListComponent(_mockHtmlHelper.Object);
            var builder = new DropDownListBuilder(component, null);

            builder.Id("myDropDownList");

            Assert.Equal("myDropDownList", component.Id);
        }

        [Fact]
        public void Builder_SetsName()
        {
            var component = new DropDownListComponent(_mockHtmlHelper.Object);
            var builder = new DropDownListBuilder(component, null);

            builder.Name("DropDownList1");

            Assert.Equal("DropDownList1", component.Name);
        }

        [Fact]
        public void WriteHtml_GeneratesDropDownListMarkup()
        {
            var component = new DropDownListComponent(_mockHtmlHelper.Object)
            {
                Id = "testDropDownList",
                Name = "testDropDownListName"
            };

            using var writer = new StringWriter();

            component.WriteHtml(writer);
            var html = writer.ToString();

            Assert.Contains("testDropDownList", html);
        }

        [Fact]
        public void WriteHtml_WhenNotVisible_ShouldNotGenerateHtml()
        {
            var component = new DropDownListComponent(_mockHtmlHelper.Object);
            component.IsVisible = false;
            var writer = new StringWriter();

            component.WriteHtml(writer);
            var html = writer.ToString();

            Assert.Empty(html);
        }

        [Fact]
        public void JsResources_ReturnsExpectedResources()
        {
            var component = new DropDownListComponent(_mockHtmlHelper.Object);

            var resources = component.JsResources;

            Assert.NotNull(resources);
        }

        [Fact]
        public void WriteInitScript_GeneratesJavaScriptInitialization()
        {
            var component = new DropDownListComponent(_mockHtmlHelper.Object)
            {
                Id = "testDropDownList"
            };

            using var writer = new StringWriter();

            component.WriteInitScript(writer);
            var script = writer.ToString();

            Assert.Contains("testDropDownList", script);
        }

        [Fact]
        public void Builder_ReturnsBuilderForFluentApi()
        {
            var component = new DropDownListComponent(_mockHtmlHelper.Object);
            var builder = new DropDownListBuilder(component, null);

            var result = builder.Id("test").Name("testName");

            Assert.IsType<DropDownListBuilder>(result);
            Assert.Equal("test", component.Id);
            Assert.Equal("testName", component.Name);
        }
    }
}
