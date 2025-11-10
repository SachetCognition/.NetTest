using Xunit;
using Microsoft.AspNetCore.Mvc.Rendering;
using Moq;
using System.IO;
using Equant.SAV2000.ComponentLibrary.MVC.Components.DataTable;
using Equant.SAV2000.ComponentLibrary.Common.Components.DataTables;

namespace Equant.SAV2000.ComponentLibrary.MVC.UnitTests.Components
{
    public class DataTableComponentTests
    {
        private readonly Mock<IHtmlHelper> _mockHtmlHelper;

        public DataTableComponentTests()
        {
            _mockHtmlHelper = new Mock<IHtmlHelper>();
        }

        [Fact]
        public void Constructor_InitializesWithDefaultValues()
        {
            var component = new DataTableComponent(_mockHtmlHelper.Object);

            Assert.NotNull(component);
            Assert.True(component.IsVisible);
            Assert.NotNull(component.Columns);
        }

        [Fact]
        public void Builder_SetsId()
        {
            var component = new DataTableComponent(_mockHtmlHelper.Object);
            var builder = new DataTableBuilder(component, null);

            builder.Id("myDataTable");

            Assert.Equal("myDataTable", component.Id);
        }

        [Fact]
        public void Builder_SetsName()
        {
            var component = new DataTableComponent(_mockHtmlHelper.Object);
            var builder = new DataTableBuilder(component, null);

            builder.Name("DataTable1");

            Assert.Equal("DataTable1", component.Name);
        }

        [Fact]
        public void Builder_SetsCssClass()
        {
            var component = new DataTableComponent(_mockHtmlHelper.Object);
            var builder = new DataTableBuilder(component, null);

            builder.CssClass("custom-datatable");

            Assert.Equal("custom-datatable", component.CssClass);
        }

        [Fact]
        public void WriteHtml_GeneratesDataTableMarkup()
        {
            var component = new DataTableComponent(_mockHtmlHelper.Object)
            {
                Id = "testDataTable",
                Name = "testDataTableName"
            };

            using var writer = new StringWriter();

            component.WriteHtml(writer);
            var html = writer.ToString();

            Assert.Contains("testDataTable", html);
        }

        [Fact]
        public void WriteHtml_WhenNotVisible_ShouldNotGenerateHtml()
        {
            var component = new DataTableComponent(_mockHtmlHelper.Object);
            component.IsVisible = false;
            var writer = new StringWriter();

            component.WriteHtml(writer);
            var html = writer.ToString();

            Assert.Empty(html);
        }

        [Fact]
        public void JsResources_ReturnsExpectedResources()
        {
            var component = new DataTableComponent(_mockHtmlHelper.Object);

            var resources = component.JsResources;

            Assert.NotNull(resources);
        }

        [Fact]
        public void WriteInitScript_GeneratesJavaScriptInitialization()
        {
            var component = new DataTableComponent(_mockHtmlHelper.Object)
            {
                Id = "testDataTable"
            };

            using var writer = new StringWriter();

            component.WriteInitScript(writer);
            var script = writer.ToString();

            Assert.Contains("testDataTable", script);
        }

        [Fact]
        public void Builder_ReturnsBuilderForFluentApi()
        {
            var component = new DataTableComponent(_mockHtmlHelper.Object);
            var builder = new DataTableBuilder(component, null);

            var result = builder.Id("test").Name("testName");

            Assert.IsType<DataTableBuilder>(result);
            Assert.Equal("test", component.Id);
            Assert.Equal("testName", component.Name);
        }

        [Fact]
        public void DataTableOptions_InitializesCorrectly()
        {
            var options = new DataTableOptions();

            Assert.NotNull(options);
        }
    }
}
