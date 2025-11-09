using Xunit;
using Microsoft.AspNetCore.Mvc.Rendering;
using Moq;
using System.IO;
using System.Data;
using Equant.SAV2000.ComponentLibrary.MVC.Components.TreeGrid;

namespace Equant.SAV2000.ComponentLibrary.MVC.UnitTests.Components
{
    public class TreeGridComponentTests
    {
        private readonly Mock<IHtmlHelper> _mockHtmlHelper;

        public TreeGridComponentTests()
        {
            _mockHtmlHelper = new Mock<IHtmlHelper>();
        }

        [Fact]
        public void Constructor_InitializesWithDefaultValues()
        {
            var treeData = new DataTable();
            var component = new TreeGridComponent(_mockHtmlHelper.Object, treeData);

            Assert.NotNull(component);
            Assert.True(component.IsVisible);
            Assert.NotNull(component.Data);
        }

        [Fact]
        public void Builder_SetsId()
        {
            var treeData = new DataTable();
            var component = new TreeGridComponent(_mockHtmlHelper.Object, treeData);
            var builder = new TreeGridBuilder(component, null);

            builder.Id("myTreeGrid");

            Assert.Equal("myTreeGrid", component.Id);
        }

        [Fact]
        public void Builder_SetsName()
        {
            var treeData = new DataTable();
            var component = new TreeGridComponent(_mockHtmlHelper.Object, treeData);
            var builder = new TreeGridBuilder(component, null);

            builder.Name("TreeGrid1");

            Assert.Equal("TreeGrid1", component.Name);
        }

        [Fact]
        public void WriteHtml_GeneratesTreeGridMarkup()
        {
            var treeData = new DataTable();
            var component = new TreeGridComponent(_mockHtmlHelper.Object, treeData)
            {
                Id = "testTreeGrid",
                Name = "testTreeGridName"
            };

            using var writer = new StringWriter();

            component.WriteHtml(writer);
            var html = writer.ToString();

            Assert.Contains("testTreeGrid", html);
        }

        [Fact]
        public void WriteHtml_WhenNotVisible_ShouldNotGenerateHtml()
        {
            var treeData = new DataTable();
            var component = new TreeGridComponent(_mockHtmlHelper.Object, treeData);
            component.IsVisible = false;
            var writer = new StringWriter();

            component.WriteHtml(writer);
            var html = writer.ToString();

            Assert.Empty(html);
        }

        [Fact]
        public void JsResources_ReturnsExpectedResources()
        {
            var treeData = new DataTable();
            var component = new TreeGridComponent(_mockHtmlHelper.Object, treeData);

            var resources = component.JsResources;

            Assert.NotNull(resources);
        }

        [Fact]
        public void WriteInitScript_GeneratesJavaScriptInitialization()
        {
            var treeData = new DataTable();
            var component = new TreeGridComponent(_mockHtmlHelper.Object, treeData)
            {
                Id = "testTreeGrid"
            };

            using var writer = new StringWriter();

            component.WriteInitScript(writer);
            var script = writer.ToString();

            Assert.Contains("testTreeGrid", script);
        }

        [Fact]
        public void Builder_ReturnsBuilderForFluentApi()
        {
            var treeData = new DataTable();
            var component = new TreeGridComponent(_mockHtmlHelper.Object, treeData);
            var builder = new TreeGridBuilder(component, null);

            var result = builder.Id("test").Name("testName");

            Assert.IsType<TreeGridBuilder>(result);
            Assert.Equal("test", component.Id);
            Assert.Equal("testName", component.Name);
        }
    }
}
