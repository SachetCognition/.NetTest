using Xunit;
using Microsoft.AspNetCore.Mvc.Rendering;
using Moq;
using System.IO;
using System.Collections.Generic;
using Equant.SAV2000.ComponentLibrary.MVC.Components.VerticalMenu;
using Equant.SAV2000.ComponentLibrary.MVC.Components.Menu;

namespace Equant.SAV2000.ComponentLibrary.MVC.UnitTests.Components
{
    public class VerticalMenuComponentTests
    {
        private readonly Mock<IHtmlHelper> _mockHtmlHelper;

        public VerticalMenuComponentTests()
        {
            _mockHtmlHelper = new Mock<IHtmlHelper>();
        }

        [Fact]
        public void Constructor_InitializesWithDefaultValues()
        {
            var component = new VerticalMenuComponent(_mockHtmlHelper.Object);

            Assert.NotNull(component);
            Assert.True(component.IsVisible);
            Assert.NotNull(component.MenuItems);
        }

        [Fact]
        public void Builder_SetsId()
        {
            var component = new VerticalMenuComponent(_mockHtmlHelper.Object);
            var builder = new VerticalMenuBuilder(component, null);

            builder.Id("myVerticalMenu");

            Assert.Equal("myVerticalMenu", component.Id);
        }

        [Fact]
        public void Builder_SetsName()
        {
            var component = new VerticalMenuComponent(_mockHtmlHelper.Object);
            var builder = new VerticalMenuBuilder(component, null);

            builder.Name("VerticalMenu1");

            Assert.Equal("VerticalMenu1", component.Name);
        }

        [Fact]
        public void Builder_SetsMenuItems()
        {
            var component = new VerticalMenuComponent(_mockHtmlHelper.Object);
            var builder = new VerticalMenuBuilder(component, null);
            var menuItems = new List<MenuItem>
            {
                new MenuItem { Text = "Item 1", Url = "/item1" }
            };

            builder.MenuItems(menuItems);

            Assert.Single(component.MenuItems);
            Assert.Equal("Item 1", component.MenuItems[0].Text);
        }

        [Fact]
        public void WriteHtml_GeneratesVerticalMenuMarkup()
        {
            var component = new VerticalMenuComponent(_mockHtmlHelper.Object)
            {
                Id = "testVerticalMenu",
                Name = "testVerticalMenuName"
            };

            using var writer = new StringWriter();

            component.WriteHtml(writer);
            var html = writer.ToString();

            Assert.Contains("testVerticalMenu", html);
        }

        [Fact]
        public void WriteHtml_WhenNotVisible_ShouldNotGenerateHtml()
        {
            var component = new VerticalMenuComponent(_mockHtmlHelper.Object);
            component.IsVisible = false;
            var writer = new StringWriter();

            component.WriteHtml(writer);
            var html = writer.ToString();

            Assert.Empty(html);
        }

        [Fact]
        public void JsResources_ReturnsExpectedResources()
        {
            var component = new VerticalMenuComponent(_mockHtmlHelper.Object);

            var resources = component.JsResources;

            Assert.NotNull(resources);
        }

        [Fact]
        public void WriteInitScript_GeneratesJavaScriptInitialization()
        {
            var component = new VerticalMenuComponent(_mockHtmlHelper.Object)
            {
                Id = "testVerticalMenu"
            };

            using var writer = new StringWriter();

            component.WriteInitScript(writer);
            var script = writer.ToString();

            Assert.Contains("testVerticalMenu", script);
        }

        [Fact]
        public void Builder_ReturnsBuilderForFluentApi()
        {
            var component = new VerticalMenuComponent(_mockHtmlHelper.Object);
            var builder = new VerticalMenuBuilder(component, null);

            var result = builder.Id("test").Name("testName");

            Assert.IsType<VerticalMenuBuilder>(result);
            Assert.Equal("test", component.Id);
            Assert.Equal("testName", component.Name);
        }
    }
}
