using Xunit;
using Microsoft.AspNetCore.Mvc.Rendering;
using Moq;
using System.IO;
using Equant.SAV2000.ComponentLibrary.MVC.Components.Menu;

namespace Equant.SAV2000.ComponentLibrary.MVC.UnitTests.Components
{
    public class MenuComponentTests
    {
        private readonly Mock<IHtmlHelper> _mockHtmlHelper;

        public MenuComponentTests()
        {
            _mockHtmlHelper = new Mock<IHtmlHelper>();
        }

        [Fact]
        public void Constructor_InitializesWithDefaultValues()
        {
            var component = new MenuComponent(_mockHtmlHelper.Object);

            Assert.NotNull(component);
            Assert.True(component.IsVisible);
        }

        [Fact]
        public void Builder_SetsId()
        {
            var component = new MenuComponent(_mockHtmlHelper.Object);
            var builder = new MenuBuilder(component, null);

            builder.Id("myMenu");

            Assert.Equal("myMenu", component.Id);
        }

        [Fact]
        public void Builder_SetsName()
        {
            var component = new MenuComponent(_mockHtmlHelper.Object);
            var builder = new MenuBuilder(component, null);

            builder.Name("Menu1");

            Assert.Equal("Menu1", component.Name);
        }

        [Fact]
        public void WriteHtml_GeneratesMenuMarkup()
        {
            var component = new MenuComponent(_mockHtmlHelper.Object)
            {
                Id = "testMenu",
                Name = "testMenuName"
            };

            using var writer = new StringWriter();

            component.WriteHtml(writer);
            var html = writer.ToString();

            Assert.Contains("testMenu", html);
        }

        [Fact]
        public void WriteHtml_WhenNotVisible_ShouldNotGenerateHtml()
        {
            var component = new MenuComponent(_mockHtmlHelper.Object);
            component.IsVisible = false;
            var writer = new StringWriter();

            component.WriteHtml(writer);
            var html = writer.ToString();

            Assert.Empty(html);
        }

        [Fact]
        public void JsResources_ReturnsExpectedResources()
        {
            var component = new MenuComponent(_mockHtmlHelper.Object);

            var resources = component.JsResources;

            Assert.NotNull(resources);
        }

        [Fact]
        public void WriteInitScript_GeneratesJavaScriptInitialization()
        {
            var component = new MenuComponent(_mockHtmlHelper.Object)
            {
                Id = "testMenu"
            };

            using var writer = new StringWriter();

            component.WriteInitScript(writer);
            var script = writer.ToString();

            Assert.Contains("testMenu", script);
        }

        [Fact]
        public void Builder_ReturnsBuilderForFluentApi()
        {
            var component = new MenuComponent(_mockHtmlHelper.Object);
            var builder = new MenuBuilder(component, null);

            var result = builder.Id("test").Name("testName");

            Assert.IsType<MenuBuilder>(result);
            Assert.Equal("test", component.Id);
            Assert.Equal("testName", component.Name);
        }
    }
}
